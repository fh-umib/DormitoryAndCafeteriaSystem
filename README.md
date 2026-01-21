# Dormitory and Cafeteria Management System

**Authors:** Flutura and Tringa Hyseni
**Technologies:** C# Console Application, SQL (PostgreSQL)

---

## Project Overview

This project is a **console-based management system** for a dormitory and cafeteria.
It consists of two integrated parts:

1. **C# Console Application** – handles student interactions, dormitory applications, cafeteria orders, and reporting.
2. **SQL Database** – stores all persistent data including students, dormitory applications, cafeteria orders, and payments.

The project demonstrates **database interaction, modular application design, and collaborative development**.

---

## Features

* Apply for dormitories and manage applications
* Place cafeteria orders and calculate total prices
* View student and order reports
* Fully integrated C# application with SQL backend
* Proper error handling and validation
* Collaborative development with reflection on AI usage

---

## Installation

1. Clone the repository:

```bash
git clone [https://github.com/fh-umib/DormitoryAndCafeteriaSystem.git]
```

2. Open the C# solution in **Visual Studio** or your preferred IDE.

3. Configure the SQL database connection string in `DbConnectionFactory.cs`:

```csharp
Host=localhost;Port=5432;Database=DormitoryAndCafeteriaSystemDB;Username=postgres;Password=2206;
```

4. Execute the SQL script `database_setup.sql` to create all tables.

5. Build and run the application.

---

## Usage

Run the application via console:

```bash
dotnet run
```

Follow prompts to:

* Apply for dormitory
* Place cafeteria orders
* View reports

---

## Screenshots
//Rregulloje folderin qe e ke thirr te qikjo sc...
### 1. Dormitory Menu

![Dormitory Menu](screenshots/dormitory_menu.png)

### 2. Dormitory Application Submission

![Dormitory Submission](screenshots/dormitory_submission.png)

### 3. Cafeteria Order Placement

![Cafeteria Order](screenshots/cafeteria_order.png)

### 4. Error Handling Example

![Error Handling](screenshots/error_handling.png)

---

## Collaborative & AI Notes

* This project was developed **jointly by Flutura and Tringe**.
* AI was used responsibly for guidance, explanation, and debugging advice. **No code was copied directly.**
* Full reflection and AI usage documentation is provided in [`Reflection Paper.md`] 

---

## Future Improvements

* Implement a GUI for better user interaction
* Add more validation and automated testing
* Integrate advanced reporting features
* Consider multiple users access and authentication
