<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>LaraSharp Documentation</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            line-height: 1.6;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
            color: #333;
        }
        .container {
            width: 80%;
            margin: auto;
            overflow: hidden;
            padding: 20px;
            background: #fff;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        h1, h2, h3 {
            color: #d9534f;
        }
        img {
            display: block;
            margin: 0 auto;
            max-width: 100%;
        }
        pre {
            background: #eee;
            padding: 10px;
            border-left: 3px solid #d9534f;
            overflow-x: auto;
        }
        code {
            font-family: Consolas, monospace;
        }
        .section {
            margin-bottom: 40px;
        }
    </style>
</head>
<body>
    <div class="container">
        <h1>LaraSharp Documentation</h1>
        <img src="https://cdn.discordapp.com/attachments/1165057088146386954/1255180248757174322/DALL_E-2024-06-25-17.14-removebg-preview.png?ex=667c3133&is=667adfb3&hm=18ef020779de912b0242fad21276c0bc129b52f6e874b36b937ec96a74ce1158&" alt="LaraSharp Logo" width="300"/>

        <div class="section">
            <h2>Introduction</h2>
            <p>LaraSharp is a .NET-based framework designed to bring the elegance and simplicity of Laravel to C# developers. This documentation will guide you through the setup, features, and usage of LaraSharp, helping you to quickly integrate its capabilities into your applications.</p>
        </div>

        <div class="section">
            <h2>Features</h2>
            <p>LaraSharp includes several powerful components inspired by Laravel, tailored for .NET environments:</p>
            <h3>SqlBuilder</h3>
            <ul>
                <li><strong>Purpose</strong>: Facilitates building and executing SQL queries.</li>
                <li><strong>Key Methods</strong>:
                    <ul>
                        <li><code>Create</code>: Inserts data into the specified table.</li>
                        <li><code>Read</code>: Retrieves data from the database based on the specified criteria.</li>
                    </ul>
                </li>
            </ul>
            <h3>HtmlBuilder</h3>
            <ul>
                <li><strong>Purpose</strong>: Assists in programmatically generating HTML content for dynamic web pages.</li>
            </ul>
            <h3>Routes</h3>
            <ul>
                <li><strong>Purpose</strong>: Manages routing within the application, directing HTTP requests to the correct handlers.</li>
                <li><strong>Key Functions</strong>:
                    <ul>
                        <li><code>RegisterRoute</code>: Registers a new route and its associated view.</li>
                        <li><code>GetRoute</code>: Retrieves the content for a specific route.</li>
                    </ul>
                </li>
            </ul>
            <h3>Views</h3>
            <ul>
                <li><strong>Purpose</strong>: Provides a simple system to manage HTML views and integrate them with backend logic.</li>
            </ul>
        </div>

        <div class="section">
            <h2>Getting Started</h2>
            <h3>Prerequisites</h3>
            <ul>
                <li>.NET SDK</li>
                <li>MySql Database</li>
            </ul>
            <h3>Installation</h3>
            <ol>
                <li>Clone the LaraSharp repository from GitHub.</li>
                <li>Install necessary dependencies.</li>
                <li>Configure your database connection settings.</li>
            </ol>
            <h3>Configuration</h3>
            <p>Edit the <code>settings.json</code> file to set up your database and server configurations:</p>
            <pre><code>{
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
}</code></pre>
        </div>

        <div class="section">
            <h2>Examples</h2>
            <h3>Setting up a SQL Connection</h3>
            <pre><code>var sqlBuilder = new SqlBuilder("localhost", "3306", "user", "password", "database");
var sql = new Sql("database", sqlBuilder.Connection);
sql.Create("users", new string[] { "name", "email" }, new string[] { "John Doe", "john@example.com" });
await sql.Execute();</code></pre>
            <h3>Routing Example</h3>
            <pre><code>Routes.RegisterRoute("welcome", "index");
var content = Routes.GetRoute("welcome");
Console.WriteLine(content);</code></pre>
        </div>

        <div class="section">
            <h2>Contributing</h2>
            <p>Contributions are welcome! Please feel free to submit pull requests, or file issues for bugs, documentation improvements, or new features.</p>
        </div>
    </div>
</body>
</html>
