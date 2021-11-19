##### 查询数据

###### 查询语句的基本语法

```SQL
-- 语法
SELECT 属性列表
	FROM 表名和视图列表
	[WHERE 条件表达式1]
	[GROUP BY 属性名1 [HAVING 条件表达式2]]
	[ORDER BY 属性名2 [ASC|DESC]];

DROP TABLE IF EXISTS employee;
CREATE TABLE employee(
	num INT(10) NOT NULL UNIQUE PRIMARY KEY,
    name VARCHAR(20) NOT NULL,
    age INT(4),
    sex VARCHAR(20),
    homeaddr VARCHAR(50)
);

-- 示例10-1
SELECT num,name,age,sex,homeaddr FROM employee;

-- 示例10-2
SELECT num,d_id,name,age,sex,homeaddr
	FROM employee
	WHERE age<26
	ORDER BY d_id DESC;
```



###### 在单表上查询数据

从一张表中查询所需要的数据（与多表查询有区别）

* 查询所有字段

  ```SQL
  -- 示例10-3
  SELECT num,d_id,name,age,sex,homeaddr FROM employee;
  
  -- 示例10-4
  SELECT * FROM employee;
  ```

* 查询指定的字段

```SQL
-- 示例10-5
SELECT num,name,sex,homeaddr FROM employee;

```

* 查询指定的行

```sql
-- 示例10-6
SELECT * FROM employee WHERE d_id=1001;
```

| 查询条件     | 符号或关键字                    |
| ------------ | ------------------------------- |
| 比较         | =，<，<=，>，>=，!=，<>，!>，!< |
| 指定范围     | BETWEEN AND，NOT BETWEEN AND    |
| 指定集合     | IN，NOT IN                      |
| 匹配字符     | LIKE，NOT LIKE                  |
| 是否为空值   | IS NULL，IS NOT NULL            |
| 多个查询条件 | AND，OR                         |

```SQL
-- 示例10-8
SELECT * FROM employee WHERE d_id IN(1001, 1004);

-- 示例10-9
SELECT * FROM employee WHERE name NOT IN("张三", "李四");

-- 示例10-10
SELECT * FROM employee WHERE age BETWEEN 15 AND 25;

-- 示例10-11
SELECT * FROM employee WHERE age NOT BETWEEN 15 AND 25;

-- 示例10-12
SELECT * FROM employee WHERE name LIKE "Aric";

-- 示例10-13
SELECT * FROM employee WHERE homeaddr LIKE "北京%";

-- 示例10-14
SELECT * FROM employee WHERE name LIKE "Ar_c";

-- 示例10-15
SELECT * FROM employee WHERE name NOT LIKE "张%";

-- 示例10-16
SELECT * FROM work WHERE info IS NULL;

-- 示例10-17
SELECT * FROM work WHERE info IS NOT NULL;
```



* 执行多条件查询

```sql
-- 示例10-18
SELECT * FROM employee WHERE d_id=1001 AND sex LIKE "男";

-- 示例10-19
SELECT * FROM employee WHERE d_id<1004 AND age<26 AND sex='男';

-- 示例10-20
SELECT * FROM employee WHERE num in (1,2,3) AND age BETWEEN 15 AND 25 AND homeaddr LIKE "%北京市%"

-- 示例10-21
SELECT * FROM employee WHERE d_id=1001 OR sex='男';

-- 示例10-21
SELECT * FROM employee WHERE num in (1,2,3) OR age BETWEEN 24 AND 25 OR homeaddr LIKE "%北京市%";

-- 示例10-22
SELECT * FROM employee WHERE num in (1,2,3) AND age=25 OR sex='女';

-- 示例10-23
SELECT * FROM employee WHERE sex='女' OR num in (1,2,3) AND age=25;
```



* 查询结果不重复

```sql
-- 示例10-24
SELECT DISTINCT d_id FROM employee;
```



* 给查询结果排序

