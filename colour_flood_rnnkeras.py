""" Trains an agent with (stochastic) Policy Gradients on Pong. Uses OpenAI Gym. """
import numpy as np
import _pickle as pickle
# import gym
from colour_flood import ColourFlood
import time
import random

import keras
from keras.models import Sequential, load_model, Model
from keras.layers import Dense, Dropout, Flatten, Lambda, Input, concatenate, merge, Reshape
from keras.layers import Conv2D, MaxPooling2D
from keras import backend as K
from sklearn.utils import shuffle
from scipy.special import softmax

keras.backend.set_image_data_format('channels_first')
# hyperparameters
gamma = 0.9  # discount factor for reward
resume = False  # resume from previous checkpoint?
render = True

# model initialization
GRID_SIZE = 10
S = 6
Sin = S + 2
D = GRID_SIZE  # input dimensionality: 80x80 grid


def model_icm():
	if resume:
		icm_model = load_model('icm_model.h5')
	else:
		# input
		s_0 = Input(shape=(Sin, D, D), name="state0")
		s_1 = Input(shape=(Sin, D, D), name="state1")
		a = Input(shape=(S,), name="action")

		# feature model
		model = Sequential()
		model.add(Flatten())
		model.add(Dense(400, activation='relu'))
		# model.add(Dense(200, activation='relu'))
		
		f_0 = model(s_0)
		f_1 = model(s_1)

		# auto-enc
		s_0_flat = Flatten()(s_0)
		f_0_fc1 = Dense(400, activation='relu')(f_0)
		# f_0_fc2 = Dense(400, activation='relu')(f_0_fc1)
		s_0_hat = Dense(Sin*D*D, activation='sigmoid', name="s_0_hat")(f_0_fc1)
		aut_loss = Lambda(lambda x: 0.5 * K.sum(K.square(x[0] - x[1]), axis=-1), output_shape=(1,), name="aut_loss")([s_0_flat, s_0_hat])
		
		# inverse
		# i_conc = concatenate([f_0, f_1])
		# i_fc1 = Dense(100, activation='relu')(i_conc)
		# i_fc1 = Dropout(0.3)(i_fc1)
		# a_hat = Dense(S, activation='softmax', name="a_hat")(i_conc)
		# inw_loss = Lambda(lambda x: -K.sum(x[0] * K.log(x[1] + K.epsilon()), axis=-1), output_shape = (1,))([a, a_hat])

		# forward
		f_conc = concatenate([f_0, a])
		f_fc1 = Dense(400, activation='relu')(f_conc)
		# f_fc1 = Dropout(0.3)(f_fc1)
		f_1_hat = Dense(400, activation='sigmoid', name="f_1_hat")(f_fc1)
		forw_loss = Lambda(lambda x: 0.5 * K.sum(K.square(x[0] - x[1]), axis=-1), output_shape=(1,), name="in_reward")([f_1, f_1_hat])
		
		beta = 0.5
		# total_loss = Lambda(lambda x: beta * x[0] + (1.0 - beta) * (x[2]) + 0*x[1], output_shape=(1,))([forw_loss, inw_loss, aut_loss])
		total_loss = Lambda(lambda x: beta * x[0] + (1.0 - beta) * (x[1]), output_shape=(1,))([forw_loss, aut_loss])

		icm_model = Model(input=[s_0, s_1, a], output=[total_loss])
	forward_model = Model(input=icm_model.input, output=[icm_model.get_layer("in_reward").output])

	icm_model.compile(loss=keras.losses.mean_squared_error,
			   optimizer=keras.optimizers.Adadelta(),
			   metrics=['accuracy'])

	return icm_model, forward_model


