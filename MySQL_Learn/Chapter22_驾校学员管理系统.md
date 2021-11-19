#### 驾校学员管理系统

用户管理：对管理员的登录进行管理

学籍信息管理：处理学籍信息的插入、查询、修改和删除

体检信息管理：对学员体检后的体检信息进行插入、查询、修改和删除

成绩成绩信息管理：对学员的成绩信息进行插入、查询、修改和删除

领证信息管理：对学员的驾驶证的领取进行管理

```SQL
-- 1、创建数据库
-- CREATE DATABASE drivingschool;
use drivingschool;


-- 2、创建表
-- > user表
DROP TABLE IF EXISTS user;
CREATE TABLE user (
	username VARCHAR(20) PRIMARY KEY UNIQUE NOT NULL,
    password VARCHAR(20) NOT NULL
);

-- > studentInfo表
DROP TABLE IF EXISTS studentInfo;
CREATE TABLE studentInfo(
	sno INT(8) PRIMARY KEY UNIQUE NOT NULL,
    sname VARCHAR(20) NOT NULL,
    sex ENUM("男", "女") NOT NULL,
    age INT(3),
    identify VARCHAR(18) UNIQUE NOT NULL,
    tel VARCHAR(15),
    car_type VARCHAR(4) NOT NULL,
    enroll_time DATE NOT NULL,
    leave_time DATE,
    scondition ENUM('学习', '结业', '退学') NOT NULL,
    s_text TEXT
);

-- >healthInfo表
DROP TABLE IF EXISTS healthInfo;
CREATE TABLE healthInfo(
	id INT(8) PRIMARY KEY UNIQUE NOT NULL AUTO_INCREMENT,
    sno INT(8) UNIQUE,
    sname VARCHAR(20) NOT NULL,
    height FLOAT,
    weight FLOAT,
    differentiate ENUM('正常', '色弱', '色盲'),
    left_sight FLOAT,
    right_sight FLOAT,
    left_ear ENUM('正常', '偏弱'),
    right_ear ENUM('正常', '偏弱'),
    legs ENUM('正常', '不相等'),
    pressure ENUM('正常', '偏高', '偏低'),
    history VARCHAR(50),
    h_text TEXT,
    CONSTRAINT health_fk FOREIGN KEY(sno)
    REFERENCES studentInfo(sno)
);

-- > courseInfo表
DROP TABLE IF EXISTS courseInfo;
CREATE TABLE courseInfo(
	cno INT(4) PRIMARY KEY UNIQUE NOT NULL,
    cname VARCHAR(20) UNIQUE NOT NULL,
    before_cour INT(4) NOT NULL DEFAULT 0
);

-- > gradeInfo表
DROP TABLE IF EXISTS gradeInfo;
CREATE TABLE gradeInfo(
	id INT(8) PRIMARY KEY UNIQUE NOT NULL AUTO_INCREMENT,
    sno INT(8) NOT NULL,
    cno INT(4) NOT NULL,
    last_time DATE,
    times INT(4) DEFAULT 1,
    grade FLOAT DEFAULT 0,
    CONSTRAINT grade_sno_fk FOREIGN KEY(sno)
    REFERENCES studentInfo(sno),
    CONSTRAINT grade_cno_fk FOREIGN KEY(cno)
    REFERENCES courseInfo(cno)
);

-- > licenseInfo表
DROP TABLE IF EXISTS licenseInfo;
CREATE TABLE licenseInfo(
	id INT(8) PRIMARY KEY UNIQUE NOT NULL AUTO_INCREMENT,
    sno INT(8) UNIQUE NOT NULL,
    sname VARCHAR(20) NOT NULL,
    lno VARCHAR(18) UNIQUE NOT NULL,
    receive_time DATE,
    receive_name VARCHAR(20),
    l_text TEXT,
    CONSTRAINT license_fk FOREIGN KEY(sno)
    REFERENCES studentInfo(sno)
);



-- 3、设计索引
-- >在studentInfo表上建立索引
CREATE INDEX index_stu_name ON studentInfo(sname);
CREATE INDEX index_car ON studentInfo(car_type);
ALTER TABLE studentInfo ADD INDEX index_con(scondition);

-- >在healthInfo表上建立索引
CREATE INDEX index_h_name ON healthInfo(sname);

-- >在licenseInfo表上建立索引
ALTER TABLE licenseInfo ADD INDEX index_license_name(sname);
ALTER TABLE licenseInfo ADD INDEX index_receive_name(receive_name);



-- 4、设计视图
CREATE VIEW grade_view
AS SELECT g.id, g.sno, s.sname, c.cname, last_time, times, grade
FROM studentInfo s, courseInfo c, gradeInfo g
WHERE g.sno=s.sno AND g.cno=c.cno;



-- 5、设计触发器
-- >设计INSERT触发器
DELIMITER &&
CREATE TRIGGER license_stu AFTER INSERT
	ON licenseInfo FOR EACH ROW
	BEGIN
		UPDATE studentInfo SET leave_time=NEW.receive_time, scondition="结业"
		WHERE sno=NEW.sno;
	END
	&&
DELIMITER;

-- >设计UPDATE触发器
DELIMITER &&
CREATE TRIGGER update_sname AFTER UPDATE
	ON studentInfo FOR EACH ROW
	BEGIN
		UPDATE healthInfo SET sname=NEW.sname WHERE sno=NEW.sno;
		UPDATE licenseInfo SET sname=NEW.sname WHERE sno=NEW.sno;
	END
	&&
DELIMITER;

-- >设计DELETE触发器
DELIMITER &&
CREATE TRIGGER delete_stu AFTER DELETE
	ON studentInfo FOR EACH ROW
	BEGIN
		DELETE FROM gradeInfo WHERE sno=OLD.sno;
		DELETE FROM healthInfo WHERE sno=OLD.sno;
		DELETE FROM licenseInfo WHERE sno=OLD.sno;
	END
	&&
DELIMITER;
```

