""" Trains an agent with (stochastic) Policy Gradients on Pong. Uses OpenAI Gym. """
import numpy as np
import _pickle as pickle
# import gym
import colour_flood
import time

# hyperparameters
H = 200  # number of hidden layer neurons
batch_size = 10  # every how many episodes to do a param update?
learning_rate = 1e-4
gamma = 0.9  # discount factor for reward
decay_rate = 0.9  # decay factor for RMSProp leaky sum of grad^2
resume = True  # resume from previous checkpoint?
render = False

# model initialization
GRID_SIZE = 10
S = 6
D = S * GRID_SIZE * GRID_SIZE  # input dimensionality: 80x80 grid
if resume:
    model = pickle.load(open('save.p', 'rb'))
else:
    model = {}
    model['W1'] = np.random.randn(H, D) / np.sqrt(D)  # "Xavier" initialization
    model['W2'] = np.random.randn(S, H) / np.sqrt(H)

# update buffers that add up gradients over a batch
grad_buffer = {k: np.zeros_like(v) for k, v in model.items()}
rmsprop_cache = {k: np.zeros_like(v)
                 for k, v in model.items()}  # rmsprop memory


def sigmoid(x):
    # sigmoid "squashing" function to interval [0,1]
    return 1.0 / (1.0 + np.exp(-x))

def dsigmoid(x):
    # sigmoid "squashing" function to interval [0,1]
    return np.exp(-x) / pow((1.0 + np.exp(-x)), 2)


def prepro(I):
    """ prepro 210x160x3 uint8 frame into 6400 (80x80) 1D float vector """
    I_new = [False for _ in range(D)]
    ind = 0
    for row in I:
        for v in row:
            I_new[ind + v] = True
            ind += S
    return np.array(I_new)


def discount_rewards(r):
    """ take 1D float array of rewards and compute discounted reward """
    discounted_r = np.zeros_like(r, dtype=float)
    running_add = 0
    for t in reversed(range(r.size)):
        # if r[t] != 0:
        #     # reset the sum, since this was a game boundary (pong specific!)
        #     running_add = 0
        running_add = running_add * gamma + r[t]
        discounted_r[t] = running_add
    return discounted_r


def policy_forward(x):
    h = np.dot(model['W1'], x)
    h[h < 0] = 0  # ReLU nonlinearity

    sigmoid_v = np.vectorize(sigmoid)
    p = sigmoid_v(np.dot(model['W2'], h))
    return p, h  # return probability of taking action 2, and hidden state


def policy_backward(eph, epdlogp):
    """ backward pass. (eph is array of intermediate hidden states) """
    dW2 = np.dot(epdlogp.T, eph)
    dh = np.dot(epdlogp, model['W2'])
    dh[eph <= 0] = 0  # backpro prelu
    dW1 = np.dot(dh.T, epx)
    return {'W1': dW1, 'W2': dW2}


cf = colour_flood.ColourFlood(GRID_SIZE, S, render)
cur_x = cf.reset()
prev_x = None  # used in computing the difference frame
xs, hs, dlogps, drs = [], [], [], []
running_reward = None
reward_sum = 0
episode_number = 0
start_time = time.time()
while True:
    if render:
        cf.render()

    # forward the policy network and sample an action from the returned probability
    x = prepro(cur_x)
    p, h = policy_forward(x)

    # cannot choose player's or opponent's current color
    pl_s, op_s = cf.getState()
    op_p = p[op_s]
    pl_p = p[pl_s]
    p[op_s] = -1
    p[pl_s] = -1

    state = np.argmax(p)
    aprob = p[state]
    if np.random.uniform() < aprob:
        action = state
    else:  # roll the dice!
        choices = [v for v in range(S) if v not in {state, op_s, pl_s}]
        action = np.random.choice(choices)

    p[op_s] = op_p
    p[pl_s] = pl_p

    # record various intermediates (needed later for backprop)
    xs.append(x)  # observation
    hs.append(h)  # hidden state

    labeldiff = -p
    labeldiff[action] += 1
    # y = 1 if action == state else 0  # a "fake label"
    # grad that encourages the action that was taken to be taken (see http://cs231n.github.io/neural-networks-2/#losses if confused)
    dlogps.append(labeldiff)

    # step the environment and get new measurements
    cur_x, reward, done = cf.step(action)
    opp_reward = 0
    if not done: # roll the dice!
        choices = [v for v in range(S) if v not in {op_s, pl_s}]
        action = np.random.choice(choices)
        cur_x, opp_reward, done = cf.step(action, False)

    # punishment for hogging
    # if time.time() - start_time > 0.5:
    #     reward -= 0.5

    reward_sum += reward - opp_reward

    # record reward (has to be done after we call step() to get reward for previous action)
    drs.append(reward - opp_reward)

    if time.time() - start_time > 0.5 or done:  # an episode finished
        start_time = time.time()
        episode_number += 1

        # stack together all inputs, hidden states, action gradients, and rewards for this episode
        epx = np.vstack(xs)
        eph = np.vstack(hs)
        epdlogp = np.vstack(dlogps)
        epr = np.vstack(drs)
        xs, hs, dlogps, drs = [], [], [], []  # reset array memory

        # compute the discounted reward backwards through time
        discounted_epr = discount_rewards(epr)
        # standardize the rewards to be unit normal (helps control the gradient estimator variance)
        discounted_epr -= np.mean(discounted_epr)
        if np.std(discounted_epr):
            discounted_epr /= np.std(discounted_epr)

        # modulate the gradient with advantage (PG magic happens right here.)
        epdlogp *= discounted_epr
        grad = policy_backward(eph, epdlogp)
        for k in model:
            grad_buffer[k] += grad[k]  # accumulate grad over batch

        # perform rmsprop parameter update every batch_size episodes
        if episode_number % batch_size == 0:
            for k, v in model.items():
                g = grad_buffer[k]  # gradient
                rmsprop_cache[k] = decay_rate * rmsprop_cache[k] + (1 - decay_rate) * g**2
                model[k] += learning_rate * g / (np.sqrt(rmsprop_cache[k]) + 1e-5)
                # reset batch gradient buffer
                grad_buffer[k] = np.zeros_like(v)

        # boring book-keeping
        running_reward = reward_sum if running_reward is None else running_reward * \
            0.99 + reward_sum * 0.01
        print('ep %d: reward total was %f. running mean: %f' %
              (episode_number, reward_sum, running_reward))
        if episode_number % 100 == 0:
            pickle.dump(model, open('save.p', 'wb'))
        reward_sum = 0
        observation = cf.reset()  # reset env
        prev_x = None
