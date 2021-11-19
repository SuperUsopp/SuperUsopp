#### 存储过程和函数

创建存储过程和函数是指将经常使用的一组SQL语句的组合在一起，并将这些SQL语句当做一个整体存储在MySQL服务器中

* 创建存储过程

```SQL
-- 创建存储过程
CREATE PROCEDURE sp_name([proc_parameter[,...]])
	[characteristic...] routine_body
-- 示例14-1
CREATE PROCEDURE num_from_employee(IN emp_id INT, OUT count_num INT)
	READS SQL DATA
	BEGIN
		SELECT COUNT(*) INTO count_num
		FROM employee
		WHERE d_id=emp_id
	END
```

* 创建存储函数

```SQL
-- 创建存储函数
CREATE FUNCTION sp_name([func_parameter[,...]])
	RETURNS type
	[characteristic...] routine_body
-- 示例14-2
CREATE FUNCTION name_from_employee(emp_id INT)
	RETURNS VARCHAR(20)
	BEGIN
		RETURN (SELECT name FROM employee WHERE num=emp_id)
	END
```



* 变量的使用

```SQL
-- 变量的定义
DECLARE var_name[,...] type [DEFAULT value]

-- 为变量赋值
-- 方法1
SET var_name=expr[, var_name=expr]
-- 方法2
SELECT col_name[,...] INTO var_name[,...]
	FROM table_name WHERE condition
```



* 定义条件和处理程序

  定义条件和处理程序是事先定义程序执行过程中可能遇到的问题。并且可以在处理程序中定义解决这些问题的办法。这种方式可以提前预测可能出现的问题，并提出解决办法。这样可以增强程序处理问题的能力，避免程序异常停止。

  ```SQL
  -- 定义条件
  DECLARE condition_name CONDITION FOR condition_value
  -- 其中：
  condition_value:
  	SQLSTATE [VALUE] sqlstate_value | mysql_error_code
  	
  
  -- 定义处理程序
  DECLARE handler_type HANDLER FOR contidion_value[,...] sp_statement
  -- 其中
  handler_type:
  	CONTINUE|EXIT|UNDO
  condition_value:
  	SQLSTATE [VALUE] sqlstate_value | condition_name | SQLWARNING | NOT FOUND | SQLEXCEPTION | sql_error_code
  ```

  

* 光标的使用

```SQL
-- 1、声明光标
DECLARE cursor_name CURSOR FOR select_statement;
-- 示例14-8
DECLARE cur_employee CURSOR FOR SELECT name, age FROM employee;


-- 2、打开光标
OPEN cursor_name;
-- 示例14-9
OPEN cur_employee;


-- 3、操作光标
FETCH cursor_name INTO var_name[,var_name...];
-- 示例14-10
FETCH cur_employee INTO emp_name, emp_age;

-- 4、关闭关闭
CLOSE cursor_name;
-- 示例14-11
CLOSE cur_employee;
```



* 流程控制的使用

```SQL
-- 1、IF语句
IF search_condition THEN statement_list
	[ELSEIF search_condition THEN statement_list]...
	[ELSE statement_list]
END IF
-- 示例14-12
IF age>20 THEN SET @count1=@count1+1;
	ELSEIF age=20 THEN @count2=@count2+1;
	ELSE @count3=@count3+1;
END IF;S


-- 2、CASE语句
CASE case_value
	WHEN when_value THEN statement_list
	[WHEN when_value THEN statement_list]...
	[ELSE statement_list]
END CASE

CASE 
	WHEN search_condition THEN statement_list
	[WHEN search_condition THEN statement_list]...
	[ELSE statement_list]
END CASE
-- 示例14-13
CASE age
	WHEN 20 THEN SET @count1=@count1+1;
	ELSE SET @count2=@count2+1;
END CASE;

CASE
	WHEN age=20 THEN SET @count1=@count1+1;
	ELSE SET @count2=@count2+1;
END CASE


-- 3、LOOP语句
[begin_label:]LOOP
	statement_list
END LOOP[end_label]
-- 示例14-14
add_num:LOOP
	SET @count=@count+1;
END LOOP add_num


-- 4、LEAVE语句
LEAVE label
-- 示例14-15
add_num:LOOP
	SET @count=@count+1;
	IF @count=100 THEN
		LEAVE add_num;
END LOOP add_num;


-- 5、ITERATE语句
ITERATE label
-- 示例14-16
add_num:LOOP
	SET @count=@count+1;
	IF @count=100 THEN 
		LEAVE add_num;
	ELSE IF MOD(@count,3)=0 THEN
		ITERATE add_num;
	SELECT * FROM employee;
END LOOP add_num;


-- 6、REPEAT语句
[begin_label:]REPEAT
	statement_list
	UNTIL search_condition
END REPEAT [end_label]
-- 示例14-17
REPEAT 
	SET @count=@count+1;
	UNTIL @count=100
END REPEAT


-- 7、WHILE语句
[begin_label:]WHERE search_condition DO
	statement_list
END WHILE [end_label]
-- 示例14-18
WHILE @count<100 DO
	SET @count=@count+1;
END WHILE;
```



* 调用存储过程和函数

```sql
-- 1、调用存储过程
CALL sp_name([parameter[,...]]);
-- 示例14-19
DELIMITER &&
CREATE PROCEDURE num_from_employee(IN emp_id INT, OUT count_num INT)
READS SQL DATA
BEGIN
	SELECT COUNT(*) INTO count_num
		FROM employee
		WHERE d_id=emp_id;
END &&
DELIMITER ;

CALL num_from_employee(1002,@n);

-- 2、调用存储函数
CALL sp_name([parameter[,...]]);
-- 示例14-20
DELIMITER &&
CREATE FUNCTION name_from_employee(emp_id INT)
RETURNS VARCHAR(20)
BEGIN
	RETURN (SELECT name FROM employee WHERE num=emp_id);
END &&
DELIMITER;
```



* 查看存储过程和函数

```SQL
-- 1、SHOW STATUS
SHOW {PROCEDURE|FUNCTION} STATUS [LIKE 'pattern'];
-- 示例14-21
SHOW PROCEDURE STATUS LIKE 'num_from_employee' \G


-- 2、SHOW CREATE
SHOW CREATE {PROCEDURE|FUNCTION} sp_name;
-- 示例14-22
SHOW CREATE PROCEDURE num_from_employee \G


-- 3、从information——schema.Routines表中查看存储过程和函数的信息
SELECT * FROM information_schema.Routines WHERE ROUTINE_NAME='sp_name';
-- 
```



* 修改存储过程和函数

```sql
-- 使用ALTER PROCEDURE/ALTER FUNCTION语句来修改存储过程
ALTER {PROCEDURE|FUNCTION} sp_name [characteristic...]
-- 其中：
characteristic:
	{CONTAINS SQL | NO SQL | READS SQL DATA | MODIFIES SQL DATA}
	| SQL SECURITY {DEFINER|INVOKER}
	|COMMENT 'string'
-- 示例14-24
ALTER PROCEDURE num_from_employee
	MODIFIES SQL DATA
	SQL SECURITY INVOKE;
```



* 删除存储过程和函数

```SQL
DROP {PROCEDURE|FUNCTION} sp_name;
-- 示例14-26
DROP PROCEDURE num_from_employee;
DROP FUNCTION name_from_employee;
```

