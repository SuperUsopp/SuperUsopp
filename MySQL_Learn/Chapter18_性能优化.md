#### 性能优化

​	定义：性能优化是通过某些有效的方法提高MySQL数据库的性能。

​	目的：性能优化的目的是为了使MySQL数据库运行速度更快、占用的磁盘空间更小

* 性能优化的介绍

  ```SQL
  -- 如果MySQL数据库需要进行大量的查询操，那么就需要对查询语句进行优化。
  -- 如果连接MySQL数据库的用户很多，那么就需要对MySQL服务器进行优化。
  -- 数据库管理员可以使用SHOW STATUS语句查询MySQL数据库的性能
  SHOW STATUS LIKE 'value';
  
  -- value取值如下：
  -- Connections:连接MySQL服务器的次数
  -- Uptime:MySQL服务器的上线时间
  -- Slow_queries:慢查询的次数
  -- Com_select:查询操作的次数
  -- Com_insert:插入操作的次数
  -- Com_update:更新操作的次数
  -- Com_delete:删除操作的次数
  ```

  

* 优化查询

  ​	查询是数据库中最频繁的操作，提高了查询速度可以有效的提高MySQL数据库的性能

  * 分析查询语句

    ```SQL
    -- MySQL中，可以使用EXPLAIN语句和DESCRIBE语句来分析查询语句
    EXPLAIN SELECT 语句;
    
    -- 示例18-1
    EXPLAIN SELECT * FROM student \G
    DESCRIBE SELECT * FROM student \G
    ```

    

  * 索引对查询速度的影响

  	```SQL
  -- 索引可以快速的定位表中的某条记录，使用索引可以提高数据库查询的速度，从而提高数据库的性能
  -- 示例18-2（不适用索引查询）
  EXPLAIN SELECT * FROM student WHERE name='张三' \G
  -- （添加索引后再次查询）
  CREATE INDEX index_name ON student(name);
  EXPLAIN SELECT * FROM student WHERE name='张三';

  * 使用索引查询

    ```SQL
    -- 1、查询语句中使用LIKE关键字
    -- （在查询语句中使用LIKE关键字进行查询是，如果匹配字符串的第一个字符为“%”时，索引不会被使用。如果“%”不是在第一个位置，索引就会被使用）
    -- 示例18-3
    -- （索引不会被使用）
    EXPLAIN SELECT * FROM student WHERE name LIKE "%四" \G
    -- (索引会被使用)
    EXPLAIN SELECT * FROM student WHERE name LIKE "李%" \G
    
    -- 2、查询语句中使用多列索引
    -- （多列索引是在表的多个字段上创建一个索引，只有查询条件中使用了这些字段中第一个字段时，索引才会被使用）
    -- 示例18-4
    CREATE INDEX index_birth_department ON student(birth, department);
    EXPLAIN SELECT * FROM student WHERE birth=1991 \G;			  -- 该条语句会使用索引查询
    EXPLAIN SELECT * FROM student WHERE department="英语系" \G;	-- 该条语句不会使用索引查询
    
    -- 3、查询语句中使用OR关键字
    -- （查询语句只有OR关键字时，如果OR前后的两个条件的列都是索引时，查询中将使用索引。如果OR前后有一个条件的列不是索引，那么查询中将不使用索引）
    -- 示例18-5
    EXPLAIN SELECT * FROM student WHERE name="张三" or sex="女" \G		-- sex不是索引列
    EXPLAIN SELECT * FROM student WHERE name="张三" or id=3 \G		-- id是索引列
    ```
  
  * 优化子查询
  
  	```SQL
  	-- 子查询可以使查询语句很灵活，但子查询的执行效率不高
	  -- 子查询时，MySQL需要为内层查询语句的查询结果建立一个临时表，然后外层查询语句再在临时表中查询记录，查询完毕后，MySQL需要撤销这些临时表
    -- 在MySQL中可以使用连接查询来代替子查询，连接查询不需要建立临时表，其速度比子查询要快
    ```

  
  


* 优化数据库结构


  * 将字段很多的表分解成多个表

    ```SQL
    -- 背景：有些表在设计时设置了很多的字段，而有些字段的使用频率很低，当这个表的数据量很大时，查询数据的速度就会很慢
    -- 解决：对于这种字段特别多且有些字段的使用频率很低的表，可以将其分解成多个表
    ```

  * 增加中间表

    ```SQL
    -- 背景：有时需要经常查询某两个表中的几个字段，如果经常进行连表查询，会降低MySQL数据库的查询速度
    -- 解决：对于这种情况，可以建立中间表来提高查询速度
    ```

  * 增加冗余字段

    ```SQL
    -- 背景：表的规范化程度越高，表与表之间的关系就越多；查询时可能经常需要在多个表之间进行连接查询；而进行连接操作会降低查询速度
    -- 解决：如果进场需要进行连接查询时，就会浪费很多的时间，因此可以在一个经常查询的表中增加一个冗余字段
    ```

    

  * 优化插入记录的速度

    ```SQL
    -- 背景：插入记录时，索引、唯一性校验都会影响到插入记录的速度。而且，一次插入多条记录和多次插入记录所耗费的时间是不一样的（影响插入速度）
    -- 解决：针对不同的情况，分别进行不同的优化
    
    -- 1、禁用索引
    -- 背景：插入记录时，MySQL会根据表的索引对插入的记录进行排序。如果插入大量数据时，这些排序会降低插入记录的速度
    -- 解决：为了解决这种情况，在插入记录之前先禁用索引，等到记录都插入完毕后再开启索引
    -- >禁用索引
    ALTER TABLE 表名 DISABLE KEYS;
    -- >重新开启索引
    ALTER TABLE 表名 ENABLE KEYS;
    
    -- 2、禁用唯一性检查
    -- 背景：插入记录时，MySQL会对插入的记录进行唯一性校验，这种校验也会降低插入记录的速度
    -- 解决：在插入记录之前禁用唯一性检查，等到记录插入完毕后再开启
    -- >禁用唯一性检查
    SET UNIQUE_CHECKS=0
    -- >重新开启唯一性检查语句
    SET UNIQUE_CHECKS=1;
    
    -- 3、优化INSERT语句
    -- 背景：在插入大量数据时，有两种插入方式（a.使用一条INSERT语句插入多条记录，b.使用多条INSERT语句，每条语句插入一条记录）
    -- 解决：当插入大量数据时，建议使用一个INSERT语句插入多条记录的方式
    ```

    

  * 分析表、检查表和优化表

    * 分析表：分析关键字的分布

    ```SQL
    ANALYZE TABLE 表名1[,表名2...];
    ```

    

    * 检查表：检查表是否存在错误

    ```sql
    CHECK TABLE 表名1[,表名2...] [option];
    ```

    

    * 优化表：消除删除或更新造成的空间浪费

    ```SQL
    OPTIMIZE TABLE 表名1[,表名2...];
    ```

    

* 优化MySQL服务器

  优化服务器可以从两个方面来理解：

  ​	1）从硬件方面来进行优化

  ​	2）从MySQL服务的参数进行优化

