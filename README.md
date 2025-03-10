# eRenting - Car Rental Management System

## About the Project

This project was developed as part of the **Web Programming** course in the **Computer Engineering** degree at the Instituto Superior de Engenharia de Coimbra (ISEC). The goal of this project is to provide a **web-based car rental management system** that allows companies to rent out their fleet of vehicles while efficiently managing their operations.

The application was built using **C# ASP.NET MVC5**, utilizing **SQL Server (LocalDb)** and **Entity Framework Code First** for database management.

## Features

The system includes the following functionalities:

- **Car Management**
  - Register new vehicles
  - Edit and delete vehicle records
  - Manage vehicle categories
  - Add vehicle images (thumbnails)

- **User Management**
  - Different user roles:
    - **Guest** (can browse available vehicles)
    - **Customer** (can book rentals)
    - **Worker** (manages reservations and vehicle inspections)
    - **Company Admin** (manages employees and vehicles)
    - **System Admin** (full control over the system)
  
- **Reservation System**
  - Customers can book vehicles online
  - Employees confirm or reject reservations
  - Automatic association of deliveries with reservations
  - Customers can track their reservation history

- **Delivery & Inspection Management**
  - Employees confirm vehicle handover and return
  - Record vehicle condition and defects after return
  - Attach images for damage verification

- **Authentication & Security**
  - User authentication using **ASP.NET Identity**
  - Role-based access control
  - Email confirmation and password recovery

## Technologies Used

- **Backend:** C# ASP.NET MVC5
- **Database:** SQL Server (LocalDb), Entity Framework (Code First)
- **Frontend:** Bootstrap 4, SB Admin 2 Template
- **Authentication:** ASP.NET Identity
- **Dependency Management:** NuGet Packages

## Database Structure

The application uses a relational database with the following key tables:

- **Vehicles** – Stores information about available vehicles
- **Reservations** – Manages vehicle bookings
- **Categories** – Organizes vehicles into categories
- **Deliveries** – Tracks vehicle handover and return
- **Defects** – Logs issues found in returned vehicles
- **Users** – Stores registered users and their roles

## Installation & Setup

### Prerequisites
- .NET Framework 4.7 or later
- SQL Server (LocalDb) installed
- Visual Studio (recommended)

### Steps to Run the Application
1. **Clone the repository:**
   ```bash
   git clone https://github.com/your-repo/eRenting.git
   ```
2. **Open the solution in Visual Studio**
3. **Restore NuGet packages**
4. **Update the database:**
   ```bash
   Update-Database
   ```
5. **Run the application**

## Usage Guide

1. **Browse available vehicles** (Guest & Customers)
2. **Register an account** to book rentals
3. **Company employees confirm reservations**
4. **Customers pick up and return vehicles**
5. **Employees inspect returned vehicles**
6. **Admin manages system settings**

## Notes
- Guests can only view the vehicle catalog.
- Customers must register and log in to make reservations.
- Employees manage vehicle handovers and inspections.
- Admin users have full control over the system.

This project was developed to enhance **web development skills**, focusing on **MVC architecture, role-based authentication, and database management**.