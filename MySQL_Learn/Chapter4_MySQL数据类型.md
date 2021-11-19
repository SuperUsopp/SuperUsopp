#### MySQL数据类型

* 整数类型（TINYINT<1>, SMALLINT<2>, MEDIUMINT<3>, INT<4>, INTEGER<4>, BIGINT<8>,<数据类型所占字节数>）

![MySQL的整数类型](.\images\MySQL的整数类型.png)

```sql
-- 示例4-1
use test;
drop table if exists chapter4_1;
create table chapter4_1(
	a tinyint zerofill, # zerofill表示数字不足的显示空间由0来填补；eg.,'001'
    b smallint,
    c mediumint,
    d int,
    e bigint
    -- 各数据的默认宽度：
    -- tinyint:4
    -- smallint:6
    -- mediumint:9
    -- int:11
    -- bigint:20
);
insert into chapter4_1 (a,b,c,d,e) values (1,2,3,4,5);

-- 示例4-2
use test;
drop table if exists chapter4_2;
create table chapter4_2 (
	a int(4), # 括号中为设置的显示宽度，当插入数据的显示宽度大于设置的显示宽度时，数据依然可以插入。但数据的宽度不能大于默认宽度
    b int
);
insert into chapter4_2 (a,b) values (111, 22222222);

-- Note:数据的宽度不能大于默认宽度。如果大于默认宽度，则该数据已经超过了改类型的最大值。
```



* 浮点数类型（FLOAT<4>，DOUBLE<8>）和定点数类型（DECIMAL<M+2>）

![MySQL的浮点数类型和定点数类型](.\images\MySQL的浮点数类型和定点数类型.png)

```SQL
-- MySQL中可以指定浮点数和定点数的精度，其基本形式为	数据类型(M,D)
-- 示例4-3
use test;
drop table if exists chapter4_3;
create table chapter4_3 (
	a float(6,2),
    b double(6,2),
    c decimal(6,2)
);
insert into chapter4_3 (a,b,c) values(3.143, 3.145, 3.1434);

-- 示例4-4
use test;
drop table if exists chapter4_4;
create table chapter4_4 (
	a float,
    b double,
    c decimal
);
insert into chapter4_4 (a,b,c) values (3.143,3.145,3.1434);
select * from chapter4_4;
```



* 日期与时间类型

![MySQL的日期与时间类型](.\images\MySQL的日期与时间类型.png)

* YEAR<1>：使用一个字节来表示年份

  ```SQL
  -- 示例4-5（使用完整年份来表示）
  use test;
  drop table if exists chapter4_5;
  create table chapter4_5 (
  	a year
  );
  insert into chapter4_5 (a) values (1997);
  insert into chapter4_5 (a) values (1998);
  insert into chapter4_5 (a) values (1900);
  select * from chapter4_5;
  
  -- 示例4-6(使用2位字符串来表示)
  use test;
  drop table if exists chapter4_5;
  create table chapter4_5 (
  	a year
  );
  insert into chapter4_5 (a) values ("24");
  insert into chapter4_5 (a) values ("86");
  insert into chapter4_5 (a) values ("0");
  insert into chapter4_5 (a) values ("00");
  select * from chapter4_5;
  
  -- 示例4-7（使用2位数字来表示）
  use test;
  drop table if exists chapter4_7;
  create table chapter4_7 (
  	a year
  );
  insert into chapter4_7 (a) values (24);
  insert into chapter4_7 (a) values (86);
  insert into chapter4_7 (a) values (0);
  insert into chapter4_7 (a) values (00);
  select * from chapter4_7;
  ```

  

