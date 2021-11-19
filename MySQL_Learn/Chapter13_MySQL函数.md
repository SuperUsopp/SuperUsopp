#### MySQL函数

MySQL函数是MySQL数据库提供的内部函数，这些内部函数可以帮助用户更加方便的处理表中的数据。

* 数学函数

  ![MySQL的数学函数](.\images\MySQL的数学函数.png)

* 字符串函数

  ![MySQL的字符串函数](.\images\MySQL的字符串函数.png)

* 日期和时间函数

  | 函数                                                         | 作用                                   |
  | ------------------------------------------------------------ | -------------------------------------- |
  | CURDATE(), CURRENT_DATE()                                    | 返回当前日期                           |
  | CURTIME(), CURRENT_TIME()                                    | 返回当前时间                           |
  | NOW(), CURRENT_TIMESTAMP(), LOCALTIME(), SYSDATE(), LOCALTIMESTAMP() | 返回当前日期和时间                     |
  | UNIX_TIMESTAMP()                                             | 以UNIX时间戳的形式返回当前时间         |
  | UNIX_TIMESTAMP(d)                                            | 将时间d以UNIX时间戳的形式返回          |
  | FROM_UNIXTIME(d)                                             | 把UNIX时间戳的时间转换为普通格式的时间 |

  

* 条件判断函数

  * IF(expr, v1, v2)		：如果表达式expr成立，则返回v1，否则返回v2
  * IFNULL(v1, v2)         :  如果v1的不为空，就显示v1的值，否则显示v2的值
  * CASE函数
    * CASE WHEN expr1 THEN v1[WHEN expr2 THEN v2...] [ELSE vn] END
    * CASE expr WHEN e1 THEN v1[WHEN e2 THEN v2...] [ELSE vn] END

* 系统信息函数

  ![MySQL的系统信息函数](D:\Local_Repo\MySQL_Learn\images\MySQL的系统信息函数.png)

* 加密函数

  * PASSWORD('str')
  * MD5(str)
  * ENCODE(str, pswd_str)
  * DECODE(crypt_str, pswd_str)

* 格式化函数

  * 格式化函数
    * FORMAT(x, n)
  * 不同进制的数字进行转换
    * ASCII(s)
    * BIN(x)
    * HEX(x)
    * OCT(x)
    * CONV(x, f1, f2)
  * IP地址与数字相互转换
    * INET_ATON(IP)
    * INET_NTOA(n)
  * 加锁函数和解锁函数
    * GET_LOCK(name, time)
    * RELEASE_LOCK(name)
    * IS_FREE_LOCK(name)
  * 重复执行指定操作
    * BENCHMARK(count, expr)
  * 改变字符集
    * CONVERT(s USING cs)
  * 改变字段数据类型
    * CAST(x AS type)
    * CONVERT(x, type)

