##### 视图

**视图是从一个或多个表中导出来的表，是一种虚拟存在的表，作用是方便用户对数据的操作**

###### 视图的含义和作用

​	含义：它是一种虚拟的表，是从数据库中的一个或多个表中导出来的表。数据库中只存放了视图的定义，并没有存放视图中的数据，这些数据仍存在原来的表中。

​	作用：视图起着类似于筛选的作用

    	* 使操作简单化
    	* 增加数据的安全性（通过视图，用户只能查询和修改制定的数据）
    	* 提高表的逻辑独立性

###### 如何创建视图

语法：

```SQL
CREATE [ALGORITHM={UNDEFINED|MERGE|TEMPTABLE}]
		VIEW 视图名 [(属性清单)]
		AS SELECT 语句
		[WITH [CASCADED|LOCAL] CHECK OPTION];
ALGORITHM参数：
 * UNDEFINED：表示MySQL将自动选择所要使用的算法
 * MERGE：表示MySQL将使用视图的语句与视图定义合并起来，使得视图定义的某一部分取代语句的对应部分
 * TEMPTABLE：表示将视图的结果存入临时表，然后使用临时表执行语句
```

在单表上创建视图

```SQL
-- 示例8-1：创建department和work表
USE example;
DROP TABLE IF EXISTS department;
CREATE TABLE department(
	d_id INT(4) NOT NULL PRIMARY KEY,
    d_name VARCHAR(20) NOT NULL,
    function VARCHAR(50),
    adderss VARCHAR(50)
);

DROP TABLE IF EXISTS worker;
CREATE TABLE worker(
	num INT(10) NOT NULL PRIMARY KEY,
    d_id INT(4),
    name VARCHAR(20) NOT NULL,
    sex VARCHAR(4) NOT NULL,
    birthday DATETIME,
    homeaddress VARCHAR(50),
    CONSTRAINT worker_fk FOREIGN KEY(d_id)
    	REFERENCES department(d_id)
);

-- 示例8-2：在单表上创建视图
CREATE VIEW department_view1
AS SELECT * FROM department;

-- 示例8-3
CREATE VIEW
	department_view2(name, function, location)
	AS SELECT d_name, function, address
	FROM department;
```

在多表上创建视图

```SQL
-- 示例8-4
CREATE ALGORITHM=MERGE VIEW
	worker_view1(name, department, sex, age, address)
	AS SELECT name, department.d_name, sex, 2021-birthday, address
	FROM worker, department WHERE worker.d_id=department.d_id
	WITH LOCAL CHECK OPTION;
```



###### 如何查看视图

查看数据库中已存在的视图的定义（需要有SHOW VIEW的权限）

* DESCRIBE语句查看视图基本信息

  ```SQL
  -- 语法：
  DESCRIBE 视图名;
  
  -- 示例8-5
  DESCRIBE worker_view1;
  ```

  

* SHOW TABLE STATUS

  ```SQL
  -- 语法：
  SHOW TABLE STATUS LIKE '视图名';
  
  -- 示例8-6
  SHOW TABLE STATUS LIKE 'worker_view1';
  ```

  

* SHOW CREATE VIEW

  ```SQL
  -- 语法：
  SHOW CREATE VIEW 视图名;
  
  -- 示例8-7
  SHOW CREATE VIEW worker_view1;
  ```

  

* 查询information_schema数据库下的views表等

  ```SQL
  -- 语法：
  SELECT * FROM information_schema.views;
  
  -- 示例8-8
  SELECT * FROM information_schema.views \G
  ```

  

###### 如何修改视图

* CREATE OR REPLACE VIEW语句修改视图

```SQL
-- 语法：
CREATE OR REPLACE [ALGORITHM={UNDEFINED|MERGE|TEMPTABLE}]
	VIEW 视图名 [(属性清单)]
	AS SELECT语句
	[WITH [CASCADED|LOCAL] CHECK OPTION];
	
-- 示例8-9
CREATE OR REPLACE ALGORITHM=TEMPTABLE
	VIEW department_view1(department, function, location)
	AS SELECT d_name, function, address FROM department;
```

