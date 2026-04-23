# Event Management System
## C#.NET MVC + Microservices Web Application

A comprehensive event management system built with ASP.NET Core MVC, microservices architecture, Entity Framework Core, and role-based authentication.

---

## 🎯 Project Overview

This application allows users to browse and register for events, while administrators can manage events, categories, and view registrations. The system demonstrates a complete microservices architecture with MVC frontend communicating with backend APIs.

### Key Features
- ✅ **User Authentication & Authorization** (Login, Register, Role-based access)
- ✅ **Event Management** (Full CRUD operations)
- ✅ **Registration System** (Users can register for events)
- ✅ **Category Management** (Admin only)
- ✅ **Microservices Architecture** (API Gateway + 3 Microservices)
- ✅ **Entity Framework Core** (SQLite database with migrations)
- ✅ **Responsive UI** (Bootstrap 5)
- ✅ **Dashboard** (Statistics and featured events)

---

## 🏗️ Architecture

### System Flow
```
User → MVC App → API Gateway → Microservices → Database
Database → Microservices → API Gateway → MVC App → User
```

### Components
1. **MVC Frontend** (`EventManagementSystemFinal`)
   - Controllers, Views, Models
   - Authentication & Authorization
   - UI/UX with Bootstrap

2. **Microservices**
   - **AuthService** (Port 5001) - User authentication
   - **EventService** (Port 5002) - Event management
   - **RegistrationService** (Port 5003) - Registration handling
   - **API Gateway** (Port 5004) - Routes requests to services

3. **Database**
   - SQLite with Entity Framework Core
   - 4 Tables: Users, Events, Categories, Registrations
   - Relationships with Foreign Keys

---

## 📊 Database Schema

### Tables
1. **AspNetUsers** (Identity)
   - Id, Email, FirstName, LastName, CreatedAt

2. **Events**
   - EventId (PK), Title, Description, Location, EventDate, Capacity
   - CategoryId (FK), CreatedAt, UpdatedAt, IsActive

3. **Categories**
   - CategoryId (PK), Name, Description, CreatedAt, IsActive

4. **Registrations**
   - RegistrationId (PK), EventId (FK), UserId (FK)
   - RegistrationDate, Status, Notes

---

## 🚀 Setup Instructions

### Prerequisites
- .NET 10.0 SDK
- Visual Studio 2022 or VS Code
- Git

### Installation Steps

1. **Clone Repository**
   ```bash
   git clone <repository-url>
   cd EventManagementSystemFinal
   ```

2. **Restore Packages**
   ```bash
   dotnet restore
   ```

3. **Run Database Migrations**
   ```bash
   dotnet ef database update
   ```
   *Note: Database is auto-seeded on first run*

4. **Run Application**
   ```bash
   dotnet run
   ```

5. **Access Application**
   - Open browser: `http://localhost:5298`

### Run Microservices (Required for full functionality)
```bash
# Terminal 1 - API Gateway
cd ApiGateway
dotnet run

# Terminal 2 - Auth Service
cd Microservices/AuthService
dotnet run

# Terminal 3 - Event Service
cd Microservices/EventService
dotnet run

# Terminal 4 - Registration Service
cd Microservices/RegistrationService
dotnet run
```

---

## 👥 Default Users

### Admin Account
- **Email:** `admin@eventmanagement.com`
- **Password:** `Admin123!`
- **Permissions:** Full access (Create/Edit/Delete events, Manage categories)

### Test User
- Register a new account via the Register page
- **Permissions:** View events, Register for events, Manage own registrations

---

## 🎨 Features Walkthrough

### 1. Authentication
- **Register:** Create new user account
- **Login:** Access with email/password
- **Logout:** Secure sign out
- **Role-based Access:** Admin vs User permissions

### 2. Event Management (Admin)
- **Create Event:** Add new events with details
- **Edit Event:** Update event information
- **Delete Event:** Soft delete (mark inactive)
- **View All Events:** Browse complete event list

### 3. Event Registration (Users)
- **Browse Events:** View upcoming events
- **Event Details:** See full event information
- **Register:** Sign up for events
- **My Registrations:** View and manage registrations
- **Cancel Registration:** Remove registration

### 4. Category Management (Admin)
- **Create Category:** Add event categories
- **Edit Category:** Update category details
- **Delete Category:** Remove categories
- **View Categories:** List all categories

