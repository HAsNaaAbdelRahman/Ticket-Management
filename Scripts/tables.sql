USE [Ticket Management ];


CREATE SCHEMA Tickets_Services;
GO 

CREATE SCHEMA Tickets_order ;
GO 

CREATE SCHEMA Customers_INFO
GO

-- 1) Priority Table (Low, Medium, High)

CREATE TABLE Tickets_order.PriorityTypes 
(
PriorityID INT IDENTITY(1,1) PRIMARY KEY,
PriorityName VARCHAR(20) NOT NULL
);


-- 2) IssueTypes Table (Technical, Billing, Complaint, Other)

CREATE TABLE Tickets_order.IssueTypes 
(
IssueTypeID INT IDENTITY(1,1) PRIMARY KEY ,
IssueTypeName  VARCHAR(50)  NOT NULL,
);

SELECT * FROM Tickets_order.IssueTypes ;

-- 3) Status Table (Open, In Progress, On Hold, Resolved, Closed)

CREATE TABLE Tickets_order.StatusTypes 
(
StatusID INT IDENTITY(1,1) PRIMARY KEY,
StatusName VARCHAR(30) NOT NULL
);


select * from Tickets_order.StatusTypes 
-- 4) CustomerTickets Table

CREATE TABLE Tickets_Services.CustomerTickets 
(

TicketID  INT PRIMARY KEY  IDENTITY(1 , 1),
FullName NVARCHAR(225) NOT NULL,
MobileNumber VARCHAR(25),
Email VARCHAR(255) NOT NULL,
Description NVARCHAR(MAX) NULL,
CreatedDate  DATETIME2(0)  DEFAULT GETUTCDATE() NOT NULL, 
IssueTypeID INT NOT NULL,
PriorityID INT  NOT NULL ,
StatusID INT NOT NULL ,
CONSTRAINT FK_CustomerTickets_IssueType FOREIGN KEY(IssueTypeID) REFERENCES Tickets_order.IssueTypes(IssueTypeID) ON UPDATE CASCADE,
CONSTRAINT FK_CustomerTickets_PriorityTypes FOREIGN KEY(PriorityID) REFERENCES Tickets_order.PriorityTypes(PriorityID) ON UPDATE CASCADE,
CONSTRAINT FK_CustomerTickets_StatusTypes FOREIGN KEY(StatusID) REFERENCES Tickets_order.StatusTypes(StatusID) ON UPDATE CASCADE,
);



-------------- Customer Table

CREATE TABLE Customers_INFO.Customers
(
ID INT IDENTITY(1 , 1) PRIMARY KEY,
FullName NVARCHAR(225) NOT NULL,
MobileNumber VARCHAR(25),
Email VARCHAR(255) NOT NULL,
Password VARCHAR(30) NOT NULL
);

SELECT *  FROM Customers_INFO.Customers;
SELECT * FROM Tickets_Services.CustomerTickets ;

ALTER TABLE Tickets_Services.CustomerTickets
    ADD CustomerID INT NOT NULL; 
GO
ALTER TABLE Tickets_Services.CustomerTickets
    ADD CONSTRAINT FK_CustomerTickets_Customers
        FOREIGN KEY (CustomerID)
        REFERENCES Customers_INFO.Customers(ID)
        ON UPDATE CASCADE;


ALTER TABLE Customers_INFO.Customers
ALTER COLUMN Password NVARCHAR(256) NOT NULL;