def model_d2nn():
	if resume:
		model = load_model('model.h5')
	else:
		model = Sequential()

		input_layer = Input(shape=(Sin, D, D))
		conv1 = Conv2D(32, 4, strides=(1, 1), activation='relu')(input_layer)
		conv2 = Conv2D(64, 3, strides=(1, 1), activation='relu')(conv1)
		conv3 = Conv2D(64, 1, strides=(1, 1), activation='relu')(conv2)
		# pool = MaxPooling2D(pool_size=(2, 2))(conv3)

		flat = Flatten()(conv3)
		# fc1 = Dense(400, activation='relu')(flat)

		a_fc = Dense(100, activation='relu')(flat)
		advantage = Dense(S, activation='linear', name="advantage")(a_fc)

		v_fc = Dense(100, activation='relu')(flat)
		value = Dense(1, activation='linear', name="value")(v_fc)

		policy = Lambda(lambda x: x[0]-K.mean(x[0])+x[1])([advantage, value])

		model = Model(input=[input_layer], output=[policy])
		# model_a = Model(input=[input_layer], output=[advantage])
		# model_v = Model(input=[input_layer], output=[value])

	model_a = Model(input=[model.input], output=[model.get_layer("advantage").output])
	model_v = Model(input=[model.input], output=[model.get_layer("value").output])
	model.compile(loss=keras.losses.mean_squared_error,
			   optimizer=keras.optimizers.Adadelta(),
			   metrics=['accuracy'])
	return model, model_a, model_v

def get_action(model, x, out_name):
	x[0] = x[0].reshape(1, Sin, D, D)
	x[1] = x[1].reshape(1, Sin, D, D)
	x[2] = x[2].reshape(1, S)
	in_name = ['state0', 'state1', 'action']
	inp = [model.get_layer(v).output for v in in_name]
	out = [model.get_layer(v).output for v in out_name]
	return K.function(inp, out)(x)

def prepro(I):	
	I_new = np.zeros((Sin, D, D), dtype='bool')
	for j in range(len(I)):
		for i in range(len(I[0])):
			I_new[I[j][i]][j][i] = True

	pl, op = cf.getTeri()
	for x, y in pl:
		I_new[S][y][x] = True
	for x, y in op:
		I_new[S+1][y][x] = True
	return I_new

def icm_prepro(I):
	I_new = I.copy()
	# I_new[:, I[-1]] = False
	# I_new = I_new[:-1]
	return I_new

def d_prepro(I):
	I_new = np.zeros((D, D))
	for i, arr in enumerate(I[:S]):
		I_new[arr] = i
	return I_new

def d1_prepro(I):
	I_new = np.zeros((D, D))
	I = I > 0.5
	ind = 0
	for j in range(D):
		for i in range(D):
			for k in range(Sin):
				if I[ind] and k < S:
					I_new[j][i] = k
				ind += 1
	return I_new


def filter_q(q, state):
	for i, s in enumerate(state[:S]):
		if s[0][0] or s[-1][-1]:
			# q[i] = -10
			pass
	return q

def model_action(x):
	global explo
	d_x = cur_x
	pl_s, op_s = cf.getState()

	cur_q = model.predict(x.reshape(1, Sin, D, D))[0]
	v = model_v.predict(x.reshape(1, Sin, D, D))[0]
	a = model_a.predict(x.reshape(1, Sin, D, D))[0]

	state_prob = softmax(cur_q)
	state = np.argmax(cur_q)

	choices = [v for v in range(S) if v not in {op_s, pl_s}]
	if state in choices:
		action = state
	else:  # roll the dice!
		# action = np.random.choice(choices)
		action = int(d_prepro(x)[0][0])

	return action


model, model_a, model_v = model_d2nn()
icm_model, forward_model = model_icm()
print(model.summary())
print(icm_model.summary())

cf = ColourFlood(GRID_SIZE, S, render)
cur_x = cf.reset()
pre_reward = None
win = [0, 0, 0]
done_rec = 0
running_reward = None
reward_sum = 0
episode_number = 0
explore = 0
opp_x = None

traject = []
traject_w = []
traject_l = []
sample_sz = 1000
traject_sz = 5000
ep_batch = 50
ep_steps = 0

lr = 0.9
tau = .5
prev_model = keras.models.clone_model(model)
prev_model.build((S, D, D))
prev_model.set_weights(model.get_weights())

