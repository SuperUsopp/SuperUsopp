import numpy as np

# numpy.ndim:秩，即轴的数量或维度的数量
a = np.arange(24)
print(a)
print(a.ndim)
print(a.reshape(2, 4, 3))
b = a.reshape(2, 4, 3)
print(a)
print(b)
print(b.ndim)