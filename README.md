# Event Management System

ASP.NET Core MVC web application for managing events and registrations.

---

## **Prerequisites**

### **Required Software**
- **Visual Studio 2022** - [Download](https://visualstudio.microsoft.com/downloads/)
  - Workload: ASP.NET and web development
- **SQL Server Express** - [Download](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- **SQL Server Management Studio (SSMS)** - [Download](https://aka.ms/ssmsfullsetup)
- **Git** - [Download](https://git-scm.com/download/win)
- **.NET 8.0 SDK** - [Download](https://dotnet.microsoft.com/download)

### **Verify Installation**
```powershell
dotnet --version
git --version
```

---

## **GitHub Setup**

### **1. Configure Git**
```powershell
git config --global user.name "Your Name"
git config --global user.email "your.email@example.com"
```

### **2. Clone Repository**
```powershell
cd C:\Users\Main\Documents\WebDevelopment
git clone https://github.com/baoquoc05005/Event-Management-System.git
cd Event-Management-System
```

### **3. Create Your Feature Branch**
```powershell
# Member 1 - Authentication
git checkout -b feature/authentication

# Member 2 - Events
git checkout -b feature/events

# Member 3 - Registration
git checkout -b feature/registration

# Member 4 - Admin/UI
git checkout -b feature/admin-ui
```

---

## **Project Dependencies**

### **NuGet Packages**
```powershell
# Install via Package Manager Console
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
Install-Package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
```

Or via .NET CLI:
```powershell
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
```

### **Frontend Libraries (Included)**
- Bootstrap 5
- jQuery
- jQuery Validation

---

## **Database Setup**

### **1. Update Connection String**
Edit `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EventManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### **2. Create Database**
```powershell
# In Package Manager Console
Add-Migration InitialCreate
Update-Database
```

### **3. Verify in SSMS**
- Connect to: `(localdb)\mssqllocaldb`
- Check database: `EventManagementDB`

---

## **Running the Application**

### **Visual Studio**
Press `F5` or click the green play button (IIS Express)

### **Command Line**
```powershell
dotnet run
```
Navigate to: `https://localhost:5001`

---

## **Daily Workflow**

### **Before Starting Work**
```powershell
git checkout your-branch-name
git pull origin main
git merge main
```

### **Committing Changes**
```powershell
git add .
git commit -m "Descriptive message"
git push origin your-branch-name
```

### **Creating Pull Request**
1. Go to GitHub repository
2. Click "Compare & pull request"
3. Add description
4. Request team review
5. Merge after approval

---

## **Project Structure**

```
EventManagementSystem/
├── Controllers/       # MVC Controllers
├── Models/           # Data models
├── Views/            # Razor views
├── Data/             # Database context
├── wwwroot/          # Static files
└── Migrations/       # EF migrations
```

---

## **Team Modules**

- **Member 1**: Authentication & User Management
- **Member 2**: Event CRUD Operations
- **Member 3**: Registration System
- **Member 4**: Admin Dashboard & UI

---

## **Troubleshooting**

### **Database Connection Failed**
```powershell
Update-Database
```

### **NuGet Restore Failed**
```powershell
dotnet restore
```

### **Port Already in Use**
Edit `Properties/launchSettings.json` and change port numbers

### **Git Authentication**
Use Personal Access Token instead of password:
- GitHub Settings → Developer settings → Personal access tokens

---

## **Tech Stack**

- **Backend**: ASP.NET Core MVC, C#, Entity Framework Core
- **Frontend**: HTML5, CSS3, JavaScript, Bootstrap 5
- **Database**: SQL Server
- **Authentication**: ASP.NET Core Identity

---

## **Resources**

- [ASP.NET Core Docs](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [Bootstrap 5](https://getbootstrap.com/docs/5.3/)

---

**Repository**: https://github.com/baoquoc05005/Event-Management-System