import numpy as np

# 将实例7定义的结构化数据类型应用于ndarray对象
student = np.dtype([('name', 'S20'), ('age', 'i1'), ('marks', 'f4')])
a = np.array([("xiaoming", 12, 88), ("xiaomei", 11, 96)], dtype=student)
print(a[0])
print(type(a[0][0]))
