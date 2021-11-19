#### 配置MySQL

* 启动MySQL服务

  **控制面板**→**管理工具**→**服务**→**MySQL**

* 登录MySQL数据库

  **开始**→**运行**→**cmd**→**mysql -h hostname -u root -p**

* 配置PATH变量

* 更改MySQL的配置

  * 通过设置向导来更改配置

    **开始**→**所有程序**→**MySQL**→**M有SQL Server 5.1**→**MySQL Server Instance Config Wizard**

  * 手工更改配置
    * my.ini：MySQL数据库中使用的配置文件
    * my-huge.ini：适合超大型数据库的配置文件
    * my-large.ini：适合大型数据库的配置文件
    * my-medium.ini：适合中型数据库的配置文件
    * my-small.ini：适合小型数据库的配置文件
    * my-template.ini：配置文件的模板(MySQL配置向导将该配置文件中选择项写入到my.ini文件中)
    * my-innodb-heavy-4G.ini：配置文件只对于InnoDB存储引擎有效，而且服务器的内存不能小于4GB