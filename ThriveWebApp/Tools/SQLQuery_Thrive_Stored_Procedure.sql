-----Tables----

CREATE PROC uspCreateUser
AS
CREATE TABLE tblUser (
  FirstName varchar (50),
  LastName varchar (50),
  Email varchar (100) NOT NULL PRIMARY KEY,
  Pass varchar (max),  
  Addres varchar (50),
  City varchar (50),
  County varchar (50),
  Country varchar (50),
  Phone varchar (30)
);
GO

CREATE PROC uspCreateItem
AS
CREATE TABLE Item (
  ItemId varchar(50) NOT NULL PRIMARY KEY,
  ItemType varchar (20)
);
GO

CREATE PROC uspCreatePhoneCase
AS
CREATE TABLE PhoneCase (
  ProductCode varchar(50) NOT NULL PRIMARY KEY,
  PhonceCaseMakerModel varchar(50),
  PhoneCaseName varchar (100) UNIQUE,
  PhoneCasePrice decimal (5,2),
  InstructionManual xml,
  PhonceCaseImage varchar(50),
 CONSTRAINT FK_PhoneCase_Items FOREIGN KEY (ItemId) REFERENCES Item(ItemId) 
);
GO

CREATE PROC uspCreatePhoneCase1
AS
CREATE TABLE PhoneCase1 (
  ProductCode varchar (50) NOT NULL PRIMARY KEY,
  PhonceCaseMakerModel varchar(50),
  PhoneCaseName varchar (100) UNIQUE,
  PhoneCasePrice decimal (5,2),
  InstructionManual xml,
  PhonceCaseImage varbinary (max)
 );
GO

CREATE PROC uspCreateTransaction
AS
CREATE TABLE tblTransaction (
  TransactionId varchar(150) NOT NULL PRIMARY KEY,
  TransactionDate datetime,
  TransactionPrice decimal(5,2),
  Email varchar (100),
  CONSTRAINT FK_tblTransaction_tblUser FOREIGN KEY (Email) REFERENCES tblUser(Email)
);
GO

CREATE PROC uspCreateTranscationItem
AS
CREATE TABLE TranscationItem (
  TransactionId varchar(150),
  ItemId varchar(50),
  ItemQuantity int
  PRIMARY KEY(TransactionId, ItemId)
  CONSTRAINT FK_TransactionItem_tblTransaction FOREIGN KEY (TransactionId) REFERENCES tblTransaction (TranscationId),
  CONSTRAINT FK_TransactionItem_Items FOREIGN KEY (ItemId) REFERENCES Items(ItemId) 
);
GO

--CREATE PROC uspCreateCustomersX
--AS
--CREATE TABLE CustomersX (
--  CustomerFirstName varchar (30),
--  CustomerLastName varchar (30),
--  CustomerEmail varchar (100) NOT NULL PRIMARY KEY,
--  CustomerAddress varchar (30),
--  CustomerCity varchar (30),
--  CustomerCounty varchar (30),
--  CustomerCountry varchar (30),
--  CustomerPhone int,
--  CustomerPassword varchar (30)
--);
--GO

--CREATE PROC uspCreateCustomer
--AS
--CREATE TABLE Customer (
--  CustomerId char(4) NOT NULL PRIMARY KEY,
--  Email varchar (100) NOT NULL,
--  CustomerAddress varchar (50),
--  CustomerCity varchar (50),
--  CustomerCounty varchar (50),
--  CustomerCountry varchar (50),
--  CustomerPhone varchar (50)
--  CONSTRAINT FK_Customer_tblUser FOREIGN KEY (Email) REFERENCES tblUser(Email)
--);
--GO


---tblUser features---
CREATE PROC uspCheckLogin
@email varchar (100)
AS
SELECT FirstName, Pass FROM tblUser
WHERE Email = @email
GO

