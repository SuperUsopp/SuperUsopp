#### 创建、修改和删除表

###### 创建表

```SQL
CREATE TABLE 表名 (属性名 数据类型 [完整性约束条件],
                  属性名 数据类型 [完整性约束条件],
                  .
                  .
                  .
                  属性名 数据类型
                  );
-- 示例6-1
CREATE TABLE example0(
	id INT,
    name VARCHAR(20),
    sex BOOLEAN
);
```



###### 表的完整性约束条件

```sql
-- 完整性约束条件：
  -- * PRIMARY KEY：主键约束
  -- * FOREIGN KEY：外键约束
  -- * NOT NULL：非空约束
  -- * UNIQUE：值唯一约束
  -- * AUTO_INCREMENT：值自增约束 （MySQL特色）
  -- * DEFAULT：默认值约束

-- *PRIMARY KEY约束*
-- 单字段主键
-- 示例6-2
CREATE TABLE example1(
	id INT PRIMARY KEY,
    name VARCHAR(20),
    sex BOOLEAN
);

-- 多字段主键
-- 示例6-3
CREATE TABLE example2(
	stu_id INT,
    course_id INT,
    grade FLOAT,
    PRIMARY KEY(stu_id, course_id)
);

-- *FOREIGN KEY约束*
-- 设置外键的原则是依赖于数据库中已存在的父表的主键，外键可以为空
-- 外键的作用是建立该表与其父表的关联关系
-- 示例6-4
CREATE TABLE example3(
	id INT,
    stu_id INT,
    course_id INT,
    CONSTRAINT c_fk FOREIGN KEY(stu_id, course_id)
    REFERENCES example2(stu_id, course_id)
);

-- *NOT NULL约束*
-- 示例6-5
CREATE TABLE example4(
	id INT NOT NULL PRIMARY KEY,
	name VARCHAR(20) NOT NULL,
    stu_id INT,
    CONSTRAINT d_fk FOREIGN KEY(stu_id)
    REFERENCES example1(id)
);

-- *UNIQUE约束*
-- 示例6-5
CREATE TABLE example5(
	id INT PRIMARY KEY,
    stu_id INT UNIQUE,
    name VARCHAR(20) NOT NULL
);

-- *AUTO_INCREMENT约束*
-- 示例6-7
CREATE TABLE example6(
	id INT PRIMARY KEY AUTO_INCREMENT,
    stu_id INT UNIQUE,
    name VARCHAR(20) NOT NULL
);

-- *DEFAULT约束*
-- 示例6-8
CREATE TABLE example7(
	id INT PRIMARY KEY AUTO_INCREMENT,
    stu_id INT UNIQUE,
    name VARCHAR(20) NOT NULL,
    English VARCHAR(20) DEFAULT 'zero',
    Math FLOAT DEFAULT 0,
    Computer FLOAT DEFAULT 0
);
```



###### 查看表结构的方法

```sql
-- 查看表结构指查看数据库中已存在的表的定义（包括表的字段名、字段的数据类型和完整性约束条件等）
-- *DESCRIBE*
-- 示例6-9
DESCRIBE example1;

-- 示例6-10
DESC example1;

-- *SHOW CREATE TABLE*
-- 示例6-11
SHOW CREATE TABLE example1;
```



###### 修改表

