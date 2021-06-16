from win32file import CreateFile, SetFileTime, GetFileTime, CloseHandle
from win32file import GENERIC_READ, GENERIC_WRITE, OPEN_EXISTING
from pywintypes import Time  # 可以忽视这个 Time 报错（运行程序还是没问题的）
import time

import os
import time

 
def getFileCreateTime(filename):
    filemt = time.localtime(os.stat(filename).st_mtime)
    return (time.strftime("%Y-%m-%d", filemt), time.strftime("%H:%M:%S", filemt))


def modifyFileTime(filePath, createTime, modifyTime, accessTime, offset):
    """
    用来修改任意文件的相关时间属性，时间格式：YYYY-MM-DD HH:MM:SS 例如：2019-02-02 00:01:02
    :param filePath: 文件路径名
    :param createTime: 创建时间
    :param modifyTime: 修改时间
    :param accessTime: 访问时间
    :param offset: 时间偏移的秒数,tuple格式，顺序和参数时间对应
    """
    try:
        format = "%Y-%m-%d %H:%M:%S"  # 时间格式
        cTime_t = timeOffsetAndStruct(createTime, format, offset[0])
        mTime_t = timeOffsetAndStruct(modifyTime, format, offset[1])
        aTime_t = timeOffsetAndStruct(accessTime, format, offset[2])

        fh = CreateFile(filePath, GENERIC_READ | GENERIC_WRITE, 0, None, OPEN_EXISTING, 0, 0)
        createTimes, accessTimes, modifyTimes = GetFileTime(fh)

        createTimes = Time(time.mktime(cTime_t))
        accessTimes = Time(time.mktime(aTime_t))
        modifyTimes = Time(time.mktime(mTime_t))
        SetFileTime(fh, createTimes, accessTimes, modifyTimes)
        CloseHandle(fh)
        return 0
    except:
        return 1


def timeOffsetAndStruct(times, format, offset):
    return time.localtime(time.mktime(time.strptime(times, format)) + offset)




if __name__ == '__main__':
    # # 需要自己配置
    # path = r"C:\Users\zhao.li\Desktop\2021-03-22"
    # cTime = "2020-09-08 18:51:02"  # 创建时间
    # mTime = cTime
    # aTime = cTime
    # # mTime = "2019-02-02 00:01:03"  # 修改时间
    # # aTime = "2019-02-02 00:01:04"  # 访问时间
    # fName = r"C:\Users\zhao.li\Desktop\Test\新建文件夹"  # 文件路径，文件存在才能成功（可以写绝对路径，也可以写相对路径）

    # offset = (0, 1, 2)  # 偏移的秒数（不知道干啥的）

    # # 调用函数修改文件创建时间，并判断是否修改成功
    # r = modifyFileTime(fName, cTime, mTime, aTime, offset)
    # if r == 0:
    #     print('修改完成')
    # elif r == 1:
    #     print('修改失败')


    path = r"C:\Users\zhao.li\Desktop\2021-03-22"
    for root, dir, files in os.walk(path):
        for file in files:
            full_path = os.path.join(root, file)
            mtime = os.stat(full_path).st_mtime
            file_modify_date = time.strftime('%Y-%m-%d', time.localtime(mtime))
            file_modify_time = time.strftime('%H:%M:%S', time.localtime(mtime))
            if file_modify_date == "2021-03-13":
                cTime = "2020-09-01 " + str(file_modify_time)  # 创建时间
                mTime = cTime
                aTime = cTime
                offset = (0, 1, 2)
                modifyFileTime(full_path, cTime, mTime, aTime, offset)
