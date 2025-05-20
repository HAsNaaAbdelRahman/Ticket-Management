# Ticket Management System – ASP.NET MVC 5

## Overview

This is a technical pilot task developed as part of an evaluation for an ASP.NET MVC 5 developer position. The project is a functional **Ticket Management Module** that enables users to create, view, edit, and list customer service tickets.

The application follows **N-Tier Architecture** and uses **SQL Server** with **stored procedures** for all database interactions.


---

## Features

- Create new customer service tickets
- View list of all tickets (filter by issue type and priority)
- View ticket details in read-only mode
- Admin can edit existing tickets (except ID and Created Date)
- Optional home page for navigation
- Full client-side and server-side validation

---

## Architecture

The project follows a clean **N-Tier Architecture** pattern:

- **Presentation Layer (PL)**: ASP.NET MVC 5 (Controllers + Views)
- **Business Logic Layer (BLL)**: Contains all application logic and validation
- **Data Access Layer (DAL)**: Handles all database interactions using stored procedures

Data is passed between layers using **DTOs** and **ViewModels**.

---

## Technologies Used

- ASP.NET MVC 5
- C#
- SQL Server
- ADO.NET
- HTML/CSS/Bootstrap (for styling)
- JavaScript/jQuery (for client-side validation)

---

## How to Run

1. **Database Setup**
   - Open the SQL script file provided in the `Database` folder.
   - Execute the script in SQL Server Management Studio to create:
     - `CustomerTickets` table
     - `IssueTypes` table
     - Stored Procedures (Insert, Update, Select, Filter)
     - Seed Data for `IssueTypes`

2. **Configure Connection String**
   - In `Web.config`, update the `connectionStrings` section with your local SQL Server credentials.

3. **Build and Run the Application**
   - Open the solution in Visual Studio.
   - Build the project to restore all dependencies.
   - Run the application using IIS Express or your preferred method.
   - Use the navigation links to create, view, and manage tickets.

---

## Database Schema

### Tables

**1. CustomerTickets**

| Field         | Type         | Notes                     |
|---------------|--------------|---------------------------|
| TicketID      | int (PK)     | Identity, Primary Key     |
| FullName      | nvarchar     | Required                  |
| MobileNumber  | nvarchar     | Required                  |
| Email         | nvarchar     | Required, Valid Email     |
| IssueTypeID   | int (FK)     | Foreign key to IssueTypes |
| Description   | nvarchar     | Required, Multiline       |
| Priority      | nvarchar     | Low / Medium / High       |
| Status        | nvarchar     | Default: "Open"           |
| CreatedDate   | datetime     | Auto-populated server-side|

**2. IssueTypes**

| Field         | Type         | Notes                 |
|---------------|--------------|-----------------------|
| IssueTypeID   | int (PK)     | Primary Key           |
| IssueTypeName | nvarchar     | E.g., Technical, Billing, etc.|

---

## Screens Implemented

1. **Create Ticket Page** – for customers
2. **List Tickets Page** – for admin & customers (with filters)
3. **View Ticket Page** – read-only ticket details
4. **Edit Ticket Page** – for admin only
5. **(Optional)** Home Page – simple navigation UI

---

## Error Handling

- Used structured `try-catch` blocks in all layers (DAL, BLL, Controllers)
- Clear user messages for validation failures and system errors
- All exceptions are either logged or shown with context-specific messages

---

## Assumptions

- Role-based access (Admin vs Customer) is assumed logically; full identity/auth system is not implemented.
- Status is defaulted to "Open" and not editable.
- Stored procedures are used exclusively for **Insert**, **Update**, **Select**, and **Filter** operations.
- No use of scaffolding – all views and logic are hand-coded.

---

## Contributors

- Developed by: **Hasnaa**  
  Role: ASP.NET MVC Developer

---

## License

This project is for evaluation purposes only.
