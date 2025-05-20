USE [Ticket Management ];


-- Priority Table (Low, Medium, High)

INSERT INTO Tickets_order.PriorityTypes (PriorityName)
VALUES ('Low'), ('Medium' ),('High');

SELECT * FROM Tickets_order.PriorityTypes;

-- IssueTypes Table (Technical, Billing, Complaint, Other)

INSERT INTO Tickets_order.IssueTypes (IssueTypeName)
VALUES ('Technical') , ('Billing') , ('Complaint') , ('Other');

SELECT * FROM Tickets_order.IssueTypes;


-- Status Table (Open, In Progress, On Hold, Resolved, Closed)

INSERT INTO Tickets_order.StatusTypes (StatusName)
VALUES ('Open') , ('In Progress') , ('On Hold') , ('Resolved') , ('Closed');

SELECT * FROM Tickets_order.StatusTypes;


