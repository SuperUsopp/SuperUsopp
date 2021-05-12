import numpy as np
# ndarray.asarray(a, dtype, order):
# 1、将列表转化为数组
x = [1, 2, 3]
a = np.asarray(x)
print(a)
print(a.ndim)
print(a.flags)

# 2、将元组转化为数组
y = (4, 5, 6)
b = np.asarray(y)
print(b)
print(b.ndim)
print(b.itemsize)
print(b.flags)