* ALTER语句修改视图

```SQL
-- 语法：
ALTER [ALGORITHM={UNDEFINED|MERGE|TEMPTABLE}]
	VIEW 视图名 [(属性清单)]
	AS SELECT语句
	[WITH [CASCADED|LOCAL] CHECK OPTION];

-- 示例8-10
ALTER VIEW department_view2(department, name, sex, location)
	AS SELECT d_name, worker.name, worker.sex, address
	FROM department, worker WHERE department.d_id=worker.d_id
	WITH CHECK OPTION;
```



###### 更新视图

通过视图来插入（INSERT）、更新（UPDATE）和删除（DELETE）表中的数据

```SQL
-- 示例8-11
INSERT INTO department (d_id, d_name, function, address) VALUES (1001, '人事部', '管理公司人事变动', '2号楼3层');
INSERT INTO department (d_id, d_name, function, address) VALUES (1002, '生产部', '主管生产', '5号楼1层');

-- 创建视图department_view3
CREATE VIEW department_view3(name, function, location)
	AS SELECT d_name, function, address FROM department WHERE d_id=1001;
	
-- 更新视图department_view3
UPDATE department_view3 SET name='科研部', function='新产品研发', location='3号楼5层';

-- NOTE:并非所有的视图都是可以更新的。以下几种情况是无法更新视图的。
-- * 视图中包含SUM(),COUNT(),MAX()和MIN()等函数
-- * 视图中包含UNION,UNION ALL,DISTINCT,GROUP BY和HAVING等关键字
-- * 常量视图
-- * 视图中的SELECT中包含子查询
-- * 不可更新的视图导出的视图
-- * 创建视图时，ALGORITHM为TEMPTABLE类型
-- * 视图对应的表上存在没有默认值的列

```



###### 如何删除视图

删除数据库中已经存在的视图（删除视图时，只能删除视图的定义，不会删除数据）

```SQL
-- 语法：
DROP VIEW [IF EXISTS] 视图名列表 [RESTRICT|CASCADE];

-- 示例8-19
DROP VIEW IF EXISTS worker_view1;

-- 示例8-20
DROP VIEW IF EXISTS department_view1, department_view2;
```



###### 本章实例

在test数据库中work_info表上进行视图操作

| 字段名  | 字段描述 | 数据类型    | 主键 | 外键 | 非空 | 唯一 | 自增 |
| ------- | -------- | ----------- | ---- | ---- | ---- | ---- | ---- |
| id      | 编号     | INT(10)     | 是   | 否   | 是   | 是   | 否   |
| name    | 姓名     | VARCHAR(20) | 否   | 否   | 是   | 否   | 否   |
| sex     | 性别     | VARCHAR(4)  | 否   | 否   | 是   | 否   | 否   |
| age     | 年龄     | INT(5)      | 否   | 否   | 否   | 否   | 否   |
| address | 家庭地址 | VARCHAR(50) | 否   | 否   | 否   | 否   | 否   |
| tel     | 电话号码 | VARCHAR(20) | 否   | 否   | 否   | 否   | 否   |

