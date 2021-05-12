import numpy as np

# 将结构化数据类型应用于ndarray对象
dt = np.dtype([('age', np.int8)])
a = np.array([(10,), (20,), (30,)], dtype=dt)
print(type(a[0]))
