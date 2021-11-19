#### 数据库概述

* 数据库技术构成

  数据库技术的出现是为了更加有效地管理和存取大量的数据资源。简单地讲，数据库技术主要包括数据库系统、SQL语言和数据库访问技术等。

  * 数据库系统
    * 数据库：存储数据的地方
    * 数据库管理系统（DBMS: DataBase Management System）：用来定义数据、管理和维护数据的软件
    * 应用系统：需要使用数据库的软件
    * 应用开发工具：开发应用系统
    * 数据库管理员和用户（用户一般不直接与数据库接触，而是通过应用系统来使用数据）
  * SQL语言（Structured Query Language，结构化查询语言）
    * 数据定义语言（DDL: Data Definition Language)：用于定义数据库、表、视图、索引和触发器等
      * CREATE：用于创建数据库、表和创建视图等
      * ALTER：用于修改表的定义、修改视图的定义等
      * DROP：用于删除数据库、删除表和删除视图等
    * 数据操作语言（DML: Data Manipulation Language）：用于插入数据、查询数据、更新数据和删除数据
      * INSERT：用于插入数据
      * SELECT：用于查询数据
      * UPDATE：用于更新数据
      * DELETE：用于删除数据
    * 数据控制语言（DCL: Data Control Language)：用于控制用户的访问权限
      * GRANT：用于给用户增加权限
      * REVOKE：用于收回用户权限

* MySQL数据库的优势

  * MySQL是开放源代码的数据库
  * MySQL的跨平台性
  * 价格优势
  * 功能强大且使用方便