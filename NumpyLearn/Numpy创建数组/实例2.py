import numpy as np
# numpy.zeros(shape, dtype, order):创建指定大小的数组，并用0来填充
x = np.zeros((2, 3, 4), dtype=np.int8)
print(x)
print(x.ndim)
y = np.zeros([5], dtype=np.float64)
print(y)
print(y.ndim)
z = np.zeros((2, 2), dtype=[('age', np.int8), ('score', np.float64)])
print(z)
print(z.ndim)