```sql
-- 修改表包括修改表名、字段数据类型、字段名，增加字段，删除字段，修改字段的排列位置，更改默认存储引擎和删除表的外键约束等
-- 修改表名
ALTER TABLE 表名 RENAME [TO] 新表名;
-- 示例6-12
ALTER TABLE example0 RENAME user;

-- 修改字段的数据类型
ALTER TABLE 表名 MODIFY 属性名 数据类型;
-- 示例6-13
ALTER TABLE user_ MODIFY name VARCHAR(30);

-- 修改字段名
ALTER TABLE 表名 CHANGE 旧属性名 新属性名 新数据类型
-- 示例6-14
ALTER TABLE example1 CHANGE name stu_name VARCHAR(20);
-- 示例6-15
ALTER TABLE example1 CHANGE sex stu_sex INT(2);

-- 增加字段
ALTER TABLE 表名 ADD 属性名1 数据类型 [完整性约束条件] [FIRST|AFTER 属性名2]
-- 示例6-16
ALTER TABLE user_ ADD phone VARCHAR(20);
-- 示例6-17
ALTER TABLE user_ ADD age INT(4) NOT NULL;
-- 示例6-18
ALTER TABLE user_ ADD num INT(4) PRIMARY KEY FIRST;
-- 示例6-19
ALTER TABLE user_ ADD address VARCHAR(30) NOT NULL AFTER phone;

-- 删除字段
ALTER TABLE 表名 DROP 属性名;
-- 示例6-20
ALTER TABLE user_ DROP id;

-- 修改字段的排列位置
ALTER TABLE 表名 MODIFY 属性名1 数据类型 FIRST|AFTER 属性名2;
-- 示例6-21
ALTER TABLE user_ MODIFY name VARCHAR(30) FIRST;
-- 示例6-22
ALTER TABLE user_ MODIFY sex TINYINT(1) AFTER age;

-- 更改表的存储引擎
ALTER TABLE 表名 ENGINE=存储引擎名;
-- 示例6-23
ALTER TABLE user_ ENGINE=MyISAM;

-- 删除表的外键约束
ALTER TABLE 表名 DROP FROEIGN KEY 外键别名;
-- 示例6-24
ALTER TABLE example3 DROP FOREIGN KEY c_fk;
```



###### 删除表

```SQL
-- 删除没有被关联的普通表
DROP TABLE 表名;
-- 示例6-25
DROP TABLE example5;

-- 当删除不存在的表时，系统会报错
ERROR 1146 (42S02): Table 'mysql.example5' doesn't exist

-- 删除被其他表关联的父表
ERROR 1217 (23000): Cannot delete or update a parent row: a foreign key constraint fails
-- 两种方法：1、先删除子表，然后再删除父表；2、先删除子表的外键约束，再删除父表

-- 查询某表被哪些表依赖，可以使用如下语句
SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE REFERENCED_TABLE_NAME="被引用表名"


```

###### 本章实例

```SQL
USE example;

CREATE TABLE student(
	num INT(10) NOT NULL UNIQUE PRIMARY KEY,
    name VARCHAR(20) NOT NULL,
    sex VARCHAR(4) NOT NULL,
    birthday DATETIME,
    address VARCHAR(50)
);

CREATE TABLE grade(
	id INT(10) NOT NULL PRIMARY KEY UNIQUE AUTO_INCREMENT,
    course VARCHAR(10) NOT NULL,
    s_num INT(10) NOT NULL,
    grade VARCHAR(4),
    CONSTRAINT num_fk FOREIGN KEY(s_num)
    REFERENCEs student(num)
);

-- 1、将grade表的course字段的数据类型改为VARCHAR(20)
ALTER TABLE grade MODIFY course VARCHAR(20);
-- 2、将s_num字段的位置改到course字段的前面
ALTER TABLE grade MODIFY s_num INT(10) AFTER id;
-- 3、将grade字段改名为score
ALTER TABLE grade CHANGE grade score VARCHAR(4); 
-- 4、删除grade表的外键约束
ALTER TABLE grade DROP FOREIGN KEY num_fk;
-- 5、将grade表的存储引擎更改为MyISAM类型
ALTER TABLE grade ENGINE=MyISAM;
-- 6、将student表的address字段删除
ALTER TABLE student DROP address;
-- 7、在student表中增加名为phone的字段，数据类型为INT(10)
ALTER TABLE student ADD phone INT(10);
-- 8、将grade表改名为gradeInfo
ALTER TABLE grade RENAME TO gradeInfo;
-- 9、删除student表
DROP TABLE student;
```



###### 上机操作

* 操作teacher表

| 字段名   | 字段描述 | 数据类型    | 主键 | 外键 | 非空 | 唯一 | 自增 |
| -------- | -------- | ----------- | ---- | ---- | ---- | ---- | ---- |
| id       | 编号     | INT(4)      | 是   | 否   | 是   | 是   | 是   |
| num      | 教工号   | INT(10)     | 否   | 否   | 是   | 是   | 否   |
| name     | 姓名     | VARCHAR(20) | 否   | 否   | 是   | 否   | 否   |
| sex      | 性别     | VARCHAR(4)  | 否   | 否   | 是   | 否   | 否   |
| birthday | 出生日期 | DATETIME    | 否   | 否   | 否   | 否   | 否   |
| address  | 家庭住址 | VARCHAR(50) | 否   | 否   | 否   | 否   | 否   |

