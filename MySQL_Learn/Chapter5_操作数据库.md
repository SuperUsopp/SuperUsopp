### 第5章 操作数据库

**数据库：长期存储在计算机内、有组织的和可共享的数据集合**

###### 创建数据库

```SQL
mysql> show databases;
+--------------------+
| Database           |
+--------------------+
| information_schema |
| mysql              |
| performance_schema |
| test               |
+--------------------+
4 rows in set (0.00 sec)

CREATE DATABASE 数据库名;

-- 示例5-1
CREATE DATABASE example;

mysql> CREATE DATABASE example;
Query OK, 1 row affected (0.00 sec)
```



###### 删除数据库

```sql
DROP DATABASE 数据库名;

-- 示例5-2
CREATE DATABASE mybook;

mysql> CREATE DATABASE mybook;
Query OK, 1 row affected (0.00 sec)


SHOW DATABASES;

mysql> SHOW DATABASES;
+--------------------+
| Database           |
+--------------------+
| information_schema |
| example            |
| mybook             |
| mysql              |
| performance_schema |
| test               |
+--------------------+
6 rows in set (0.00 sec)


DROP DATABASE mybook;

mysql> DROP DATABASE mybook;
Query OK, 0 rows affected (0.01 sec)


SHOW DATABASES;

mysql> SHOW DATABASES;
+--------------------+
| Database           |
+--------------------+
| information_schema |
| example            |
| mysql              |
| performance_schema |
| test               |
+--------------------+
5 rows in set (0.00 sec)
```



###### 数据库的存储引擎

```SQL
-- 示例5-3：查看MySQL支持的存储引擎类型
SHOW ENGINES;
-- SHOW ENGINES \g	--效果与';'一致
-- SHOW ENGINES \G	--可让结果更美观

mysql> SHOW ENGINES \G
*************************** 1. row ***************************
      Engine: FEDERATED
     Support: NO
     Comment: Federated MySQL storage engine
Transactions: NULL
          XA: NULL
  Savepoints: NULL
*************************** 2. row ***************************
      Engine: MRG_MYISAM
     Support: YES
     Comment: Collection of identical MyISAM tables
Transactions: NO
          XA: NO
  Savepoints: NO
*************************** 3. row ***************************
      Engine: MyISAM
     Support: YES
     Comment: MyISAM storage engine
Transactions: NO
          XA: NO
  Savepoints: NO
*************************** 4. row ***************************
      Engine: BLACKHOLE
     Support: YES
     Comment: /dev/null storage engine (anything you write to it disappears)
Transactions: NO
          XA: NO
  Savepoints: NO
*************************** 5. row ***************************
      Engine: CSV
     Support: YES
     Comment: CSV storage engine
Transactions: NO
          XA: NO
  Savepoints: NO
*************************** 6. row ***************************
      Engine: MEMORY
     Support: YES
     Comment: Hash based, stored in memory, useful for temporary tables
Transactions: NO
          XA: NO
  Savepoints: NO
*************************** 7. row ***************************
      Engine: ARCHIVE
     Support: YES
     Comment: Archive storage engine
Transactions: NO
          XA: NO
  Savepoints: NO
*************************** 8. row ***************************
      Engine: InnoDB
     Support: DEFAULT
     Comment: Supports transactions, row-level locking, and foreign keys
Transactions: YES
          XA: YES
  Savepoints: YES
*************************** 9. row ***************************
      Engine: PERFORMANCE_SCHEMA
     Support: YES
     Comment: Performance Schema
Transactions: NO
          XA: NO
  Savepoints: NO
9 rows in set (0.00 sec)


-- 示例5-4：使用"SHOW VARIABLES LIKE ‘have%’;"查询MySQL支持的存储引擎
SHOW VARIABLES LIKE 'have%';

mysql> SHOW VARIABLES LIKE 'have%';
+----------------------+----------+
| Variable_name        | Value    |
+----------------------+----------+
| have_compress        | YES      |
| have_crypt           | NO       |
| have_csv             | YES      |
| have_dynamic_loading | YES      |
| have_geometry        | YES      |
| have_innodb          | YES      |
| have_ndbcluster      | NO       |
| have_openssl         | DISABLED |
| have_partitioning    | YES      |
| have_profiling       | YES      |
| have_query_cache     | YES      |
| have_rtree_keys      | YES      |
| have_ssl             | DISABLED |
| have_symlink         | YES      |
+----------------------+----------+
14 rows in set (0.00 sec)


-- 示例5-5：使用"SHOW VARIABLES LIKE 'storage_engine';"查询默认存储引擎
SHOW VARIABLES LIKE 'storage_engine';

mysql> SHOW VARIABLES LIKE 'storage_engine';
+----------------+--------+
| Variable_name  | Value  |
+----------------+--------+
| storage_engine | InnoDB |
+----------------+--------+
1 row in set (0.00 sec)


-- InnoDB存储引擎：它给MySQL的表提供了事务、回滚、崩溃修复能力和多版本并发控制的事务安全
	-- 优点：
		-- 支持自动增长列<AUTO_INCREMENT>【注：自增列必须为主键】
		-- 支持外键<FOREIGN KEY>【注：外键所在的表为子表】
		-- 所创建的表的表结构存储在*.frm文件中，数据和索引存储在innodb_data_home_dir和innodb_data_file_path定义的表空间中
		-- 提供了良好的事务管理、崩溃修复能力和并发控制		
	-- 缺点：
		-- 读写效率差
		-- 占用的数据空间相对比较大

-- MyISAM存储引擎
    -- 所创建的表被储成3个文件，文件的名字与表名相同，扩展名分别为*.frm(存储表的结构)、*.MYD(存储数据)和*.MYI(存储索引)
    -- 所创建的表支持3中不同的存储格式，分别是静态型(MyISAM存储引擎的默认存储格式，字段是固定长度的)、动态型(字段是变长的，记录的长度不是固定的)和压缩型(需要使用myisampack工具，所占用的磁盘空间较小)
	-- 优点：
	    -- 占用空间小，处理速度快
	-- 缺点：
		-- 不支持事务的完整性和并发性

-- MEMORY存储引擎（一类特殊的存储引擎，使用存储在内存中的内容来创建表，且所有数据也都存放在内存中）
	-- 每个基于MEMORY存储引擎的表实际对应一个磁盘文件【注：文件名与表名相同，后缀为*.frm，只存储表结构，而数据文件都存储在内存中】
	-- 默认使用哈希（HASH）索引【注：速度比使用B型树（BTREE）索引快】
	-- 生命周期短，一般是一次性的
	-- 表的大小首限于max_rows和max_heap_table_size
```