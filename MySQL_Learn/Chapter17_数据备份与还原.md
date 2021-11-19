#### MySQL日志

* 日志的定义、作用和优缺点

  ```SQL
  -- 日志文件中记录着MySQL数据库运行期间发生的变化
  -- MySQL日志是用来记录MySQL数据库的客户端连接情况、SQL语句的执行情况和错误信息等
  ```

  

* **二进制日志**（以二进制文件的形式记录了数据库中的操作，但不记录查询语句）

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

  * 暂时停止二进制日志功能

    ```SQL
    -- 1、删除my.ini配置文件中的log-bin选项可停止二进制日志的记录
    -- 2、暂停二进制日志记录功能
    SET SQL_LOG_BIN=0;
    -- 3、重新打开二进制日志记录功能
    SET SQL_LOG_BIN=1;
    ```

    

* **错误日志**（记录MySQL服务器的启动、关闭和运行错误等信息）

  * 启动和设置错误日志

    ```SQL
    -- 在MySQL数据库中，错误日志功能是默认开启的，且错误日志无法被禁止，默认情况下，错误日志存储在MySQL数据库的数据文件夹下
    -- 错误日志的存储位置可以通过log-error选项来设置（将log-error加入到my.ini文件）
    [mysqld]
    log-error=DIR/[filename]
    ```

    

  * 查看错误日志（可以直接查看）

  * 删除错误日志

    ```SQL
    -- 1、使用mysqladmin来开启新错误日志
    -- 以下命令开启新的错误日志，旧的错误日志更名为filename.err-old
    mysqladmin -u root -p flush-logs
    
    -- 2、使用FLUSH LOGS语句来开启新的错误日志
    ```

    

* **通用查询日志**（记录用户登录和记录查询的信息）

  * 启动和设置通用查询日志

    ```SQL
    -- 默认情况下，通用查询日志功能是关闭的，通过my.ini文件的log选项可以开启通用查询日志
    [mysqld]
    log[=DIR\[filename]
    ```

    

  * 查看通用查询日志（文本文件，可以直接打开）

  * 删除通用查询日志

    ```SQL
    -- 使用mysqladmin命令来开启新的通用查询日志，新的通用查询日志会直接覆盖旧的查询日志，不需要手动删除
    mysqladmin -u root -p flush-logs
    ```

    

* **慢查询日志**（记录执行时间超过执行时间的操作，通过慢查询日志，可以查找出哪些查询语句的执行效率很低，以便进行优化）

  * 启动和设置慢查询日志

    ```SQL
    -- 默认情况下，慢查询日志功能是关闭的，修改my.ini文件的log-slow-queries选项可以开启慢查询日志
    [mysqld]
    log-slow-queries[=DIR\[filename]]
    long_query_time
    
    ```

    

  * 查看慢查询日志（文本文件，可以直接打开）

  * 删除慢查询日志

    ```SQL
    -- 慢查询日志删除方法通通用查询日志删除方法
    mysqladmin -u root -p flush-logs
    ```

    

