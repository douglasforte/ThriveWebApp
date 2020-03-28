CREATE TABLE tblCustomers (
  CustomerEmail varchar (20) NOT NULL PRIMARY KEY,
  CustomerFirstName varchar (30),
  CustomerLastName varchar (30),
  CustomerAddress varchar (30),
  CustomerCity varchar (30),
  CustomerCounty varchar (30),
  CustomerCountry varchar (30),
  CustomerPhone int,
  CustomerPassword varchar (30)
);

CREATE TABLE tblComments (
  InquiryID int NOT NULL PRIMARY KEY,
  CustomerEmail varchar (20),
  Query varchar (200)
  CONSTRAINT FK_tblCustomers_tblComments FOREIGN KEY (CustomerEmail) REFERENCES tblCustomers (CustomerEmail) 
);

CREATE TABLE tblItems (
  ItemID int NOT NULL PRIMARY KEY,
  ItemType varchar (20)
);

CREATE TABLE tblPhoneCaseMakers (
  PhonceCaseMakerID int NOT NULL PRIMARY KEY,
  ItemID int,
  PhonceCaseMakerName varchar (20),
  PhonceCaseMakerModel varchar (20),
  CONSTRAINT FK_tblPhoneCaseMakers_tblItems FOREIGN KEY (ItemID) REFERENCES tblItems(ItemID) 
);

CREATE TABLE tblPhoneCases (
  PhoneCaseID int NOT NULL PRIMARY KEY,
  PhonceCaseMakerID int,
  PhoneCaseName varchar (50),
  PhoneCasePrice float (53),
  PhoneCaseDescription varchar (200),
  InstructionManual xml,
  PhonceCaseImage image,
  CONSTRAINT FK_tblPhonceCaseMakers_tblPhonceCases FOREIGN KEY (PhonceCaseMakerID) REFERENCES tblPhoneCaseMakers(PhonceCaseMakerID) 
);

CREATE TABLE tblOrders (
  OrderID int NOT NULL PRIMARY KEY,
  CustomerEmail varchar (20),
  OrderDate datetime,
  TotalAmount float (53),
  CONSTRAINT FK_tblOrders_tblCustomers FOREIGN KEY (CustomerEmail) REFERENCES tblCustomers(CustomerEmail) 
);

CREATE TABLE tblOrderItems (
  OrderItemID int NOT NULL PRIMARY KEY,
  OrderID int,
  ItemID int, 
  CONSTRAINT FK_tblOrderItems_tblOrders FOREIGN KEY (OrderID) REFERENCES tblOrders(OrderID),
  CONSTRAINT FK_tblOrderItems_tblItems FOREIGN KEY (ItemID) REFERENCES tblItems(ItemID) 
);







