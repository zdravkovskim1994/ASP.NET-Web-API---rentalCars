CREATE DATABASE CarRentalDB;

USE CarRentalDB;


BEGIN--Tables
CREATE TABLE Cars(
CarID INT IDENTITY(1,1) NOT NULL,
TagNumber varchar(20) NULL,
Model varchar(50) NULL,
CarYear SMALLINT NULL,
AirConditioner BIT NULL,
Daily int NULL,
Monthly int NULL,
PRIMARY KEY (CarID)
);


CREATE TABLE Customers(
CustomerID INT IDENTITY(1,1) NOT NULL,
DrvLicNumber varchar(50) NULL,
[FullName] [varchar](50) NULL,
[Address] [varchar](50) NULL,
[Country] [varchar](50) NULL,
[City] [varchar](50) NULL,
PRIMARY KEY (CustomerID)
);


CREATE TABLE RentalOrders(
RentalOrderID INT IDENTITY(1,1) NOT NULL,
CustomerID INT NOT NULL,
CarID INT NOT NULL,
RentStartDate datetime NULL,
RentEndDate datetime NULL,
FOREIGN KEY (CarID) REFERENCES Cars(CarID),
FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);


CREATE TABLE UserLogin(
	Id int IDENTITY(1,1) NOT NULL,
	UserName varchar(50) NULL,
	Password varchar(50) NULL,
	Email varchar(50) NULL,
	Role varchar(50) NULL,
    PRIMARY KEY (Id)
);





END



BEGIN--INSERTING TABLES
--INSERT CARS
INSERT Cars (TagNumber, Model, CarYear, AirConditioner, Daily, Monthly)
VALUES ('SK 3341 SS', 'WV-Golf', 2015, 1, 2500, 15000);

INSERT Cars (TagNumber, Model, CarYear, AirConditioner, Daily, Monthly)
VALUES ('SK 3341 SS', 'Opel-Corsa', 2013, 0, 1500, 12000);

INSERT Cars (TagNumber, Model, CarYear, AirConditioner, Daily, Monthly)
VALUES ('BT 3341 SS', 'Audi-A3', 2019, 1, 4000, 25000);

INSERT Cars (TagNumber, Model, CarYear, AirConditioner, Daily, Monthly)
VALUES ('BT 1231 AB', 'Mercedes-Cls', 2018, 1, 5000, 25000);

INSERT Cars (TagNumber, Model, CarYear, AirConditioner, Daily, Monthly)
VALUES ('SK 3341 AS', 'Audi-A4', 2018, 1, 6500, 27300);


--INSERT CUSTOMERS
INSERT Customers(DrvLicNumber,FullName,Address,Country,City)
VALUES ('A885','John Wattson','Mimosa 79','USA','Washington');

INSERT Customers(DrvLicNumber,FullName,Address,Country,City)
VALUES ('A134','Charles Calhoun','Leila 64','USA','Alexandria');

INSERT Customers(DrvLicNumber,FullName,Address,Country,City)
VALUES ('A532','Mario Zdravkovski','Filip Vtori Makedonski 98','Macedonia','Bitola');

INSERT Customers(DrvLicNumber,FullName,Address,Country,City)
VALUES ('A893','Aleksandar Stojanovski','Boris Bastero 22','Macedonia','Skopje');

INSERT Customers(DrvLicNumber,FullName,Address,Country,City)
VALUES ('A520','Kevin Allen','Historic Road 66','USA','Guntersville');





--INSERT ORDERS
INSERT RentalOrders (CustomerID,CarID,RentStartDate,RentEndDate)
VALUES (1, 1, CAST('2020-01-01T16:40:18.420' AS DateTime), CAST('2020-02-10T09:37:31.580' AS DateTime))

INSERT RentalOrders (CustomerID,CarID,RentStartDate,RentEndDate)
VALUES (2, 5, CAST('2020-01-01T16:40:18.420' AS DateTime), CAST('2020-02-01T09:37:31.580' AS DateTime))

INSERT RentalOrders (CustomerID,CarID,RentStartDate,RentEndDate)
VALUES (3, 4, CAST('2019-01-01T16:40:18.420' AS DateTime), CAST('2019-02-01T09:37:31.580' AS DateTime))



--INSERT Users
INSERT UserLogin (UserName, Password, Email, Role)
VALUES ('admin', '123456', 'admin@gmail.com', 'admin')

