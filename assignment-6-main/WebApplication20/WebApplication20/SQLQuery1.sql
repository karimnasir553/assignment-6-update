CREATE DATABASE StudentDB;
GO
USE StudentDB;
GO 

--making required tables--
CREATE TABLE BooksData (
    bookID int,
    bookName varchar(255),
	shelfNumber varchar(255),
	category varchar(255),
	price int,
	bookIssuedStatus varchar(255)
    
);

CREATE TABLE UserData (
    userID int,
    userName varchar(255),
	bookIssuedList varchar(255),    
);

CREATE TABLE Penalty(
userID int,
bookcCheckIn DATETIME,
BookReceived DATETIME

);

--insert data into tables--

INSERT INTO Penalty(userID,bookcCheckIn,BookReceived)
Values(1,'2022-10-11','2022-11-11');
INSERT INTO Penalty(userID,bookcCheckIn,BookReceived)
Values(2,'2022-9-11','2022-10-11');
INSERT INTO Penalty(userID,bookcCheckIn,BookReceived)
Values(3,'2022-8-11','2022-11-11');
INSERT INTO Penalty(userID,bookcCheckIn,BookReceived)
Values(4,'2022-11-1','2022-11-11');
INSERT INTO Penalty(userID,bookcCheckIn,BookReceived)
Values(5,'2022-5-11','2022-11-11');
INSERT INTO Penalty(userID,bookcCheckIn,BookReceived)
Values(6,'2022-7-7','2022-10-11');
INSERT INTO Penalty(userID,bookcCheckIn,BookReceived)
Values(7,'2022-11-8','2022-10-11');



INSERT INTO UserData(userID, userName, bookIssuedList)
Values(1,'Ahmad', 'In Search of Lost Time');

INSERT INTO UserData(userID, userName, bookIssuedList)
Values(2,'Akbar', 'War and Peace');

INSERT INTO UserData(userID, userName, bookIssuedList)
Values(3,'Zubair', 'Invisible Man');

INSERT INTO UserData(userID, userName, bookIssuedList)
Values(4,'Matt', 'The Stand');

INSERT INTO UserData(userID, userName, bookIssuedList)
Values(5,'Tom', 'Kite Runner');

INSERT INTO UserData(userID, userName, bookIssuedList)
Values(6,'Jack', 'The Great Gatsby');

INSERT INTO UserData(userID, userName, bookIssuedList)
Values(7,'Zafar', 'The Talent Code');


INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (1, 'In Search of Lost Time','F12','Action',450,'yes');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (2, 'The Great Gatsby','F12','Fiction',1450,'yes');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (3, 'Wuthering Heights','F13','Adventure',650,'no');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (4, 'Catcher in the Rye','D12','Action',850,'no');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (5, 'War and Peace','E12','Horror',950,'yes');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (6, 'In Search of Lost Time','F12','Action',450,'no');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (7, 'Invisible Man','F12','Action',450,'yes');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (8, 'In Search of Found Time','F17','Action',850,'no');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (9, 'Crime and Punishment','F17','Action',850,'no');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (10, 'To kill a mocking Bird','F19','Adventure',850,'no');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (11, 'The Talent Code','F12','Action',450,'yes');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (12, 'Relentless','F12','Fiction',1450,'no');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (13, 'The Mindful Athlete','F13','Adventure',650,'no');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (14, 'The kite runner','D12','Action',850,'yes');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (15, 'The giving tree','E12','Horror',950,'no');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (16, 'Storm of swords','F12','Action',450,'no');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (17, 'Invisible Man','F12','Action',400,'no');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (18, 'Visible Man','F17','Action',1050,'no');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (19, 'The Stand','F17','Action',800,'yes');

INSERT INTO BooksData(bookID, bookName, shelfNumber,category,price,bookIssuedStatus)
VALUES (20, 'The return of the King','F19','Adventure',150,'yes');




GO;


--Creating Procedures

--reading all the data--
CREATE PROCEDURE SelectAllBooksData
AS
SELECT * FROM BooksData

GO;
CREATE PROCEDURE PenaltyData
AS
SELECT * FROM Penalty

GO;
CREATE PROCEDURE SelectAllUsersData
AS
SELECT * FROM UserData
GO;


--adding data procedures--

--TASK1--
CREATE PROCEDURE AddBook
    @bookID int,
    @bookName varchar(255),
	@shelfNumber varchar(255),
	@category varchar(255),
	@price int,
	@bookIssuedStatus varchar(255)
AS
INSERT INTO BooksData (bookID ,bookName,shelfNumber, category,price,bookIssuedStatus) VALUES (@bookID, @bookName,@shelfNumber,@category,@price,@bookIssuedStatus) 

GO;
--TASK2--
CREATE PROCEDURE AddUser
    @userID int,
    @userName varchar(255),
	@bookIssuedList varchar(255)
AS
INSERT INTO UserData (userID, userName, bookIssuedList) VALUES (@userID, @userName,@bookIssuedList)

GO;




--updating data procedures--


--TASK 5--
CREATE PROCEDURE UpdateBook
    @bookID int,
    @bookName varchar(255),
	@shelfNumber varchar(255),
	@category varchar(255),
	@price int,
	@bookIssuedStatus varchar(255)
AS
UPDATE BooksData SET bookID=@bookID ,bookName=@bookName,shelfNumber=@shelfNumber,category=@category,price=@price,bookIssuedStatus=@bookIssuedStatus WHERE bookID=@bookID


GO;
--TASK 6--
CREATE PROCEDURE UpdateUser
    @userID int,
    @userName varchar(255),
	@bookIssuedList varchar(255)
AS
UPDATE UserData SET userID=@userID, userName=@userName, bookIssuedList=@bookIssuedList WHERE userID=@userID

GO;
--TASK 7--
CREATE PROCEDURE IssueBook
    @userID int,
	@bookIssuedList varchar(255),
	@bookIssuedStatus varchar(255),
	@bookID int
AS
UPDATE UserData SET bookIssuedList=@bookIssuedList WHERE userID=@userID
UPDATE BooksData SET bookIssuedStatus=@bookIssuedStatus WHERE bookID=@bookID

GO;

CREATE PROCEDURE ReturnBook
    @userID int,
	@bookIssuedList varchar(255),
	@bookIssuedStatus varchar(255),
	@bookID int
AS
UPDATE UserData SET bookIssuedList=@bookIssuedList WHERE userID=@userID
UPDATE BooksData SET bookIssuedStatus=@bookIssuedStatus WHERE bookID=@bookID

GO;





--deleting data procedures--

--TASK 8--
CREATE PROCEDURE DeleteBook
@bookID int
AS
DELETE FROM BooksData WHERE bookID=@bookID

GO;
--TASK 9--
CREATE PROCEDURE DeleteUser
@userID int
AS
DELETE FROM UserData WHERE userID=@userID



GO;




SELECT DB_NAME() AS [Current Database]

Select *  from sysservers;

DROP PROCEDURE SelectAllBooksData
DROP PROCEDURE UpdateBook
DROP PROCEDURE ReturnBook

select @@SERVERNAME

DROP TABLE BooksData;

DELETE FROM BooksData
DELETE FROM UserData
DELETE FROM Penalty
