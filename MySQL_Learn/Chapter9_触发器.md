##### 触发器

###### 触发器的含义和作用

```SQL
-- 触发器是由INSERT、UPDATE和DELETE等事件来触发某种特定操作。
-- 作用；可以保证某些操作之间的一致性

```



###### 如何创建触发器

```SQL
-- 示例9-1:创建只有一个执行语句的触发器
CREATE TRIGGER 触发器名 BEFORE|AFTER 触发事件
	ON 表名 FOR EACH ROW 执行语句
	
CREATE TRIGGER dept_trig1 BEFORE INSERT
	ON department FOR EACH ROW
	INSERT INTO trigger_time VALUES(NOW());

-- 示例9-2：创建有多个执行语句的触发器
CREATE TRIGGER 触发器名 BEFORE|AFTER 触发事件
	ON 表名 FOR EACH ROW
	BEGIN
		执行语句列表
	END

DELIMITER &&
CREATE TRIGGER dept_trig2 AFTER DELETE
	ON department FOR EACH ROW
	BEGIN
		INSERT INTO trigger_time VALUES ('21:01:01');
		INSERT INTO trigger_time VALUES ('22:01:01');
	END
	&&
DELIMITER ;
```



###### 如何查看触发器

查看数据库中已存在的触发器的定义、状态和语法等信息

```SQL
-- SHOW TRIGGERS语句 (不可查询指定触发器)
-- 示例9-3
SHOW TRIGGERS \G;

-- 查询information_schema数据库下的triggers表（可查询指定触发器）
-- 示例9-4
SELECT * FROM information_schema.triggers \G
-- 示例9-5
SELECT * FROM information_schema.triggers WHERE TRIGGER_NAME='dept_trig1' \G
```

###### 触发器的使用

```SQL
-- 示例9-6
// 创建BEFORE INSERT触发器
CREATE TRIGGER before_insert BEFORE INSERT
	ON department FOR EACH ROW
	INSERT INTO trigger_test VALUES(null, "before_insert");
	
// 创建AFTER INSERT触发器
CREATE TRIGGER after_insert AFTER INSERT
	ON department FOR EACH ROW
	INSERT INTO trigger_test VALUES(null, "after_insert");

```



* 在MySQL中，触发器的执行顺序是BEFORE触发器、表操作（INSERT, UPDATE和DELETE）和AFTER触发器
* 触发器中不能包含START TRANSACTION, COMMIT或ROLLBACK等关键词，也不能包含CALL语句
* 在触发器执行过程中，任何步骤出错都会阻止程序向下执行

###### 如何删除触发器

```SQL
DROP TRIGGER 触发器名;

```

###### 本章实例

在product表上创建3个触发器，每次激活触发器后，都会更新operate表

product表：

| 字段名   | 字段描述 | 数据类型    | 主键 | 外键 | 非空 | 唯一 | 自增 |
| -------- | -------- | ----------- | ---- | ---- | ---- | ---- | ---- |
| id       | 产品编号 | INT(10)     | 是   | 否   | 是   | 是   | 否   |
| name     | 产品名称 | VARCHAR(20) | 否   | 否   | 是   | 否   | 否   |
| function | 主要功能 | VARCHAR(50) | 否   | 否   | 否   | 否   | 否   |
| company  | 生产厂商 | VARCHAR(20) | 否   | 否   | 是   | 否   | 否   |
| address  | 家庭住址 | VARCHAR(50) | 否   | 否   | 否   | 否   | 否   |

operate表

| 字段名  | 字段描述 | 数据类型    | 主键 | 外键 | 非空 | 唯一 | 自增 |
| ------- | -------- | ----------- | ---- | ---- | ---- | ---- | ---- |
| op_id   | 编号     | INT(10)     | 是   | 否   | 是   | 是   | 是   |
| op_type | 操作方式 | VARCHAR(20) | 否   | 否   | 是   | 否   | 否   |
| op_time | 操作时间 | TIME        | 否   | 否   | 是   | 否   | 否   |

```SQL
use example;
DROP TABLE IF EXISTS product;
CREATE TABLE product(
	id INT(10) NOT NULL UNIQUE PRIMARY KEY,
    name VARCHAR(20) NOT NULL,
    function VARCHAR(50),
    company VARCHAR(20) NOT NULL,
    address VARCHAR(50)
);

DROP TABLE IF EXISTS operate;
CREATE TABLE operate(
	op_id INT(10) NOT NULL UNIQUE PRIMARY KEY AUTO_INCREMENT,
    op_type VARCHAR(20) NOT NULL,
    op_time TIME NOT NULL
);
-- 1、在product表上分别创建BEFORE INSERT、AFTER UPDATE和AFTER DELETE3个触发器，触发器的名称分别为product_bf_insert, product_af_update和product_af_del。执行语句都是向operate表插入操作方法和操作时间
CREATE TRIGGER product_bf_insert BEFORE INSERT
	ON product FOR EACH ROW
	INSERT INTO operate VALUES(null,'INSERT', NOW());

CREATE TRIGGER product_af_update AFTER UPDATE
	ON product FOR EACH ROW
	INSERT INTO operate VALUES(null,'UPDATE', NOW());

CREATE TRIGGER product_af_del AFTER DELETE
	ON product FOR EACH ROW
	INSERT INTO operate VALUES(null,'DELETE', NOW());
	
-- 2、对product表分别执行INSERT、UPDATE和DELETE操作
INSERT INTO product VALUES (1,'abc','zhiliao','abccompany','changping');
INSERT INTO product VALUES (2,'abc','zhiliao','abccompany','changping');
INSERT INTO product VALUES (3,'abc','zhiliao','abccompany','changping');
UPDATE product SET name='efg' WHERE id=2;
DELETE FROM product WHERE id=3;

-- 3、删除product_bf_insert和product_af_update这两个触发器
DROP TRIGGER product_bf_insert;
DROP TRIGGER product_af_update;
```

###### 上机实践

```SQL
--  在product表上分别创建AFTER INSERT、BEFORE UPDATE和BEFORE DELETE3个触发器，触发器的名称分别为product_af_insert、product_bf_update和product_bf_del。执行语句部分都是向operate表中插入操作方法和操作时间
CREATE TRIGGER product_af_insert AFTER INSERT
	ON product FOR EACH ROW
	INSERT INTO operate VALUES(null, 'INSERT', NOW());

CREATE TRIGGER product_bf_update BEFORE UPDATE
	ON product FOR EACH ROW
	INSERT INTO operate VALUES(null, 'UPDATE', NOW());

CREATE TRIGGER product_bf_del BEFORE DELETE
	ON product FOR EACH ROW
	INSERT INTO operate VALUES(null, 'DELETE', NOW());
	
-- 查看product_bf_del触发器的基本结构
SHOW TRIGGERS\G;
SELECT * FROM information_schema.triggers WHERE trigger_name='product_bf_del' \G

-- 分别执行INSERT、UPDATE和DELETE操作来出发这3个触发器
INSERT INTO product VALUES (4,'abc','zhiliao','abccompany','changping');
UPDATE product SET name='efg' WHERE id=2;
DELETE FROM product WHERE id=1;

-- 删除product_bf_update和product_bf_del这两个触发器
DROP TRIGGER product_bf_update;
DROP TRIGGER product_bf_del;

```

