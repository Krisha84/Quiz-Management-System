# 📝 Quiz Management System - ASP.NET Core MVC

The **Quiz Management System** is a complete web application built using **ASP.NET Core MVC** with **SQL Server database**, designed to simplify the creation, management, and evaluation of quizzes for academic or organizational purposes.

> _"Empower learning and assessments through an efficient quiz platform."_

---

## ✨ Why Quiz Management System?

This system helps in:
- Creating and managing quizzes easily
- Categorizing quizzes by subjects or topics
- Auto-grading quizzes to reduce manual work
- Tracking user performance and quiz statistics

This project was developed to explore **ASP.NET Core MVC** capabilities and to practice building a full-stack web application with real-world functionality.

---

## 🌟 Features

| Feature               | Description                          |
|-----------------------|--------------------------------------|
| 👤 **User Authentication** | Secure login system for admins and users |
| 📝 **Quiz Creation**       | Create quizzes with multiple questions |
| ❓ **Question Management** | Add, Edit, Delete quiz questions |
| 📋 **Category-wise Quizzes** | Organize quizzes by subject/topic |
| 📈 **Statistics Dashboard** | View performance stats and quiz attempts |
| 🗄️ **Database Integrated** | Data persistence using SQL Server |

---

## 🛠️ Tech Stack

- **Frontend:** ASP.NET Core MVC, HTML5, CSS3, Bootstrap
- **Backend:** C# with ASP.NET Core
- **Database:** SQL Server, T-SQL
- **Tools:** Visual Studio, SSMS (SQL Server Management Studio)

---

## 📂 Folder Structure

- **Controllers/**: Contains all the **controllers** that handle HTTP requests and control the flow between models and views.

- **Models/**: Holds all the **C# model classes** representing entities like Quiz, Question, Result, etc.

- **Views/**: Includes all the **Razor Views (frontend pages)** that display data and UI.

- **Properties/**: Contains **project configuration settings**.

- **CheckAccess.cs**: A custom class to manage **access control logic** for secure operations.

- **CommonVariables.cs**: Stores commonly used **variables/constants** across the project.

- **Program.cs**: The **main entry point** of the ASP.NET Core application.

- **SQLQueryQuiz.sql**: SQL script to **initialize the database**, create tables, and seed data.

- **appsettings.json**: Configuration file containing the **database connection string** and other project settings.
