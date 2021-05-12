import numpy as np
# ndarray.ones(shape, dtype, order):创建指定形状的数组，并用1来填充数据元素
x = np.ones((2, 3), dtype=np.int)
print(x)
print(x.ndim)

y = np.ones(5)
print(y)
print(y.itemsize)