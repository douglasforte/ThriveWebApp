CREATE PROC uspCreateStudent
AS
CREATE TABLE Student
(
StudentId CHAR(4),
StudentName VARCHAR(20),
ContactNo VARCHAR(20)
)

EXEC uspCreateStudent