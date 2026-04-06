# Event Management System
## **Tech Stack**
- **Backend**: ASP.NET Core MVC, C#, Entity Framework Core
- **Frontend**: HTML5, CSS3, JavaScript, Bootstrap 5
- **Database**: SQL Server
- **Authentication**: ASP.NET Core Identity

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
cd "ur folder path"
git clone https://github.com/baoquoc05005/Event-Management-System.git
cd Event-Management-System
```

### **3. Create Feature Branch**
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
## **Workflow**
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
3. Add description & request review
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
---