* TIME类型<3>

  ```SQL
  -- 示例4_8('D HH:MM:SS'格式字符串表示法)
  use test;
  drop table if exists chapter4_8;
  create table chapter4_8 (
  	a time
  );
  insert into chapter4_8 (a) values ('2 23:50:50');
  insert into chapter4_8 (a) values ('22:22:22');
  insert into chapter4_8 (a) values ('11:11');
  insert into chapter4_8 (a) values ('2 20:20');
  insert into chapter4_8 (a) values ('2 20');
  insert into chapter4_8 (a) values ('30');
  select * from chapter4_8;
  
  -- 示例4_9("HHMMSS"格式的字符串或HHMMSS格式的数值表示法)
  use test;
  drop table if exists chapter4_9;
  create table chapter4_9 (
  	a time
  );
  insert into chapter4_9 (a) values ("112233");
  insert into chapter4_9 (a) values (122334);
  select * from chapter4_9;
                               
  -- 示例4_10(使用CURRENT_TIME或NOW()输入当前系统时间)                             
  use test;
  drop table if exists chapter4_10;
  create table chapter4_10 (
  	a time
  );
  insert into chapter4_10 (a) values (CURRENT_TIME);
  insert into chapter4_10 (a) values (now());
  select * from chapter4_10;
  ```

  

* DATE类型<4>

  ```SQL
  -- 示例4_11（‘YYYY-MM-DD’或‘YYYYMMDD’格式的字符串表示DATE）
  use test;
  drop table if exists chapter4_11;
  create table chapter4_11 (
  	a date
  );
  insert into chapter4_11 (a) values ("2021-06-01");
  insert into chapter4_11 (a) values (20210601);
  select * from chapter4_11;
  # MySQL中还支持一些不严格的语法格式，任何标点都可以用来做间隔符号。
  
  -- 示例4_12（“YY-MM-DD”或“YYMMDD”格式的字符串表示DATE）
  use test;
  drop table if exists chapter4_12;
  create table chapter4_12 (
      a date
  );
  insert into chapter4_12 (a) values ("21-06-01");
  insert into chapter4_12 (a) values (210601);
  select * from chapter4_12; 
  
  -- 示例4_13(YYYYMMDD或YYMMDD格式的数字表示)
  use test;
  drop table if exists chapter4_13;
  create table chapter4_13 (
  	a date
  );
  insert into chapter4_13 (a) values (20210601);
  insert into chapter4_13 (a) values (210601);
  select * from chapter4_13;
  
  -- 示例4_14(使用CURRENT_DATE或NOW()来输入当前系统日期)
  use test;
  drop table if exists chapter4_14;
  create table chapter4_14 (
  	a date
  );
  insert into chapter4_14 (a) values (CURRENT_DATE);
  insert into chapter4_14 (a) values (now());
  select * from chapter4_14;
  ```

* DATETIME<8>类型

```SQL
-- 示例4_15('YYYY-MM-DD HH:MM:SS'或'YYYYMMDDHHMMSS'格式的字符串表示)
use test;
drop table if exists chapter4_15;
create table chapter4_15 (
	a datetime
);
insert into chapter4_15 (a) values ("2021-06-01 12:01:02");
insert into chapter4_15 (a) values ('20210601120203');
select * from chapter4_15;

-- 示例4_16('YY-MM-DD HH:MM:SS'或"YYMMDDHHMMSS"格式的字符串表示)
use test;
drop table if exists chapter4_16;
create table chapter4_16(
	a datetime
);
insert into chapter4_16 (a) values ('21-06-01 12:03:04');
insert into chapter4_16 (a) values ('210601120506');
select * from chapter4_16;

-- 示例4_17(YYYYMMDDHHMMSS或YYMMDDHHMMSS格式的数字表示)
use test;
drop table if exists chapter4_17;
create table chapter4_17 (
	a datetime
);
insert into chapter4_17 (a) values (20210601120607);
insert into chapter4_17 (a) values (210601120607);
select * from chapter4_17;
```

* TIMESTAMP类型

* 字符串类型

  * CHAR类型和VARCHAR类型

  ```SQL
  字符串类型(M)
  ```

  * TEXT类型(TINYTEXT, TEXT, MEDIUMTEXT & LONGTEXT)

  ![各种TEXT类型的对比](.\images\各种TEXT类型的对比.PNG)

  * ENUM类型

  ```SQL
  属性名 ENUM('值1', '值2', ..., '值n')
  ```

  * SET类型

  ```SQL
  属性名 SET('值1', '值2', ..., '值n')
  ```

* 二进制类型（BINARY, VARBINARY, BIT, TINYBLOB, BLOB, MEDIUMBLOB & LONGBLOB）

  ![MySQL的二进制类型](.\images\MySQL的二进制类型.png)