CREATE PROC uspInserttblUser 
@firstName varchar (50),
@lastName varchar (50),
@email varchar (100),
@pass varchar (max),
@addres varchar (50),
@city varchar (50),
@county varchar (50),
@country varchar (50),
@phone varchar (30)
AS
INSERT INTO tblUser VALUES(@firstName,@lastName,@email,@pass,@addres,@city,@county,@country,@phone)

----tblTransaction Features----
CREATE PROC uspInserttblTransaction
@transactionId varchar(150),
@transactionDate datetime,
@transactionPrice decimal(5,2),
@email varchar (100)
AS
INSERT INTO tblTransaction VALUES (@transactionId,@transactionDate ,@transactionPrice,@email)
GO


---Customer features---

CREATE PROC uspDeleteCustomer
@customerId char(4)
AS
DELETE FROM Customer WHERE Customer.CustomerId = @customerId 
GO

CREATE PROC uspInsertIntoCustomer
@id char(4),
@email varchar (100),
@address varchar (50),
@city varchar (50),
@county varchar (50),
@country varchar (50),
@phone varchar (50)
AS
INSERT INTO Customer VALUES(@id, @email,@address ,@city,@county,@country,@phone)
GO

CREATE PROC uspShowAllCustomer
AS
SELECT FROM Customer 
GO

----instruction Manual Features----


----PhoneCase Features----

CREATE PROC uspAllPhoneCase
AS
SELECT * FROM PhoneCase
GO


CREATE PROC uspDeletePhoneCase
@productcode varchar(50)
AS
DELETE FROM PhoneCase WHERE PhoneCase.ProductCode = @productcode 
GO

CREATE PROC uspFindPhoneCase
@productcode varchar(50)
AS
SELECT * FROM PhoneCase WHERE PhoneCase.ProductCode = @productcode 
GO

CREATE PROC uspFindPhoneCaseByName
@phoneCaseName varchar(100)
AS
SELECT * FROM PhoneCase WHERE PhoneCase.PhoneCaseName = @phoneCaseName 
GO

CREATE PROC uspInsertPhoneCase
@productCode varchar(50),
@phonceCaseMakerModel varchar(50),
@phoneCaseName varchar (100),
@phoneCasePrice decimal (5,2),
@instructionManual xml,
@phonceCaseImage varchar(50)
AS
INSERT INTO PhoneCase VALUES (@productCode,@phonceCaseMakerModel,@phoneCaseName,@phoneCasePrice,@instructionManual,@phonceCaseImage)
GO

----PhoneCase1 Features----

CREATE PROC uspAllPhoneCase1
AS
SELECT * FROM PhoneCase1
GO

CREATE PROC uspDeletePhoneCase1
@productcode varchar(50)
AS
DELETE FROM PhoneCase1 WHERE PhoneCase1.ProductCode = @productcode 
GO

CREATE PROC uspFindPhoneCase1
@productcode varchar(50)
AS
SELECT * FROM PhoneCase1 WHERE PhoneCase1.ProductCode = @productcode 
GO

CREATE PROC uspFindPhoneCaseName1
@phoneCaseName varchar(100)
AS
SELECT * FROM PhoneCase1 WHERE PhoneCase1.PhoneCaseName = @phoneCaseName 
GO

CREATE PROC uspFindPhoneCase1ByName
@phoneCaseName varchar(100)
AS
SELECT * FROM PhoneCase1 WHERE PhoneCase1.PhoneCaseName = @phoneCaseName 
GO

CREATE PROC uspInsertPhoneCase1
@productCode varchar(50),
@phonceCaseMakerModel varchar(50),
@phoneCaseName varchar (100),
@phoneCasePrice decimal (5,2),
@instructionManual xml,
@phonceCaseImage varchar(50)
AS
INSERT INTO PhoneCase1 VALUES (@productCode,@phonceCaseMakerModel,@phoneCaseName,@phoneCasePrice,@instructionManual,@phonceCaseImage)
GO









