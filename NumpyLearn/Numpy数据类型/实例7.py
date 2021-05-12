import numpy as np

# 定义一个结构化数据类型Student
student = np.dtype([('name', 'S20'), ('age', 'i1'), ('marks', 'f4')])
print(student)
