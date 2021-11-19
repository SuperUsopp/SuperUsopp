#### 插入、更新与删除数据

* 插入新数据

  * 为表的所有字段插入数据

  ```SQL
  -- 1、INSERT语句中不指定具体的字段名
  INSERT INTO 表名 VALUES(值1, 值2, ..., 值n);
  
  -- 2、INSERT语句中列出所有字段
  INSERT INTO 表名(属性1, 属性2, ..., 属性n)
  	VALUES(值1, 值2, ..., 值n);
  ```

  * 为表的指定字段插入数据

  ```SQL
  INSERT INTO 表名(属性1, 属性2, ..., 属性n)
  	VALUES(值1, 值2, ..., 值n);
  ```

  * 同时插入多条记录

  ```SQL
  INSERT INTO 表名[(属性列表)]
  	VALUES(取值列表1),(取值列表2)
  	...,
  	(取值列表n);	
  ```

  * 将查询结果插入到表中

  ```SQL
  INSERT INTO 表名1(属性列表1)
  	SELECT 属性列表2 FROM 表名2 WHERE 条件表达式;
  ```

  

* 更新数据

  ```SQL
  UPDATE 表名
  	SET 属性名1=取值1, 属性名2=取值2,
  	...
  	属性名n=取值n
  	WHERE 条件表达式：
  ```

  

* 删除记录

  ```SQL
  DELETE FROM 表名 [WHERE 条件表达式];
  ```

  

