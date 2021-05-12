import numpy as np

# 类型字段名可以用于存取实际的'age'列
dt = np.dtype([('age', np.int8)])
a = np.array([(10,), (20,), (30,)], dtype=dt)
print(a['age'][0])
