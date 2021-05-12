import numpy as np
# ndarray.itemsize:以字节形式返回数组中每一个元素的大小
x = np.array([1, 2, 3, 4, 5], dtype=np.int8)
print(x)
print(x.itemsize)

y = np.array([1, 2, 3, 4, 5], dtype=np.float64)
print(y)
print(y.itemsize)