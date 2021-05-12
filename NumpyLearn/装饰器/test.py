# -*- coding:utf-8 -*-
from functools import wraps

def repeat(times):
    def repeat_inner(MyFunc):
        @wraps(MyFunc)
        def wrapper():
            tmp = times
            while tmp:
                print("This is wrapper func.")
                MyFunc()
                tmp -= 1
        return wrapper
    return repeat_inner


@repeat(3)
def MyFunc():
    print("This is my own func!")


if __name__ == '__main__':
    MyFunc()
    print(MyFunc.__name__)
