#### MySQL用户管理

用户管理包括管理用户的账户、权限等

* 权限表介绍

  * user表（用户列、权限列、安全列和资源控制列）

  ```SQL
  -- user表的用户列包括Host、User、Password，分别表示主机名、用户名和密码。用户登录时，首先要判断这3个字段。如果这三个字段同时匹配，MySQL数据库系统才会允许其登录。
  
  -- user表的权限列包括Select_priv、Insert_priv等以priv结尾的字段。这些字段决定了用户的权限。
  
  -- user表的安全列只有4个字段，分别是ssl_type、ssl_cipher、x509_issuer和x509_subject。ssl用于加密，x509标准可以用来标识用户。
  
  -- user表的4个资源控制列是max_questions、max_updates、max_connections和max_user_connections。max_question和max_updates分别规定每小时可以允许执行多少次查询和更新；max_connections规定每小时可以建立多少连接；max_user_connections规定单个用户可以同时具有的连接数。
  ```

  

  * db表（用户列和权限列，存储了某个用户对一个数据库的权限）

  ```SQL
  -- db表的用户列有3个字段，分别是Host、Db和User，分别表示主机名、数据名和用户名
  -- db表的权限列比host表的权限列多了一个Create_routine_priv字段和Alter_routine_priv字段，这两个字段决定用户是否具有创建和修改存储过程的权限
  -- 用户先根据user表的内容获取权限，然后再根据db表的内容获取权限
  ```

  

  * host表（用户列和权限列）

  ```sql
  -- host表是db表的扩展，如果db表中找不到Host字段的值，就需要到host表中去寻找
  ```

  * tables_priv表和columns_priv表

  ```SQL
  -- tables_priv表可以对单个表进行权限设置。columns_priv表可以对单个数据列进行权限设置
  -- tables_priv表包含8个字段，分别是Host、Db、User、Table_name、Table_priv、Column_priv、Timestamp和Grantor
  -- columns_priv表包括7个字段，分别是Host、Db、User、Table_name、Column_name、Column_priv和Timestamp
  ```

  * procs_priv表

  ```SQL
  -- procs_priv表可以对存储过程和存储函数进行权限设置
  -- procs_priv表包含8个字段，分别是Host、Db、User、Routine_name、Routine_type、Proc_priv、Timestamp和Grantor
  ```

  

* 用户登录和退出MySQL服务器

```SQL
-- 示例15-1
mysql -h 192.168.0.1 -r root -p test;

-- 示例15-2
mysql -h localhost -u root -p mysql "DESC func";

-- 示例15-3
mysql -h 127.0.0.1 -u root -proot;

```



* 创建和删除普通用户

  * 使用CREATE USER语句来创建新用户

  ```SQL
  CREATE USER user [IDENTIFIED BY [PASSWORD] 'password']
  	[, user [IDENTIFIED BY [PASSWORD] 'password']]...
  	
  -- 示例15-4
  CREATE USER ‘test1'@'localhost' IDENTIFIED BY 'test1';
  ```

  

  * 直接在mysql.user表中添加用户（使用INSERT语句来新建普通用户）

  ```SQL
  INSERT INTO mysql.user(Host, User, Password) VALUES ('hostname', 'username', PASSWORD ('password'))
  
  -- 示例15-5
  INSERT INTO mysql.user(Host, User, Password, ssl_cipher, x509_issuer, x509_subject)
  	VALUES('localhost', 'test2', PASSWORD("test2"), '', '', '');
  FLUSH PRIVILEGES;
  ```

  

  * 使用GRANT语句来新建用户

  ```SQL
  GRANT priv_type ON database.table
  	TO user [IDENTIFIED BY [PASSWORD]'password']
  			[, user [IDENTIFIED BY [PASSWORD]'password']]...
  
  -- 示例15-6
  GRANT SELECT ON *.* TO 'test3'@'localhost' IDENTIFIED BY 'test3';
  ```

  * 使用DROP USER语句来删除普通用户

  ```SQL
  DROP USER user [,user]...
  
  -- 示例15-7
  DROP USER 'test2'@'localhost';
  ```

  

  * 直接在mysql.user表中删除用户（用DELETE语句来删除普通用户）

  ```SQL
  DELETE FROM mysql.user WHERE Host='hostname' AND user='username';
  
  -- 示例15-8
  DELETE FROM mysql.user WHERE host='localhost' AND user='test3';
  FLUSH PRIVILEGES;
  ```

  * root用户修改自己的密码

    * 使用mysqladmin命令来修改root用户的密码

    ```SQL
    mysqladmin -u username -p password "new_password";
    
    -- 示例15-9
    mysqladmin -u root -p password "123456789";
    ```

    

    * 修改mysql数据库下的user表

    ```SQL
    -- 示例15-10
    UPDATE mysql.user SET Password=PASSWORD("root")
    	WHERE User="root" AND Host='localhost';
    ```

    * 使用SET语句来修改root用户的密码

    ```SQL
    SET PASSWORD=PASSWORD("new_password");
    
    -- 示例15-11
    SET PASSWORD=PASSWORD("123456");
    ```

    

* 普通用户和root用户的密码管理

* 权限管理