```SQL
-- 创建work_info表
CREATE test;
USE test;
DROP TABLE IF EXISTS work_info;
CREATE TABLE work_info(
	id INT(10) NOT NULL UNIQUE PRIMARY KEY,
    name VARCHAR(20) NOT NULL,
    sex VARCHAR(4) NOT NULL,
    age INT(5),
    address VARCHAR(50),
    tel VARCHAR(20)
);

-- 向表中插入几条记录
INSERT INTO work_info (id, name, sex, age, address, tel)	VALUES(1, '张三', 'M', 18, '北京市海淀区', '1234567');
INSERT INTO work_info (id, name, sex, age, address, tel)	VALUES(2, '李四', 'M', 22, '北京市昌平区', '2345678');
INSERT INTO work_info (id, name, sex, age, address, tel)	VALUES(3, '王五', 'F', 17, '湖南省永州市', '3456789');
INSERT INTO work_info (id, name, sex, age, address, tel)	VALUES(4, '赵六', 'F', 25, '辽宁省阜新市', '4567890');

-- 创建视图info_view(从work_info表中选出age>20的记录来创建视图，视图的字段包括id, name, sex和address，ALGORITHM设置为MERGE类型，加上WITH LOCAL CHEKC OPTION条件)
CREATE ALGORITHM=MERGE VIEW info_view (id, name, sex, address)
	AS SELECT id, name, sex, address FROM work_info WHERE age>20
	WITH LOCAL CHECK OPTION;
	
-- 查看视图info_view的基本结构和详细结构
DESC info_view;
SHOW CREATE VIEW info_view \G

-- 查看视图info_view的所有记录
SELECT * FROM info_view;

-- 修改视图info_view，使其显示age<20的信息，其他条件不变
ALTER ALGORITHM=MERGE VIEW info_view (id, name, sex, address)
	AS SELECT id, name, sex, address FROM work_info WHERE age<20
	WITH LOCAL CHECK OPTION;

CREATE OR REPLACE ALGORITHM=MERGE VIEW info_view(id, name, sex, address)
	AS SELECT id, name, sex, address FROM work_info WHERE age>20
	WITH LOCAL CHECK OPTION;

-- 更新视图，将id为3的记录进行更新，设置其sex为M
UPDATE info_view SET sex='M' WHERE id=3;

-- 删除视图
DROP VIEW IF EXISTS info_view;
```



###### 上机实践

在example库下创建collage表

| 字段名 | 字段描述 | 数据类型    | 主键 | 外键 | 非空 | 唯一 | 自增 |
| ------ | -------- | ----------- | ---- | ---- | ---- | ---- | ---- |
| number | 学号     | INT(10)     | 是   | 否   | 是   | 是   | 否   |
| name   | 姓名     | VARCHAR(20) | 否   | 否   | 是   | 否   | 否   |
| major  | 专业     | VARCHAR(20) | 否   | 否   | 是   | 否   | 否   |
| age    | 年龄     | INT(5)      | 否   | 否   | 否   | 否   | 否   |

```SQL
USE example;
DROP TABLE IF EXISTS collage;
CREATE TABLE collage(
	number INT(10) NOT NULL UNIQUE PRIMARY KEY,
    name VARCHAR(20) NOT NULL,
    major VARCHAR(20) NOT NULL,
    age INT(5)
);

-- 在collage表上创建视图collage_view（视图的字段包括student_num, student_name, student_age和department，ALGORITHM设置为UNDEFINED类型，加上WITH LOCAL CHECK OPTION条件）
CREATE ALGORITHM=UNDEFINED VIEW collage_view (student_num, student_name, student_age, department) 
	AS SELECT number, name, age, major FROM collage 
	WITH LOCAL CHECK OPTION;
	
-- 查看视图collage_view的详细结构
SHOW CREATE VIEW collage_view \G

-- 更新视图（向视图中插入3条记录）
INSERT INTO collage_view (student_num, student_name, student_age, department) VALUES (0901, '张三', 20, '外语');
INSERT INTO collage_view VALUES (0902, '李四', 22, '计算机');
INSERT INTO collage_view VALUES (0903, '王五', 19, '计算机');

-- 修改视图，使其显示专业为计算机的信息，其他条件不变
ALTER ALGORITHM=UNDEFINED VIEW collage_view (student_num, student_name, student_age, department) 
	AS SELECT number, name, age, major FROM collage WHERE major='计算机'
	WITH LOCAL CHECK OPTION;
	
-- 删除视图
DROP VIEW collage_view;
```

