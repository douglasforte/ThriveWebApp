CREATE PROCEDURE uspGet_tblComments
AS
BEGIN
    SELECT * FROM tblComments
END
GO

CREATE PROCEDURE uspGet_tblCustomers
AS
BEGIN
  SELECT CustomerEmail, CustomerFirstName, CustomerLastName, CustomerAddress, CustomerCity, CustomerCounty, CustomerPhone FROM tblCustomers
END
GO

CREATE PROCEDURE uspGet_tblItems
AS
BEGIN
    SELECT * FROM tblItems
END
GO

CREATE PROCEDURE uspGet_tblOrderItems
AS
BEGIN
    SELECT * FROM tblOrderItems
END
GO

CREATE PROCEDURE uspGet_tblOrders
AS
BEGIN
    SELECT * FROM tblOrders
END
GO

CREATE PROCEDURE uspGet_tblPhoneCaseMakers
AS
BEGIN
    SELECT * FROM tblPhoneCaseMakers
END
GO

CREATE PROCEDURE uspGet_tblPhoneCases
AS
BEGIN
    SELECT * FROM tblPhoneCases
END
GO

