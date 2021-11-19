#### 索引

##### 索引的含义和特点

​		**索引**：创建在表上的，对数据库表中一列或多列的值进行排序的一种结构 (*由数据库表中一列或多列组合而成，其作用是提高对表中数据的查询速度*)

​		**最大索引数**：所有存储引擎对每个表至少支持16个索引

​		**最大索引长度**：所有存储引擎对每个表支持的索引的总索引长度至少为256字节

​		**索引的两种存储类型**：B型树（BTREE）和哈希（HASH）索引

​		**索引的优点**：提高检索数据的速度

​		**索引的缺点**：创建和维护索引需要耗费时间；索引需要占用物理空间；增加、删除和修改数据时，需要动态维护索引，造成数据的维护速度降低

##### 索引的分类

- 普通索引
- 唯一性索引
- 全文索引（MyISAM存储引擎支持）
- 单列索引
- 多列索引
- 空间索引（MyISAM存储引擎支持）

##### 如何设计索引

索引的设计原则

* 选择唯一性索引
* 为经常需要排序、分组和联合操作的字段建立索引
* 为常作为查询条件的字段建立索引
* 限制索引的数目
* 尽量使用数据量少的索引
* 尽量使用前缀来索引
* 删除不再使用或者很少使用的索引

##### 如何创建索引

* 在创建表的时候创建索引

  ```SQL
  CREATE TABLE 表名(属性名 数据类型 [完整性约束条件],
                 属性名 数据类型 [完整性约束条件],
                 ......
                 属性名 数据类型
                 [UNIQUE|FULLTEXT|SPATIAL] INDEX|KEY
                 		[别名] (属性名1 [(长度)] [ASC|DESC])
                 );
  -- 示例7-1:创建普通索引
  CREATE TABLE index1(
  	id INT,
      name VARCHAR(20),
      sex BOOLEAN,
      INDEX index1_id (id)
  );
  
  -- 使用EXPLAIN语句可以查看索引是否被使用
  EXPLAIN SELECT * FROM index1 WHERE id=1 \G
  
  -- 示例7-2：创建唯一性索引
  CREATE TABLE index2(
  	id INT UNIQUE,
      name VARCHAR(20),
      UNIQUE INDEX index2_id(id ASC)
  );
  
  -- 示例7-3：创建全文索引
  CREATE TABLE index3(
  	id INT,
      info VARCHAR(20),
      FULLTEXT INDEX index3_info (info)
  )ENGINE=MyISAM;
  
  -- 示例7-4：创建单列索引
  CREATE TABLE index4(
  	id INT,
      subject VARCHAR(30),
      INDEX index4_st(subject(10))
  );
  
  -- 示例7-5：创建多列索引
  CREATE TABLE index5(
  	id INT,
      name VARCHAR(20),
      sex CHAR(4),
      INDEX index5_ns(name, sex)
  );
  
  -- 示例7-6：创建空间索引
  CREATE TABLE index6(
  	id INT,
      space GEOMETRY NOT NULL,
      SPATIAL INDEX index6_sp(space)
  )ENGINE=MyISAM;
  ```

  

* 在已经存在的表上创建索引

  ```sql
  CREATE [UNIQUE|FULLTEXT|SPATIAL] INDEX 索引名
  ON 表名 (属性名[(长度)] [ASC|DESC]);
  
  -- 示例7-7：创建普通索引
  CREATE INDEX index7_id ON example0 (id);
  
  -- 示例7-8：创建唯一性索引
  CREATE UNIQUE INDEX index8_id ON index8(course_id);
  
  -- 示例7-9：创建全文索引
  CREATE FULLTEXT INDEX index9_info ON index9(info);
  
  -- 示例7-10：创建单列索引
  CREATE INDEX index10_addr ON index10(address(4));
  
  -- 示例7-11：创建多列索引
  CREATE INDEX index11_na ON index11(name, address);
  
  -- 示例7-12：创建空间索引
  CREATE SPATIAL INDEX index12_line ON index12(line);
  ```

  

