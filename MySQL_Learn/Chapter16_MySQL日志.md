#### MySQL日志

* 日志的定义、作用和优缺点

* **二进制日志**（已二进制文件的形式记录了数据库中的操作，但不记录查询语句）

  * 启动和设置二进制日志

    ```SQL
    # 修改my.cnf(Linux操作系统下)/my.ini(Windows操作系统下)
    [mysqld]
    log-bin [=DIR\[filename]]
    ```

  * 查看二进制日志

    ```SQL
    mysqlbinlog filename.number
    ```

  * 删除二进制日志

    ```SQL
    -- 1、删除所有二进制日志
    RESET MASTER;
    
    -- 2、根据编号来删除二进制日志
    PURGE MASTER LOGS TO 'filename.number';
    
    -- 3、根据创建时间来删除二进制日志
    PURGE MASTER LOGS TO 'yyyy-mm-dd hh:MM:ss';
    ```

  * 使用二进制日志还原数据库

    ```SQL
    mysqlbinlog filename.number | mysql -u root -p;
    ```

    

* **错误日志**（记录MySQL服务器的启动、关闭和运行错误等信息）

* **通用查询日志**（记录用户登录和记录查询的信息）

* **慢查询日志**（记录执行时间超过执行时间的操作）

* 日志管理