```SQL
CREATE DATABASE school;

use school;

CREATE TABLE teacher(
	id INT(4) NOT NULL UNIQUE PRIMARY KEY AUTO_INCREMENT,
    num INT(10) NOT NULL UNIQUE,
    name VARCHAR(20) NOT NULL,
    sex VARCHAR(4) NOT NULL,
    birthday DATETIME,
    address VARCHAR(50)
);

-- 1、将teacher表的 name字段的数据类型改为VARCHAR(30)
ALTER TABLE teacher MODIFY name VARCHAR(30) NOT NULL;

-- 2、将birthday字段的位置改到sex字段的前面
ALTER TABLE teacher MODIFY birthday DATETIME after name;

-- 3、将num字段改名为t_id
ALTER TABLE teacher CHANGE num t_id INT(10) NOT NULL;

-- 4、将teacher表的address字段删除
ALTER TABLE teacher DROP address;

-- 5、在teacher表中增加名为wages的字段，数据类型为FLOAT
ALTER TABLE teacher ADD wages FLOAT;

-- 6、将teacher表改名为teacherInfo
ALTER TABLE teacher RENAME TO teacherInfo;

-- 7、将teacher表的存储引擎更改为MyISAM类型
ALTER TABLE teacherInfo ENGINE=MyISAM;
```

* 操作department表和worker表

department表的内容

| 字段名   | 字段描述 | 数据类型    | 主键 | 外键 | 非空 | 唯一 | 自增 |
| -------- | -------- | ----------- | ---- | ---- | ---- | ---- | ---- |
| d_id     | 部门号   | INT(4)      | 是   | 否   | 是   | 是   | 否   |
| d_name   | 部门名   | VARCHAR(20) | 否   | 否   | 是   | 是   | 否   |
| function | 部门职能 | VARCHAR(50) | 否   | 否   | 否   | 否   | 否   |
| address  | 部门位置 | VARCHAR(20) | 否   | 否   | 否   | 否   | 否   |

worker表的内容

| 字段名   | 字段描述 | 数据类型    | 主键 | 外键 | 非空 | 唯一 | 自增 |
| -------- | -------- | ----------- | ---- | ---- | ---- | ---- | ---- |
| id       | 编号     | INT(4)      | 是   | 否   | 是   | 是   | 是   |
| num      | 员工号   | INT(10)     | 否   | 否   | 是   | 是   | 否   |
| d_id     | 部门号   | INT(4)      | 否   | 是   | 否   | 否   | 否   |
| name     | 姓名     | VARCHAR(20) | 否   | 否   | 是   | 否   | 否   |
| sex      | 性别     | VARCHAR(4)  | 否   | 否   | 是   | 否   | 否   |
| birthday | 出生日志 | DATETIME    | 否   | 否   | 否   | 否   | 否   |
| address  | 家庭住址 | VARCHAR(50) | 否   | 否   | 否   | 否   | 否   |

```SQL
USE example;

DROP TABLE if exists department;

CREATE TABLE department(
	d_id INT(4) NOT NULL UNIQUE PRIMARY KEY,
    d_name VARCHAR(20) NOT NULL UNIQUE,
    function VARCHAR(50),
    address VARCHAR(20)
);

DROP TABLE if exists worker;

CREATE TABLE worker(
	id INT(4) NOT NULL UNIQUE PRIMARY KEY AUTO_INCREMENT,
    num INT(10) NOT NULL UNIQUE,
    d_id INT(4),
    name VARCHAR(20) NOT NULL,
    sex VARCHAR(4) NOT NULL,
    birthday DATETIME,
    address VARCHAR(50),
    CONSTRAINT worker_fk FOREIGN KEY(d_id)
    REFERENCES department(d_id)
);
```

###### 习题

