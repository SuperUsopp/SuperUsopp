import numpy as np
# ndarray.fromiter(iterable, dtype, count):从迭代对象中创建数组，返回一维数组
list = range(10)
a = iter(list)
print(a)
b = np.fromiter(a, dtype=np.float64)
print(b)
print(b.itemsize)
print(b.ndim)
print(b.flags)
