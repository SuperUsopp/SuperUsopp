# -*- coding:utf-8 -*-
import os
import time
 
filename = r'D:\ShareFolder\NewFolder\0.uihlog'  # 当前路径
filemt = time.localtime(os.stat(filename).st_mtime)
print(time.strftime("%Y-%m-%d", filemt), time.strftime("%H:%M:%S", filemt))
# print(time.strftime("%H:%M:%S", filemt))
