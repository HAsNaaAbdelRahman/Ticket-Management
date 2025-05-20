USE [Ticket Management ];

CREATE SCHEMA Tickets_PROCEDURE;
GO 

-- CustomerTickets Table
--                                Select (List & Single)

CREATE OR ALTER PROCEDURE Tickets_PROCEDURE.ShowCustomerTickets 
AS
BEGIN 
SELECT
C.TicketID ,C.FullName , C.MobileNumber , C.Email , C.Description , C.CreatedDate , S.StatusName 
as Status , I.IssueTypeName AS Issue , P.PriorityName AS Priority
FROM Tickets_Services.CustomerTickets as C JOIN  Tickets_order.StatusTypes AS S
ON C.StatusID = S.StatusID
JOIN Tickets_order.IssueTypes AS I
ON C.IssueTypeID = I.IssueTypeID 
JOIN 
Tickets_order.PriorityTypes AS P
ON C.PriorityID = P.PriorityID
END ;

------------------------------------------------------------
--                              USE Stored Procedures
EXEC Tickets_PROCEDURE.ShowCustomerTickets  ;
-----------------------------------------------

---                                          Update

CREATE OR ALTER PROCEDURE Tickets_PROCEDURE.UpdateCustomerTickets(@Ch_TicketID AS INT , @NewEmail AS VARCHAR(255))
AS
BEGIN 
UPDATE Tickets_Services.CustomerTickets  
SET Email = @NewEmail
WHERE TicketID = @Ch_TicketID
END;



------------------------------------------------------------
--                              USE Stored Procedures
EXEC Tickets_PROCEDURE.UpdateCustomerTickets 2 , 'ahmedali199@gmail.com' ;
-----------------------------------------------
--------------------------------

---                                         insert 

CREATE OR ALTER PROCEDURE Tickets_PROCEDURE.InsertValuesIntoCustomer
(@V_FullName AS NVARCHAR(225) , @V_MobileNumber AS VARCHAR(25) , @V_Email AS VARCHAR(255) ,
 @V_Description AS NVARCHAR(MAX) = NULL ,@V_IssueTypeID AS INT , @V_PriorityID AS INT , @V_StatusID AS INT = 1 , @V_CreatedDate DATETIME2(0) = NULL)
AS
BEGIN 
INSERT INTO Tickets_Services.CustomerTickets
(FullName , MobileNumber , Email , Description , IssueTypeID , PriorityID , StatusID , CreatedDate)
VALUES(@V_FullName , @V_MobileNumber , @V_Email , @V_Description , @V_IssueTypeID , @V_PriorityID , @V_StatusID , @V_CreatedDate)
END;
GO

------------------------------------------------------------
--                              USE Stored Procedures
EXEC Tickets_PROCEDURE.InsertValuesIntoCustomer 
@V_FullName = 'Ahmed Ali',  @V_MobileNumber = '+447386545775' ,  @V_Email = 'aliahmed@gmail.com',
 @V_Description = N'' , @V_IssueTypeID = 1 , @V_PriorityID =  1  , @V_StatusID = 1 ,  @V_CreatedDate = '2025-05-18 10:00:00' ;
-----------------------------------------------


---                                       Filter

-- 1) Filter by Priority
CREATE OR ALTER PROCEDURE Tickets_PROCEDURE.FilterByPriority(@F_PriorityID AS INT)
AS
BEGIN 
SELECT * FROM Tickets_Services.CustomerTickets 
WHERE PriorityID = @F_PriorityID
END;
GO

------------------------------------------------------------
--                              USE Stored Procedures
EXEC Tickets_PROCEDURE.FilterByPriority 1;
-----------------------------------------------


-- 2) Filter by Status
CREATE OR ALTER PROCEDURE Tickets_PROCEDURE.FilterByStatus(@F_StatusID AS INT)
AS
BEGIN 
SELECT * FROM Tickets_Services.CustomerTickets 
WHERE StatusID = @F_StatusID
END;
GO

------------------------------------------------------------
--                              USE Stored Procedures
EXEC Tickets_PROCEDURE.FilterByStatus 1;
-----------------------------------------------

-- 3) Filter by IssueType
CREATE OR ALTER PROCEDURE Tickets_PROCEDURE.FilterByIssueTypes(@F_IssueTypeID AS INT)
AS
BEGIN 
SELECT * FROM Tickets_Services.CustomerTickets 
WHERE IssueTypeID = @F_IssueTypeID
END;
GO

------------------------------------------------------------
--                              USE Stored Procedures
EXEC Tickets_PROCEDURE.FilterByIssueTypes 1;
-----------------------------------------------

