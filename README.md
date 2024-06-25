
# LaraSharp Documentation

## Introduction

LaraSharp is a .NET-based framework designed to bring the elegance and simplicity of Laravel to C# developers. This documentation will guide you through the setup, features, and usage of LaraSharp, helping you to quickly integrate its capabilities into your applications.

## Features

LaraSharp includes several powerful components inspired by Laravel, tailored for .NET environments:

### SqlBuilder
- **Purpose**: Facilitates building and executing SQL queries.
- **Key Methods**:
  - `Create`: Inserts data into the specified table.
  - `Read`: Retrieves data from the database based on the specified criteria.

### HtmlBuilder
- **Purpose**: Assists in programmatically generating HTML content for dynamic web pages.

### Routes
- **Purpose**: Manages routing within the application, directing HTTP requests to the correct handlers.
- **Key Functions**:
  - `RegisterRoute`: Registers a new route and its associated view.
  - `GetRoute`: Retrieves the content for a specific route.

### Views
- **Purpose**: Provides a simple system to manage HTML views and integrate them with backend logic.

## Getting Started

### Prerequisites
- .NET SDK
- MySql Database

### Installation
1. Clone the LaraSharp repository from GitHub.
2. Install necessary dependencies.
3. Configure your database connection settings.

### Configuration
Edit the `settings.json` file to set up your database and server configurations:

```json
{
  "Database": {
    "Ip": "localhost",
    "Port": "3306",
    "User": "your_username",
    "Password": "your_password",
    "DatabaseName": "your_db_name"
  },
  "Server": {
    "Host": "127.0.0.1",
    "Port": "8080"
  }
}
```

## Examples

### Setting up a SQL Connection

```csharp
var sqlBuilder = new SqlBuilder("localhost", "3306", "user", "password", "database");
var sql = new Sql("database", sqlBuilder.Connection);
sql.Create("users", new string[] { "name", "email" }, new string[] { "John Doe", "john@example.com" });
await sql.Execute();
```

### Routing Example

```csharp
Routes.RegisterRoute("welcome", "index");
var content = Routes.GetRoute("welcome");
Console.WriteLine(content);
```

## Contributing

Contributions are welcome! Please feel free to submit pull requests, or file issues for bugs, documentation improvements, or new features.
