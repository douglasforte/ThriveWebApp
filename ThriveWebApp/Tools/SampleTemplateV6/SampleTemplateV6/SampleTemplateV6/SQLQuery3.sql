CREATE PROC uspCreateItem
AS
CREATE TABLE Item(
ItemId varchar(50) NOT NULL PRIMARY KEY,
ItemType varchar(20)
)
GO
EXEC uspCreateItem
GO
CREATE PROC uspCreateCourse
AS
CREATE TABLE Course(
CourseCode varchar(50),
CourseTitle varchar(100) UNIQUE,
CourseType varchar(20),
CoursePrice decimal(5,2)
FOREIGN KEY(CourseCode) REFERENCES Item(ItemId),
PRIMARY KEY(CourseCode)
)
GO
REATE PROC uspCreateModule
AS
CREATE TABLE Module(
ModuleCode varchar(50) NOT NULL PRIMARY KEY,
ModuleTitle varchar(100),
ModuleHours int,
ModuleSyllabus xml,
CourseCode varchar(50),
FOREIGN KEY(CourseCode) REFERENCES Course(CourseCode)
)
GO
CREATE PROC uspCreateBook
AS
CREATE TABLE Book(
ISBN varchar(50),
Title varchar(100),
Publisher varchar(50),
DatePublished date,
Price decimal(5,2),
BookImage varchar(50),
FOREIGN KEY (ISBN) REFERENCES Item(ItemId),
PRIMARY KEY(ISBN)
)