### 5. Dashboard
- **Statistics:** Total events, upcoming events, registrations
- **Featured Events:** Display upcoming events
- **Quick Actions:** Navigate to key features

---

## 🔧 Technology Stack

### Backend
- ASP.NET Core 10.0 MVC
- ASP.NET Core Web API (Microservices)
- Entity Framework Core 10.0
- ASP.NET Core Identity
- SQLite Database

### Frontend
- Razor Views
- Bootstrap 5.1.3
- Bootstrap Icons
- jQuery & jQuery Validation

### Architecture Patterns
- MVC (Model-View-Controller)
- Microservices
- Repository Pattern
- Dependency Injection

---

## 📁 Project Structure

```
EventManagementSystemFinal/
├── Controllers/
│   ├── AccountController.cs
│   ├── EventsController.cs
│   ├── EventsApiController.cs (Microservice integration)
│   ├── RegistrationsController.cs
│   ├── CategoriesController.cs
│   └── HomeController.cs
├── Models/
│   ├── ApplicationUser.cs
│   ├── Event.cs
│   ├── Registration.cs
│   ├── Category.cs
│   └── DashboardViewModel.cs
├── Views/
│   ├── Home/
│   ├── Account/
│   ├── Events/
│   ├── Registrations/
│   ├── Categories/
│   └── Shared/
├── Data/
│   ├── ApplicationDbContext.cs
│   └── ApplicationDbInitializer.cs
├── Services/
│   ├── IEventService.cs
│   ├── EventService.cs
│   ├── IRegistrationService.cs
│   └── RegistrationService.cs
├── ApiGateway/
│   ├── Controllers/
│   ├── Program.cs
│   └── appsettings.json
├── Microservices/
│   ├── AuthService/
│   ├── EventService/
│   └── RegistrationService/
├── wwwroot/
├── appsettings.json
├── Program.cs
├── README.md
└── .gitignore
```

---

## 🌐 API Endpoints

### Events API (via Gateway)
- `GET /api/gateway/events` - Get all events
- `GET /api/gateway/events/{id}` - Get event by ID
- `POST /api/gateway/events` - Create event
- `PUT /api/gateway/events/{id}` - Update event
- `DELETE /api/gateway/events/{id}` - Delete event

### Registrations API (via Gateway)
- `GET /api/gateway/registrations/user/{userId}` - Get user registrations
- `GET /api/gateway/registrations/{id}` - Get registration by ID
- `POST /api/gateway/registrations` - Create registration
- `DELETE /api/gateway/registrations/{id}` - Delete registration

---

## 🎓 Evaluation Criteria Met

| Requirement | Status | Implementation |
|------------|--------|----------------|
| Microservice | ✅ | 3 services + API Gateway |
| Authentication | ✅ | ASP.NET Identity with roles |
| CRUD Operations | ✅ | Events, Registrations, Categories |
| Entity Framework | ✅ | EF Core with migrations |
| MVC Architecture | ✅ | Complete MVC structure |
| Database Design | ✅ | 4 tables with relationships |
| UI/UX | ✅ | Bootstrap responsive design |
| Documentation | ✅ | This README |

---

## 📸 Screenshots

### Home Page
![Home Page](screenshots/home.png)

### Event Listing
![Events](screenshots/events.png)

### Event Details
![Event Details](screenshots/event-details.png)

### Registration
![Registration](screenshots/registration.png)

### Admin Dashboard
![Admin](screenshots/admin.png)

---

## 🚀 Deployment

### Deployment URL
- **Live Application:** [To be deployed]
- **API Gateway:** [To be deployed]

### Deployment Platforms
- Azure App Service
- AWS Elastic Beanstalk
- Render
- Railway

---

## 👨‍💻 Developer

**Your Name**
- GitHub: [@yourusername](https://github.com/yourusername)
- Email: your.email@example.com

---

## 📝 License

This project is developed as part of a final project submission for [Course Name].

---

## 🙏 Acknowledgments

- ASP.NET Core Documentation
- Bootstrap Framework
- Entity Framework Core
- Microsoft Identity

---

## 📞 Support

For issues or questions:
1. Check existing documentation
2. Review code comments
3. Contact: your.email@example.com

---

**Last Updated:** April 2026