INSERT UserLogin (UserName, Password, Email, Role)
VALUES ('user123', '123456', 'user123@gmail.com', 'user')


END



BEGIN--STORE PROCEDURE CAR 
CREATE PROCEDURE Delete_car
@carId int
AS
	DELETE FROM Cars
	WHERE CarID = @carId;
	



CREATE PROCEDURE GetCar
@id int
AS
	SELECT CarID,TagNumber,Model,CarYear,AirConditioner,Daily,Monthly
	FROM Cars
	WHERE CarID = @id;




CREATE PROCEDURE GetAllCars
AS
SELECT c.CarID,c.TagNumber,c.Model,c.CarYear,c.AirConditioner,c.Daily,c.Monthly,r.RentStartDate,r.RentEndDate
FROM Cars c
LEFT JOIN RentalOrders r on c.CarID = r.CarID



--Available car
CREATE PROCEDURE AvailableCars
AS
SELECT TagNumber,Model,CarYear,AirConditioner,Daily,Monthly
FROM Cars c
FULL JOIN RentalOrders r on c.CarID = r.CarID
WHERE RentStartDate is NULL
OR
(RentStartDate is not null AND RentEndDate is NOT NULL)



CREATE PROCEDURE InsertCar
@tagNumber varchar(20),
@model varchar(50),
@carYear SMALLINT,
@AirConditioner BIT,
@daily int,
@monthly int
AS
	INSERT INTO Cars(TagNumber, Model, CarYear, AirConditioner, Daily, Monthly)
	VALUES (@tagNumber,@model,@carYear,@AirConditioner,@daily,@monthly);



	
CREATE PROCEDURE UpdateCarRates
@id int,
@daily int,
@monthly int
AS
	UPDATE Cars
	SET Daily = @daily, Monthly = @monthly
	WHERE CarID = @id;



CREATE PROCEDURE SearchCarByName
@searchTerm NVARCHAR(30)
AS
SELECT *
FROM Cars
WHERE Model LIKE '%' + @SearchTerm + '%'



END




BEGIN--SP CUSTOMERS
CREATE PROCEDURE DeleteCustomer
@id int
AS
	DELETE FROM Customers
	WHERE CustomerID = @id;


CREATE PROCEDURE GetAllCustomers
AS
    SELECT CustomerID,DrvLicNumber,FullName,Address,Country,City
    FROM Customers;



CREATE PROCEDURE GetCustomer
@id int
AS
	SELECT CustomerID,DrvLicNumber,FullName,Address,Country,City
	FROM Customers
	WHERE CustomerID = @id;
  

CREATE PROCEDURE InsertCustomer
@drvLicNumber varchar(50),
@fullName [varchar](50),
@address [varchar](50),
@country [varchar](50),
@city [varchar](50)
AS
	INSERT INTO Customers(DrvLicNumber,FullName,Address,Country,City)
	VALUES (@drvLicNumber,@fullName,@address,@country,@city);


	
CREATE PROCEDURE UpdateCustomerAddress
@id int,
@address [varchar](50)
AS
	UPDATE Customers
	SET Address = @address
	WHERE CustomerID = @id;



CREATE PROCEDURE RentStart
@customerId int,
@carId int
AS
   INSERT INTO RentalOrders (CustomerID, CarID,RentStartDate)
   VALUES (@customerId, @carId, GETDATE());








CREATE PROCEDURE RentEnd
@customerId int,
@carId int
AS
   UPDATE RentalOrders
   SET RentEndDate = GETDATE()
   WHERE CustomerID = @customerId AND CarID = @carId



END




BEGIN--STORE PROCEDURE RentalOrders

CREATE PROCEDURE GetRentalOrders
AS
SELECT RentalOrderID,CustomerID,CarID,RentStartDate,RentEndDate
FROM RentalOrders




CREATE PROCEDURE IsCarRented
@carId int
AS
	SELECT *
	FROM RentalOrders
	WHERE CarID = @carId AND RentStartDate IS NOT NULL AND RentEndDate IS NULL;



	

END



BEGIN--STORE PROCEDURE UserLogin

CREATE PROCEDURE UserLoginSp  
@username varchar(50)=null,  
@password varchar(50)=null
AS
    SELECT UserName, Password, Email, Role
	from UserLogin
	where UserName=@username and password=@password 




END




--TEST

INSERT INTO RentalOrders (CustomerID, CarID,RentStartDate)
   VALUES (2, 3, GETDATE());