* 使用ALTER TABLE语句来创建索引

  ```SQL
  ALTER TABLE 表名 ADD [UNIQUE|FULLTEXT|SPATIAL] INDEX
  					索引名(属性名[(长度)] [ASC|DESC]);
  					
  -- 示例7-13：创建普通索引
  ALTER TABLE example0 ADD INDEX index13_name(name(20));
  
  -- 示例7-14：创建唯一性索引
  ALTER TABLE index1:4 ADD UNIQUE INDEX index14_id(course_id);
  
  -- 示例7-15：创建全文索引
  ALTER TABLE index15 ADD FULLTEXT INDEX index15_info(info);
  
  -- 示例7-16：创建单列索引
  ALTER TABLE index16 ADD INDEX index16_addr(address(4));
  
  -- 示例7-17：创建多列索引
  ALTER TABLE index17 ADD INDEX index17_na(name, address);
  
  -- 示例7-18：创建空间索引
  ALTER TABLE index18 ADD SPATIAL INDEX index18_line(line);
  ```

  

##### 如何删除索引

*一些不再使用的索引会降低表的更新速度，影响数据库的性能，这种索引应删除*

```sql
DROP INDEX 索引名 ON 表名;

-- 示例7-19
DROP INDEX index1_id ON index1;
```



##### 本章实例

user表

| 字段名   | 字段描述 | 数据类型    | 主键 | 外键 | 非空 | 唯一 | 自增 |
| -------- | -------- | ----------- | ---- | ---- | ---- | ---- | ---- |
| userid   | 编号     | INT(10)     | 是   | 否   | 是   | 是   | 是   |
| username | 用户名   | VARCHAR(20) | 否   | 否   | 是   | 否   | 否   |
| passwd   | 密码     | VARCHAR(20) | 否   | 否   | 是   | 否   | 否   |
| info     | 附加信息 | TEXT        | 否   | 否   | 否   | 否   | 否   |

information表

| 字段名   | 字段描述 | 数据类型    | 主键 | 外键 | 非空 | 唯一 | 自增 |
| -------- | -------- | ----------- | ---- | ---- | ---- | ---- | ---- |
| id       | 编号     | INT(10)     | 是   | 否   | 是   | 是   | 是   |
| name     | 姓名     | VARCHAR(20) | 否   | 否   | 是   | 否   | 否   |
| sex      | 性别     | VARCHAR(4)  | 否   | 否   | 是   | 否   | 否   |
| birthday | 出生日志 | DATE        | 否   | 否   | 否   | 否   | 否   |
| address  | 家庭住址 | VARCHAR(50) | 否   | 否   | 否   | 否   | 否   |
| tel      | 电话号码 | VARCHAR(20) | 否   | 否   | 否   | 否   | 否   |
| pic      | 照片     | BLOB        | 否   | 否   | 否   | 否   | 否   |



```SQL
-- 创建job数据库
CREATE DATABASE job;

-- 创建user表（存储引擎为MyISAM类型，创建表的时候同时创建几个索引，在userid字段上创建名为index_uid的唯一性索引，且以降序的形式排列；在username和passwd字段上创建名为index_user的多列索引；在info字段上创建名为index_info的全文索引）
USE job;
DROP TABLE IF EXISTS user;
CREATE TABLE user(
	userid INT(10) NOT NULL UNIQUE PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(20) NOT NULL,
    passwd VARCHAR(20) NOT NULL,
    info TEXT,
    UNIQUE INDEX index_uid (userid DESC),
    INDEX index_user(username, passwd),
    FULLTEXT INDEX index_info(info)
)ENGINE=MyISAM;

-- 创建information表
DROP TABLE IF EXISTS information;
CREATE TABLE information(
	id INT(10) NOT NULL UNIQUE PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(20) NOT NULL,
    sex VARCHAR(4) NOT NULL,
    birthday DATE,
    address VARCHAR(50),
    tel VARCHAR(20),
    pic BLOB
);

-- 在name字段创建名为index_name的单列索引，索引长度为10
CREATE INDEX index_name ON information(name(10));

-- 在birthday和address字段上创建名为index_bir的多列索引，然后判断索引的使用情况
ALTER TABLE information ADD INDEX index_bir(birthday, address);
SHOW CREATE TABLE information \G

-- 用ALTER TABLE语句在id字段上创建名为index_id的唯一性索引，而且以升序排列
ALTER TABLE information ADD UNIQUE INDEX index_id(id ASC);

-- 删除user表上的index_user索引
DROP INDEX index_user ON user;

-- 删除information表上的index_name索引
DROP INDEX index_name ON information;
```