while True:
	if render:
		cf.render()

	# forward the policy network and sample an action from the returned probability
	x = prepro(cur_x)
	action = model_action(x)
	cur_x, reward, done = cf.step(action)
	# store log
	# if opp_x is not None:
	# 	t_opp_x = prepro(cur_x)
	# 	t_opp_x = np.rot90(t_opp_x, 2, axes=(1, 2))
	# 	t_opp_x[S], t_opp_x[S+1] = t_opp_x[S+1].copy(), t_opp_x[S].copy()
	# 	traject.append([opp_x, opp_action, 0, t_opp_x, done])

	# opponent's turn
	if not done:  # roll the dice!
		opp_x = prepro(cur_x)
		opp_x = np.rot90(opp_x, 2, axes=(1, 2))
		opp_x[S], opp_x[S+1] = opp_x[S+1].copy(), opp_x[S].copy()
		if True:
			opp_action = model_action(opp_x)
		else:
			pl_s, op_s = cf.getState()
			choices = [v for v in range(S) if v not in {pl_s, op_s}]
			opp_action = np.random.choice(choices)
			# opp_action = op_s
		# print(cur_x)
		# opp_action=int(input())
		cur_x, opp_reward, done = cf.step(opp_action, False)

		# store log
		# t_opp_x = prepro(cur_x)
		# t_opp_x = np.rot90(t_opp_x, 2, axes=(1, 2))
		# t_opp_x[S], t_opp_x[S+1] = t_opp_x[S+1].copy(), t_opp_x[S].copy()
		# traject.append([opp_x, opp_action, 0, t_opp_x, done])
		# traject.append([opp_x, opp_action, opp_reward, np.rot90(prepro(cur_x), 2, axes=(1, 2)), done])

	reward_sum += reward - opp_reward
	# store log
	traject.append([x, action, -0, prepro(cur_x), done])
	# traject.append([np.rot90(x, 2, axes=(1, 2)), action, reward-opp_reward, np.rot90(prepro(cur_x), 2, axes=(1, 2)), done])

	ep_steps += 1

	# prevent hogging
	if done or ep_steps > 30 or ((traject[-1][0][-2] == traject[-1][3][-2]).all() and (traject[-1][0][-1] == traject[-1][3][-1]).all()):
	# 	if ((traject[-1][0] == traject[-1][3]).all()) or (len(traject) > 1 and (traject[-2][0] == traject[-1][3]).all()):
	# 		traject[-1][2] -= 5
	# if done or ep_steps > 30:
		opp_x = None
		traject[-1][-1] = True
		pl, op = cf.getTeri()
		if len(pl) > len(op):
			win[0] += 1
		elif len(pl) == len(op):
			win[1] += 1
		else:
			win[2] += 1
		if done:
			done_rec += 1
		# if done:
		traject[-1][2] = (len(pl))/3
		# 	traject_w.append(traject[-1])
		# 	traject_w = traject_w[-traject_sz:]
		# 	del traject[-1]
		# elif traject[-1][2] < 0:
		# 	traject_l.append(traject[-1])
		# 	traject_l = traject_l[-traject_sz:]
		# 	del traject[-1]

		traject = traject[-traject_sz:]

		episode_number += 1
		ep_steps = 0
		cur_x = cf.reset()  # reset env
		# opponent first
		if np.random.uniform() < 0.5:
			opp_x = prepro(cur_x)
			opp_x = np.rot90(opp_x, 2, axes=(1, 2))
			opp_x[S], opp_x[S+1] = opp_x[S+1].copy(), opp_x[S].copy()
			if True:
				opp_action = model_action(opp_x)
			else:
				pl_s, op_s = cf.getState()
				choices = [v for v in range(S) if v not in {pl_s, op_s}]
				opp_action = np.random.choice(choices)
				# opp_action = op_s

			cur_x, opp_reward, done = cf.step(opp_action, False)

			# store log
			# traject.append([opp_x, opp_action, opp_reward, np.rot90(prepro(cur_x), 2, axes=(1, 2)), done])

		if episode_number % ep_batch == 0:
			# update target network
			if episode_number % (1*ep_batch) == 0:
				prev_model_weights = prev_model.get_weights()
				model_weights = model.get_weights()
				for i in range(len(model_weights)):
					prev_model_weights[i] = tau * prev_model_weights[i] + (1 - tau) * model_weights[i]
				prev_model.set_weights(prev_model_weights)
					

			states, qs = [], []  # reset array memory
			n_states, actions_hot = [], []
			# mini = min(len(traject_l), len(traject_w))
			# t_traject = traject[:mini] + traject_w[:mini] + traject_l[:mini]
			t_traject = traject
			samples = random.sample(t_traject, min(len(t_traject), sample_sz))
			
			in_rewards = []
			for state, action, reward, n_state, done in samples:
				action_hot = keras.utils.to_categorical(action, S)
				state = icm_prepro(state)
				n_state = icm_prepro(n_state)
				in_reward = forward_model.predict([state.reshape(1, Sin, D, D), n_state.reshape(1, Sin, D, D), action_hot.reshape(1, S)])
				in_rewards.append(in_reward)
			in_rewards = np.array(in_rewards)

			# in_rewards = 2*(in_rewards - in_rewards.min()) / (in_rewards.max() - in_rewards.min()) -1
			# in_rewards = (in_rewards - np.mean(in_rewards)) / np.std(in_rewards)
			in_rewards = np.clip(in_rewards, -10, 10)

			for (state, action, reward, n_state, done), in_reward in zip(samples, in_rewards):
				if not done:
					n_q = model.predict(n_state.reshape(1, Sin, D, D))[0]
					n_q = filter_q(n_q, n_state)
					n_action = np.argmax(n_q)
					target_q = prev_model.predict(n_state.reshape(1, Sin, D, D))[0][n_action]
				else:
					target_q = 0
				action_hot = keras.utils.to_categorical(action, S)

				# model_layers = [d1_prepro(v[0]) for v in get_action(icm_model, [state, n_state, action_hot], ["s_0_hat", "flatten_3"])]

				q = model.predict(state.reshape(1, Sin, D, D))[0]
				q[action] += lr * (reward +gamma * target_q + in_reward - q[action])
				q = filter_q(q, state)

				# data augmentation
				for i in range(S):
					aug_q = np.concatenate((q[i:], q[:i]), axis=0)
					aug_state = np.concatenate((state[i:S], state[:i], state[S:]), axis=0)
					aug_n_state = np.concatenate((n_state[i:S], n_state[:i], n_state[S:]), axis=0)
					aug_action = np.concatenate((action_hot[i:], action_hot[:i]), axis=0)

					qs.append(aug_q)
					states.append(aug_state)
					n_states.append(aug_n_state)
					actions_hot.append(aug_action)
			
			# stack together all inputs, hidden states, action gradients, and rewards for this episode
			states = np.array(states)
			qs = np.array(qs)
			n_states = np.array(n_states)
			actions_hot = np.array(actions_hot)

			model.fit(states, qs, batch_size=32, epochs=1, verbose=2)
			icm_model.fit([states, n_states, actions_hot], np.zeros((states.shape[0])),
							  batch_size=32, epochs=1, verbose=2)
			
			# boring book-keeping
			reward_sum /= ep_batch
			running_reward = reward_sum if running_reward is None else running_reward * 0.99 + reward_sum * 0.01
			
			print('exploring percentage: ', 100*explore/ep_batch)
			print('ep %d: win rate: %f, reward total: %f, running mean: %f' % \
				(episode_number, win[0] / ep_batch, reward_sum, running_reward))
			print(win, done_rec/ep_batch)

			model.save('model.h5')
			icm_model.save('icm_model.h5')
			reward_sum = 0
			explore = 0
			win = [0, 0, 0]
			done_rec = 0
			
