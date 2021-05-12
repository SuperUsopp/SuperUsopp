import numpy as np
# ndarray.frombuffer(buffer, dtype, count, offset):用于实现动态数组
s = b'Hello, world!'
a = np.frombuffer(s, dtype=np.int8)
print(a)
print(a.ndim)
print(a.itemsize)