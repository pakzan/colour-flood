from numpy.random import randint, seed
import cv2
import numpy as np

GRID_SIZE = 10
STATE = 6
class ColourFlood():

    def __init__(self, grid_size, total_state, render=False):
        self.grid_size = grid_size
        self.total_state = total_state

        if render:
            self.img = np.zeros((200, 200, 3), np.uint8)
            self.colors = [(239, 83, 80), (236,64,122), (171,71,188),(126,87,194),(66,165,245),(102,187,106)]
            self.label_len = int(200 / self.grid_size)

    def render(self):
        for j in range(self.grid_size):
            for i in range(self.grid_size):
                cv2.rectangle(self.img, (self.label_len*i, self.label_len*j), (self.label_len *
                                                                               (i + 1), self.label_len * (j + 1)), self.colors[self.grid[j][i]], cv2.FILLED)
        cv2.imshow('colour flood', self.img)
        cv2.waitKey(10)
          
    def reset(self):
        # self.grid = [[randint(0, self.total_state - 1) for _ in range(self.grid_size)] for _ in range(self.grid_size)]
        
        # seed(1)
        self.grid = randint(self.total_state, size=(self.grid_size, self.grid_size))
        # # include fairness
        self.grid[0][0], self.grid[-1][-1] = np.random.choice(self.total_state, 2, replace=False)
        self.grid[0][1] = (self.grid[0][0] + randint(1, self.total_state-1))%self.total_state
        self.grid[1][0] = (self.grid[0][0] + randint(1, self.total_state-1))%self.total_state
        self.grid[-1][-2] = (self.grid[-1][-1] + randint(1, self.total_state-1))%self.total_state
        self.grid[-2][-1] = (self.grid[-1][-1] + randint(1, self.total_state-1))%self.total_state

        self.comp1 = {(0, 0)}
        self.comp2 = {(self.grid_size - 1, self.grid_size - 1)}
        self.comp1_branch = {(0,0)}
        self.comp2_branch = {(self.grid_size - 1, self.grid_size - 1)}
        self.neutral = set()
        for j in range(self.grid_size):
            for i in range(self.grid_size):
                self.neutral.add((i, j))
        self.neutral -= self.comp1_branch | self.comp2_branch
        seed()
        return self.grid

    def step(self, state, isPlayer=True):
        if isPlayer:
            return self.consume(self.comp1, self.comp1_branch, state)
        else:
            return self.consume(self.comp2, self.comp2_branch, state)

    def consume(self, player, branch, state):
        queue = list(branch)
        prev_blk = len(player)
        while queue:
            x, y = queue[-1]
            queue.pop()

            waiting_list = set()
            if y-1 >= 0 and self.grid[y - 1][x] == state and (x, y-1) in self.neutral:
                waiting_list.add((x, y - 1))
            if x-1 >= 0 and self.grid[y][x - 1] == state and (x-1, y) in self.neutral:
                waiting_list.add((x-1, y))
            if y+1 < self.grid_size and self.grid[y + 1][x] == state and (x, y+1) in self.neutral:
                waiting_list.add((x, y+1))
            if x+1 < self.grid_size and self.grid[y][x + 1] == state and (x+1, y) in self.neutral:
                waiting_list.add((x + 1, y))
            queue.extend(list(waiting_list))
            self.neutral -= waiting_list
            branch |= waiting_list
            player |= waiting_list

        waiting_list = set()
        for x, y in branch:
            if len(self.neutral.intersection({(x, y - 1), (x - 1, y), (x, y + 1), (x + 1, y)})) == 0:
                waiting_list.add((x, y))
        branch -= waiting_list
        for x, y in player:
            self.grid[y][x]= state

        cur_blk = len(player)
        reward = 0
        # reward = 10*(cur_blk - prev_blk) / (self.grid_size ** 2)
        # reward = cur_blk - prev_blk
        if self.neutral:
            return self.grid, reward, False
        else:
            if len(player) > self.grid_size**2 // 2:
                reward += -self.grid_size**2 + 2*len(player)
            elif len(player) < self.grid_size**2 // 2:
                reward += -self.grid_size**2 + 2*len(player)
            else:
                reward += 0
            # reward = 0
            return self.grid, reward, True

    def getState(self, isPlayer=True):
        # return opponent's state to avoid collision
        return self.grid[0][0], self.grid[-1][-1]

    def getTeri(self, isPlayer=True):
        return self.comp1, self.comp2


if __name__ == "__main__":
    cf = ColourFlood(GRID_SIZE, STATE)
    cur_x = cf.reset()
    done = False
    while not done:
        action = randint(0, STATE-1)
        cur_x, reward, done = cf.step(action)
        action = randint(0, STATE-1)
        _, _, done = cf.step(action, False)
