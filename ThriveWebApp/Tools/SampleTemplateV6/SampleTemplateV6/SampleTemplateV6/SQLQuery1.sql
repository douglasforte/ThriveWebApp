ALTER PROC uspCreateBook1
AS
CREATE TABLE Book1(
ISBN varchar(50),
Title varchar(100),
Publisher varchar(50),
DatePublished date,
Price decimal(5,2),
BookImage varbinary(max),
PRIMARY KEY(ISBN)
)
GO
EXEC dbo.uspCreateBook1
GO
DECLARE @img AS VARBINARY(MAX)
SET @img = (SELECT BulkColumn FROM OPENROWSET(BULK'\\Client\C$\Images\CPlus.png', SINGLE_BLOB)AS MyFile)
INSERT INTO Book1 VALUES ('13232732','C++ Programming','Wiley','2016-09-08',45.00,@img)
           --   new Book("47584745","Java Programming","Orla Kelly",new DateTime(2017,01,02),34.00m,"Java.png"),
            --    new Book("243284783","Intro to Programming","John Murphy",new DateTime(2016,07,03),50.00m,"Programming.png"),
            --    new Book("65765768","Python Programming","Pat Smith",new DateTime(2015,06,12),45.00m,"Python.png"),