##### 上机实践

workinfo表

| 字段名   | 字段描述 | 数据类型    | 主键 | 外键 | 非空 | 唯一 | 自增 |
| -------- | -------- | ----------- | ---- | ---- | ---- | ---- | ---- |
| id       | 编号     | INT(10)     | 是   | 否   | 是   | 是   | 是   |
| name     | 职位名称 | VARCHAR(20) | 否   | 否   | 是   | 否   | 否   |
| type     | 职位类型 | VARCHAR(20) | 否   | 否   | 否   | 否   | 否   |
| address  | 工作地址 | VARCHAR(50) | 否   | 否   | 否   | 否   | 否   |
| wages    | 工资     | INT         | 否   | 否   | 否   | 否   | 否   |
| contents | 工作内容 | TINYTEXT    | 否   | 否   | 否   | 否   | 否   |
| extra    | 附加信息 | TEXT        | 否   | 否   | 否   | 否   | 否   |

```SQL
-- 在数据库job下创建workinfo表，同时在id字段上创建名为index_id的唯一性索引，以降序个格式排序
USE job;
DROP TABLE IF EXISTS workinfo;
CREATE TABLE workinfo(
	id INT(10) NOT NULL UNIQUE PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(20) NOT NULL,
    type VARCHAR(20),
    address VARCHAR(50),
    wages INT,
    contents TINYTEXT,
    extra TEXT,
    UNIQUE INDEX index_id (id DESC)
);

-- 使用CREATE INDEX语句为name字段创建长度为10的索引index_name
CREATE INDEX index_name ON workinfo(name(10));

-- 使用ALTER TABLE语句在type和address上创建名为index_t的索引
ALTER TABLE workinfo ADD INDEX index_t (type, address);

-- 将workinfo表的存储引擎改为MyISAM类型
ALTER TABLE workinfo ENGINE=MyISAM;

-- 使用ALTER TABLE语句在extra字段上创建名为index_ext的全文索引
ALTER TABLE workinfo ADD FULLTEXT INDEX index_ext (extra);

-- 删除workinfo表的唯一性索引index_id
DROP INDEX index_id ON workinfo;
```



##### 练习

work表

| 字段名  | 字段描述 | 数据类型    | 主键 | 外键 | 非空 | 唯一 | 自增 |
| ------- | -------- | ----------- | ---- | ---- | ---- | ---- | ---- |
| id      | 编号     | INT(10)     | 是   | 否   | 是   | 是   | 是   |
| name    | 姓名     | VARCHAR(20) | 否   | 否   | 是   | 否   | 否   |
| address | 工作地址 | VARCHAR(50) | 否   | 否   | 否   | 否   | 否   |
| info    | 备注信息 | TEXT        | 否   | 否   | 否   | 否   | 否   |

```SQL
-- 在id字段上创建名为work_id的唯一性索引
DROP TABLE IF EXISTS work;
CREATE TABLE work(
	id INT(10) NOT NULL UNIQUE PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(20) NOT NULL,
    address VARCHAR(50),
    info TEXT,
    UNIQUE INDEX work_id(id)
);

-- 在name和address字段上创建名为work_na的多列索引，且name字段的索引长度为10
CREATE INDEX index_na ON work (name(10), address);

-- 在info字段上创建名为work_info的全文索引
ALTER TABLE work ENGINE=MyISAM;
ALTER TABLE work ADD FULLTEXT INDEX work_info(info);

-- 使用DROP INDEX语句删除所有索引
DROP INDEX work_id ON work;
DROP INDEX index_na ON work;
DROP INDEX work_info ON work;
```