```sql
-- 示例10-25
SELECT * FROM employee ODERE BY age;

-- 示例10-26
SELECT * FROM employee ORDER BY age ASC;

-- 示例10-27
SELECT * FROM employee ORDER BY age DESC;

-- 示例10-28
SELECT * FROM employee ORDER BY d_id ASC, age DESC;
```



* 分组查询

```sql
-- GTOUP BY关键字可以将查询结果按某个字段或多个字段进行分组。字段中值相等的为一组。
-- 示例10-29
SELECT * FROM employee GROUP BY sex;

-- 示例10-30
SELECT sex, GROUP_CONCAT(name) FROM employee GROUP BY sex;

-- 示例10-31
SELECT sex, COUNT(sex) FROM employee GROUP BY sex;

-- 示例10-32
SELECT sex，COUNT(sex) FROM employee GROUP BY sex HAVING COUNT(sex)>=3;

-- 按多个字段进行分组
-- 示例10-33
SELECT * FROM employee GROUP BY d_id, sex;

-- GROUP BY关键字与WITH ROLLUP一起使用（使用WITH ROLLUP时，将会在所有记录的最后加上一条记录，这条记录是上面所有记录的总和）
-- 示例10-34
SELECT sex,GOUNT(sex) FROM employee GROUP BY sex WITH ROLLUP;

-- 示例10-35
SELECT sex,GROUP_CONCAT(name) FROM employee GROUP BY sex WITH ROLLUP;

```



* 用LIMIT限制查询结果的数量

```sql
-- 1、不指定初始位置
-- 示例10-36(当‘记录数’的值小于查询结果的总记录数，将会从第一条记录开始，显示指定条数的记录)
SELECT * FROM employee LIMIT 2;

-- 示例10-37(当‘记录数’的值大于查询结果的总记录数，数据库系统会直接显示查询出来的所有记录)
SELECT * FROM employee LIMIT 6;

-- 2、指定初始位置
-- 示例10-38
SELECT * FROM employee LIMIT 0,2;

-- 示例10-39
SELECT * FROM employee LIMIT 1,2;

```



###### 使用聚合函数查询数据

```SQL
-- 集合函数包括
-- COUNT()	用来统计记录的条数,
-- SUM()	用来计算字段的值的总和,
-- AVG()	用来计算字段的值的平均值,
-- MAX()	用来查询字段的最大值,
-- MIN()	用来查询字段的最小值

-- 示例10-40
SELECT COUNT(*) FROM employee;

-- 示例10-41
SELECT d_id, COUNT(*) FROM employee GROUP BY d_id;

-- 示例10-42
SELECT * FROM grade WHERE num=1001;

-- 示例10-43
SELECT num,SUM(score) FROM grade GROUP BY num;

-- 示例10-44
SELECT AVG(age) FROM employee;

-- 示例10-45
SELECT course,AVG(score) FROM grade GROUP BY course;

-- 示例10-46
SELECT MAX(age) FROM employee;

-- 示例10-47
SELECT course, MAX(score) FROM grade GROUP BY course;

-- 示例10-48(MAX()适用于字符类型)
SELECT MAX(name) FROM work;

-- 示例10-49
SELECT MIN(age) FROM employee;

-- 示例10-50
SELECT course, MIX(score) FROM grade GROUP BY course;
```



###### 多表上联合查询

```sql
-- 连接查询是将两个或两个以上的表按某个条件连接起来，从中选取需要的数据。
-- 当不同的表中存在表示相同意义的字段时，可以通过该字段来连接这几个表
-- 内连接查询：
-- 外连接查询：需要通过指定字段来进行连接，当该字段取值相等时，可以查询出该记录
CREATE TABLE employee(
	num INT(10) NOT NULL UNIQUE PRIMARY KEY,
    d_id INT(10) NOT NULL,
    name varchar(20),
    age int(4),
    sex varchar(4),
    homeaddr varchar(50)
);

-- 示例10-51
SELECT num,name,employee.d_id,age,sex,d_name,function
	FROM employee, department
	WHERE employee.d_id=department.d_id;
```



###### 子查询

###### 合并查询结果

###### 为表和字段取别名

###### 使用正则表达式查询







