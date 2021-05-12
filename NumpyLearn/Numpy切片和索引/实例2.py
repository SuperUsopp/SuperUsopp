import numpy as np
x = np.array([[1, 2, 3], [3, 4, 5], [4, 5, 6]])
print(x[..., 1])
print(x[1, ...])
print(x[..., 1 : 2])