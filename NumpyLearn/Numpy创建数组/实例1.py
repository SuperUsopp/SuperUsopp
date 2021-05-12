import numpy as np
# ndarray.empty(shape, dtype, order):创建一个指定形状、数据类型且未初始化的数组
x = np.empty((3, 2), dtype=np.int8)
print(x)
print(x.ndim)