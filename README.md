# 📮 Sarahah – ASP.NET Core MVC  
A full-stack ASP.NET Core MVC application that allows users to register, log in, and receive anonymous messages — inspired by the popular **Sarahah** platform.

This project follows a **clean architecture** style with **UI**, **Core**, and **Infrastructure** layers, ensuring scalability, maintainability, and separation of concerns.

## 🚀 Features

### 🔐 Authentication & Identity
- User registration & login
- ASP.NET Core Identity (`ApplicationUser`, `ApplicationRole`)
- Password hashing & validation
- Login/registration with DTOs and validation helpers

### 💬 Anonymous Messaging
- Anyone can send a message anonymously
- Authenticated users can view all messages sent to them
- Messages stored securely in the database
- Strong validation for message input

### 🧱 Layered Architecture
- **sarahah.UI** → MVC Frontend
- **sarahah.Core** → Business Logic
- **sarahah.Infrastructure** → Data Layer

## 🗂️ Project Structure
```
sarahahSolution/
│
├── sarahah.UI/
│   ├── Controllers/
│   ├── Views/
│   ├── wwwroot/
│   └── appsettings.json
│
├── sarahah.Core/
│   ├── Domain/
│   ├── DTO/
│   ├── Services/
│   └── ServiceContracts/
│
└── sarahah.Infrastructure/
    ├── Data/
    ├── Migrations/
    └── Repositories/
```

## 🛠️ Technologies Used
- ASP.NET Core MVC
- Entity Framework Core
- ASP.NET Core Identity
- SQL Server
- Bootstrap 5

## ⚙️ Setup
1. Clone the repo  
2. Update `appsettings.json`  
3. Run migrations  
4. Start the app

## 👨‍💻 Author
**Eslam Mousa**  
Software Engineer