CREATE DATABASE Hotel; 
GO 

USE Hotel 
GO 

CREATE TABLE Category 
( 
	id INT NOT NULL IDENTITY(1, 1),
	name NVARCHAR(50) NOT NULL,
	TV BIT NOT NULL,
	conditioner BIT NOT NULL,
	internet BIT NOT NULL,
	jacuzzi BIT NOT NULL,
	CONSTRAINT PK_Category_Id PRIMARY KEY(id) 
);
GO 

CREATE TABLE Room 
( 
	id INT NOT NULL, 
	category INT NOT NULL, 
	places INT NOT NULL, 
	price INT NOT NULL, 
	is_free BIT NOT NULL DEFAULT 1, 
	CONSTRAINT PK_Room_Id PRIMARY KEY(id), 
	CONSTRAINT FK_Room_ñategory_Category_Id FOREIGN KEY(category) REFERENCES Category(id)
);
GO

CREATE TABLE Guest
(
	id INT NOT NULL IDENTITY(1, 1),
	name NVARCHAR(50) NOT NULL,
	passport NVARCHAR(50) NOT NULL,
	room INT NOT NULL,
	check_in_date DATE NOT NULL DEFAULT GETDATE(),
	check_out_date DATE NOT NULL DEFAULT DATEADD(DAY,  1, GETDATE()),
	CONSTRAINT PK_Guest_Id PRIMARY KEY(id),
	CONSTRAINT FK_Guest_Room_Room_Id FOREIGN KEY(room) REFERENCES Room(id),
	CONSTRAINT CK_Guest_DateOfCheckOutHigherThanDateOfCheckIn CHECK (check_out_date > check_in_date)
);
GO

CREATE TABLE Administrator
(
	id INT NOT NULL IDENTITY(1, 1),
	name NVARCHAR(50) NOT NULL,
	[Login] NVARCHAR(50) NOT NULL,
	[Password] NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_Administrator_Id PRIMARY KEY (id),
	CONSTRAINT UQ_Administrator_Login UNIQUE ([Login])
)
GO

