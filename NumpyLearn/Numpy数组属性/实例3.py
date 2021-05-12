import numpy as np
# 调整数组大小
a = np.array([[1, 2, 3], [4, 5, 6]])
print(a)
print(a.ndim)
a.shape = (3, 2)
b = a.reshape(3, 2)
print(b)
print(a)