* 在example数据库中创建一个animal表并完成相应的练习

  animal表

  | 字段名   | 字段描述 | 数据类型    | 主键 | 外键 | 非空 | 唯一 | 自增 |
  | -------- | -------- | ----------- | ---- | ---- | ---- | ---- | ---- |
  | id       | 编号     | INT(4)      | 是   | 否   | 是   | 是   | 是   |
  | name     | 名称     | VARCHAR(20) | 否   | 否   | 是   | 否   | 否   |
  | kinds    | 种类     | VARCHAR(8)  | 否   | 否   | 是   | 否   | 否   |
  | legs     | 腿的条数 | INT(4)      | 否   | 否   | 否   | 否   | 否   |
  | behavior | 习性     | VARCHAR(50) | 否   | 否   | 否   | 否   | 否   |

  ```SQL
  USE example;
  
  DROP TABLE IF EXISTS animal;
  
  CREATE TABLE animal(
  	id INT(4) NOT NULL UNIQUE PRIMARY KEY AUTO_INCREMENT,
      name VARCHAR(20) NOT NULL,
      kinds VARCHAR(8) NOT NULL,
      legs INT(4),
      behavior VARCHAR(50)
  );
  
  -- 1、将name字段的数据类型改为VARCHAR(30)，且保留非空约束
  ALTER TABLE animal MODIFY name VARCHAR(30) NOT NULL;
  
  -- 2、将behavior字段的位置改到legs字段的前面
  ALTER TABLE animal MODIFY behavior VARCHAR(50) after kinds;
  
  -- 3、将kings字段改名为category
  ALTER TABLE animal CHANGE kinds category VARCHAR(8) NOT NULL;
  
  -- 4、在表中增加fur字段，数据类型为VARCHAR(10)
  ALTER TABLE animal ADD fur VARCHAR(10);
  
  -- 5、删除legs字段
  ALTER TABLE animal DROP legs;
  
  -- 6、将animal表的存储引擎更改为MyISAM类型
  ALTER TABLE animal ENGINE=MyISAM;
  
  -- 7、将animal表更名为animalInfo
  ALTER TABLE animal RENAME animalInfo;
  ```

  

* 在transport数据库创建一个transport表和一个car表

  transport表

  | 字段名   | 字段描述 | 数据类型    | 主键 | 外键 | 非空 | 唯一 | 自增 |
  | -------- | -------- | ----------- | ---- | ---- | ---- | ---- | ---- |
  | id       | 编号     | INT(4)      | 是   | 否   | 是   | 是   | 否   |
  | type     | 类型     | VARCHAR(20) | 否   | 否   | 是   | 是   | 否   |
  | function | 功能     | VARCHAR(50) | 否   | 否   | 否   | 否   | 否   |

  car表

  | 字段名  | 字段描述 | 数据类型    | 主键 | 外键 | 非空 | 唯一 | 自增 |
  | ------- | -------- | ----------- | ---- | ---- | ---- | ---- | ---- |
  | id      | 编号     | INT(4)      | 是   | 否   | 是   | 是   | 是   |
  | num     | 类型名   | INT(10)     | 否   | 是   | 是   | 否   | 否   |
  | name    | 名称     | VARCHAR(20) | 否   | 否   | 是   | 否   | 否   |
  | company | 生产商名 | VARCHAR(50) | 否   | 否   | 否   | 否   | 否   |
  | address | 生产地址 | VARCHAR(50) | 否   | 否   | 否   | 否   | 否   |

  ```SQL
  CREATE DATABASE transport;
  
  USE transport;
  
  DROP TABLE IF EXISTS transport;
  
  CREATE TABLE transport(
  	id INT(4) NOT NULL UNIQUE PRIMARY KEY,
      type VARCHAR(20) NOT NULL UNIQUE,
      function VARCHAR(50)
  );
  
  DROP TABLE IF EXISTS car;
  
  CREATE TABLE car(
  	id INT(4) NOT NULL UNIQUE PRIMARY KEY AUTO_INCREMENT,
      num INT(10) NOT NULL,
      name VARCHAR(20) NOT NULL,
      company VARCHAR(50),
      address VARCHAR(50),
      CONSTRAINT car_fk FOREIGN KEY(num)
      REFERENCES transport(id)
  );
  ```

  

  

