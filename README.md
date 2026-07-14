# Dormitory and Cafeteria Management System

A console-based management system developed in C# and integrated with a PostgreSQL database for managing university dormitory applications, cafeteria orders, payments, and reports.

The project demonstrates database integration, layered application design, input validation, error handling, and collaborative software development.

---

## Authors

**Flutura Hyseni**  
**Tringa Hyseni**

---

## Technologies Used

- C#
- .NET
- PostgreSQL
- SQL
- Npgsql
- Git
- GitHub
- Visual Studio

---

## Project Overview

The system consists of two integrated components:

1. **C# Console Application**  
   Handles user interaction, dormitory applications, cafeteria orders, payments, validation, and reporting.

2. **PostgreSQL Database**  
   Stores and manages persistent data related to students, dormitories, rooms, applications, cafeteria products, orders, and payments.

The application follows a layered structure to separate data access, business logic, domain entities, and user interaction.

---

## Main Features

### Dormitory Management

- Manage student information
- Submit dormitory applications
- Manage dormitory and room information
- Process room-related operations
- View dormitory application records

### Cafeteria Management

- Manage cafeteria products
- Create cafeteria orders
- Calculate order totals
- Store and retrieve order information
- View cafeteria reports

### Data Management

- PostgreSQL database integration
- Persistent data storage
- Repository-based data access
- Input validation
- Error handling
- SQL-based reporting

---

## Application Architecture

The project is organized using a layered architecture:

- **Entities** — Contains the main domain models
- **Repositories** — Handles database access and data operations
- **Services** — Contains application and business logic
- **Data** — Manages database configuration and connections
- **DormitoryAndCafeteria** — Contains the console application flow and user interaction
- **Documentation** — Contains project-related documentation

---

## Project Structure

```text
DormitoryAndCafeteriaSystem/
│
├── Data/
│   └── Database connection and configuration
│
├── Documentation/
│   └── Project documentation
│
├── DormitoryAndCafeteria/
│   └── Console application and user interaction
│
├── Entities/
│   └── Domain models and system entities
│
├── Repositories/
│   └── Database access and data operations
│
├── Services/
│   └── Business logic and application services
│
├── DormitoryAndCafeteriaSystem1.csproj
├── .gitattributes
├── .gitignore
└── README.md
```

---

## Installation and Setup

### Prerequisites

Before running the application, make sure that the following tools are installed:

- .NET SDK
- PostgreSQL
- Visual Studio, Visual Studio Code, or another compatible IDE
- Git

### 1. Clone the Repository

```bash
git clone https://github.com/fh-umib/DormitoryAndCafeteriaSystem.git
```

### 2. Open the Project

Open the project in Visual Studio or another compatible development environment.

### 3. Configure the Database Connection

Update the PostgreSQL connection configuration in `DbConnectionFactory.cs`.

Use your own local database credentials:

```csharp
Host=localhost;
Port=5432;
Database=DormitoryAndCafeteriaSystemDB;
Username=postgres;
Password=YOUR_PASSWORD;
```

> Do not publish real database passwords or other sensitive credentials in the repository.

### 4. Prepare the Database

Create the required PostgreSQL database and execute the SQL scripts associated with the project.

### 5. Build the Project

```bash
dotnet build
```

### 6. Run the Application

```bash
dotnet run
```

---

## Usage

After running the application, follow the console menu options to perform operations such as:

- Submit a dormitory application
- Manage dormitory-related information
- Place cafeteria orders
- Calculate order totals
- View stored records
- Generate reports

---

## Validation and Error Handling

The application includes validation and error handling for:

- Invalid menu selections
- Incorrect user input
- Missing or invalid information
- Database-related errors
- Invalid dormitory operations
- Invalid cafeteria order operations

---

## Collaborative Development

This project was developed collaboratively by **Flutura Hyseni** and **Tringa Hyseni**.

The development process included:

- Collaborative planning
- Database design
- C# application development
- Testing and debugging
- Documentation
- Git and GitHub version control

AI tools were used as learning and development support for explanations, debugging guidance, and code review.

---

## Learning Outcomes

Through this project, we strengthened our knowledge and practical skills in:

- Object-oriented programming with C#
- PostgreSQL database integration
- SQL development
- Layered software architecture
- Repository and service patterns
- Database connection management
- Input validation
- Error handling
- Collaborative software development
- Git and GitHub version control
- Technical documentation

---

## Future Improvements

Future improvements may include:

- Graphical user interface
- Web-based application
- User authentication and authorization
- Role-based access control
- Automated testing
- Advanced reporting and analytics
- Improved database configuration security
- Multi-user support
- Administrative dashboard

---

## Authors

**Flutura Hyseni**  
Computer Science & Engineering Student  
University of Mitrovica "Isa Boletini" — UMIB

[GitHub Profile](https://github.com/fh-umib)  
[LinkedIn Profile](https://www.linkedin.com/in/flutura-hyseni-9558823b0)

**Tringa Hyseni**  
Computer Science & Engineering Student
