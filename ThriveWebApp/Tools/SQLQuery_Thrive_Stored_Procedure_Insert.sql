CREATE PROCEDURE uspINSERT_tblCustomers
@CustomerEmail varchar (20),
@CustomerFirstName varchar (30),
@CustomerLastName varchar (30),
@CustomerAddress varchar (30),
@CustomerCity varchar (30),
@CustomerCounty varchar (30),
@CustomerCountry varchar (30),
@CustomerPhone int,
@CustomerPassword varchar (30)
AS
BEGIN
	INSERT INTO tblCustomers VALUES (@CustomerEmail, @CustomerFirstName, @CustomerLastName, @CustomerAddress, @CustomerCity, @CustomerCounty, @CustomerCountry, @CustomerPhone, @CustomerPassword)
END
GO

CREATE PROCEDURE uspINSERT_tblComments
@InquiryID int,
@CustomerEmail varchar (20),
@Query varchar (200)
AS
BEGIN
	INSERT INTO tblComments VALUES (@InquiryID, @CustomerEmail, @Query)
END
GO

CREATE PROCEDURE uspINSERT_tblItems
@ItemID int,
@ItemType varchar (20)
AS
BEGIN
	INSERT INTO tblItems VALUES (@ItemID, @ItemType)
END
GO

CREATE PROCEDURE uspINSERT_tblPhoneCaseMakers
@PhonceCaseMakerID int,
@ItemID int,
@PhonceCaseMakerName varchar (20),
@PhonceCaseMakerModel varchar (20)
AS
BEGIN
	INSERT INTO tblPhoneCaseMakers VALUES (@PhonceCaseMakerID, @ItemID, @PhonceCaseMakerName, @PhonceCaseMakerModel)
END
GO

