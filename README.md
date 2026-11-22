# ClassTrack

A simple Student Information Management System built with ASP.NET Core MVC, Entity Framework Core and SQL Server. It was developed as an academic project to demonstrate C#, object-oriented programming, CRUD operations, database integration and the MVC pattern.

## Features

- **Create** a student record (first name and surname).
- **Read** all students in a responsive Bootstrap table, or view one student's details.
- **Update** an existing student.
- **Delete** a student after a confirmation page.
- **Search** by student ID, first name or surname. Partial names work, and matching is case-insensitive.
- Server-side validation with Data Annotations (plus client-side validation in the browser).
- Antiforgery protection on every form that changes data.

## Technology stack

| Area | Technology |
|---|---|
| Language | C# (.NET 10) |
| Framework | ASP.NET Core MVC |
| ORM | Entity Framework Core 10 |
| Database | Microsoft SQL Server (Express) |
| Frontend | Razor views, HTML, CSS, Bootstrap 5 |

## MVC architecture

| Layer | Files | Responsibility |
|---|---|---|
| **Model** | `Models/Student.cs` | Defines the student properties and validation rules. |
| **View** | `Views/Student/*.cshtml` | Display data and forms. They contain no database logic. |
| **Controller** | `Controllers/StudentController.cs` | Receives requests, uses the database context, and chooses the view or redirect. |
| **Data** | `Data/ApplicationDbContext.cs` | The Entity Framework Core bridge to SQL Server. |

A request such as `/Student/Edit/5` is routed to `StudentController.Edit(5)`, which loads the student through `ApplicationDbContext` and returns the `Edit` view.

## Project structure

```
ClassTrack/
├── Controllers/
│   ├── StudentController.cs     # CRUD + search
│   └── HomeController.cs        # shared error page only
├── Data/
│   └── ApplicationDbContext.cs
├── Migrations/                  # EF Core migrations (InitialCreate)
├── Models/
│   ├── Student.cs
│   └── ErrorViewModel.cs
├── Views/
│   ├── Student/                 # Index, Details, Create, Edit, Delete
│   └── Shared/                  # _Layout, Error, validation scripts
├── wwwroot/                     # CSS, Bootstrap, jQuery
├── appsettings.json             # connection string
├── dotnet-tools.json            # local dotnet-ef tool
└── Program.cs                   # services and routing
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server Express (or another SQL Server instance)
- Optional: Visual Studio 2022+ or VS Code

## SQL Server setup

1. Install SQL Server Express, for example:
   ```powershell
   winget install Microsoft.SQLServer.2022.Express
   ```
2. Confirm the service is running (the default instance name is `SQLEXPRESS`):
   ```powershell
   Get-Service 'MSSQL$SQLEXPRESS'
   ```

The database (`ClassTrackDb`) is created for you by the migration step below.

## Configure the connection string

Edit `ConnectionStrings:DefaultConnection` in `appsettings.json`:

```json
"DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=ClassTrackDb;Trusted_Connection=True;TrustServerCertificate=True"
```

- It uses Windows authentication, so **no username or password is stored** in the project.
- Change `localhost\\SQLEXPRESS` if your instance has a different name. In JSON, a backslash must be written as `\\`.
- If you must use SQL authentication, do not put the password in `appsettings.json`. Store the whole connection string with user-secrets instead:
  ```powershell
  dotnet user-secrets init
  dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your connection string>"
  ```

## Run the database migrations

From the project folder:

```powershell
dotnet restore
dotnet tool restore
dotnet ef database update
```

This creates `ClassTrackDb` and the `Students` table (`Id` identity primary key, `FirstName` and `Surname` as `nvarchar(50)`, both required).

## Run the application

```powershell
dotnet run --launch-profile http
```

Open <http://localhost:5244>. The home page is the student list. (The `https` profile uses <https://localhost:7149> and needs the ASP.NET dev certificate: `dotnet dev-certs https --trust`.)

## Notes

- If you change `Models/Student.cs`, add a migration with `dotnet ef migrations add <Name>` and apply it with `dotnet ef database update`.
- There is no authentication, by design: the project scope is a single-user academic demo.
