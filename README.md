[![Build Status](https://github.com/davojc/Surreal.Client.Rest/actions/workflows/cicd.yaml/badge.svg)](https://github.com/davojc/Surreal.Client.Rest/actions/workflows/cicd.yaml)


# Surreal.Client.Rest

A lightweight, high-performance .NET client library for the [SurrealDB REST API](https://surrealdb.com/docs/surrealdb/integration/http).

This library provides a strongly-typed, asynchronous wrapper around the SurrealDB HTTP endpoints, allowing seamless integration into .NET applications using standard Dependency Injection patterns.

> It is not yet a complete set for all the Rest endpoints. This is a minimum and will be added to over time.

## Features

- Standard DI Integration: Easily registers with `IServiceCollection`.
- Asynchronous I/O: Full `async`/`await` support with `CancellationToken` propagation.
- Strongly Typed: Generic methods for CRUD operations (`Get<T>`, `Add<T>`, `Update<T>`) to map database records directly to C# objects.
- Health Checks: Built-in methods for checking database status, health, and version.
- Smart ID Handling: Configurable serialization of SurrealDB IDs (handling the `table:id` format automatically) via the `SurrealIdOptions` flags.

## Installation

```
dotnet add package Surreal.Client.Rest
```

## Configuration

To use the client, register it in your application's startup code (e.g., `Program.cs`) using the provided extension method. You can configure the base URL, authentication credentials, and ID serialization behavior.

### Basic Setup

```C#
using Surreal.Client.Rest;

var builder = WebApplication.CreateBuilder(args);

// Register the client
builder.Services.AddSurrealRestClient(options =>
{
    options.BaseAddress = new Uri("http://localhost:8000");
    options.Username = "root";
    options.Password = "root";
    options.Namespace = "test";
    options.Database = "test";
    
    // Configure ID behavior using the Flags enum
    // Default is SurrealIdOptions.None (clean IDs)
    options.SurrealIdOptions = SurrealIdOptions.None; 
});

```

Configuration via appsettings.json

You can also pass `IConfiguration` to automatically bind settings.

`appsettings.json`

```json
{
  "SurrealDb": {
    "BaseAddress": "http://localhost:8000",
    "Username": "root",
    "Password": "root",
    "Namespace": "my_ns",
    "Database": "my_db",
    "IdOptions": "ExposeSurrealIds" 
  }
}
```

`Program.cs`

```C#
// Pass the configuration section to the extension method
builder.Services.AddSurrealRestClient(
    options => { /* optional overrides here */ }, 
    builder.Configuration.GetSection("SurrealDb")
);
```


## Modelling Data (Attributes)

This library relies on attributes to map your C# classes to SurrealDB tables and handle relationships between them.

### Defining Tables (`[Table]`)

Every model class used for CRUD operations must be decorated with the `TableAttribute`. This tells the library which SurrealDB table the class belongs to.

```C#
[Table("user")]
public class User
{
    public string? Id { get; set; }
    public string Name { get; set; }
}
```


### Handling Relationships (`[IdField]`)

If your model has a property that references another model (a foreign key), you must use the `IdFieldAttribute`. This ensures the library correctly processes the ID (e.g., formatting it as `table:id` if required) based on the referenced type's table name.

```C#
[Table("post")]
public class Post
{
    public string? Id { get; set; }
    public string Title { get; set; }

    // Tells the library that 'AuthorId' refers to a record in the 'user' table
    [IdField(typeof(User))]
    public string AuthorId { get; set; }
}
```

## ID Serialization Scheme (SurrealIdOptions)

SurrealDB natively uses a table:id format for its record IDs (e.g., `user:kb28374`). The library behavior around these IDs is controlled by the SurrealIdOptions flags enum.

```C#
[Flags]
public enum SurrealIdOptions
{
    None = 0,
    ExposeSurrealIds = 1,
    Optimise = 2
}
```

### `SurrealIdOptions.None` (Default)

When set to `None`, the library automatically strips the table name when reading and re-appends it when writing. This keeps your C# models clean.

C# Model: `public string Id { get; set; } = "123";`

Sent to DB: `user:123`

Received from DB: `123`


### `SurrealIdOptions.ExposeSurrealIds`

If you prefer to work with the raw SurrealDB Record ID format you can enable this flag to expose the full ID scheme.

C# Model: `public string Id { get; set; } = "user:123";`

Sent to DB: `user:123`

Received from DB: `user:123`

## Usage Examples

Once registered, you can inject `ISurrealRestClient` into any service or controller.

### Checking Database Health

```C#
public class HealthService
{
    private readonly ISurrealRestClient _client;

    public HealthService(ISurrealRestClient client)
    {
        _client = client;
    }

    public async Task CheckConnection()
    {
        var isReady = await _client.Health();
        var version = await _client.Version();
        
        Console.WriteLine($"DB Ready: {isReady.Data} | Version: {version.Data}");
    }
}
```

### Creating and Retrieving Records

Ensure your models have the `[Table]` attribute.

```C#
[Table("user")]
public class User
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
}
```

**Adding a Record:**

```C#
public async Task CreateUser(string name)
{
    var newUser = new User { Name = name, Role = "Engineer" };
    
    // POST request to /key/user
    // Automatically uses the table name "user" from the attribute
    var response = await _client.Add(newUser);
    
    if (response.IsSuccess)
    {
        Console.WriteLine($"Created User with ID: {response.Data.Id}");
    }
}

```

**Retrieving All Records:**

```C#
public async Task ListUsers()
{
    // GET request to /key/user
    var response = await _client.Get<User>();
    
    foreach (var user in response.Data)
    {
        Console.WriteLine($"{user.Id}: {user.Name}");
    }
}
```

**Retrieving a Single Record:**

```C#
public async Task<User?> GetUser(string id)
{
    // GET request to /key/user/{id}
    var response = await _client.Get<User>(id);
    return response.Data;
}
```

### Running Custom SurrealQL Queries

For complex logic that goes beyond simple CRUD, you can execute raw SurrealQL queries via the REST end

```C#
public async Task FindAdmins()
{
    string sql = "SELECT * FROM user WHERE role = $role";
    var parameters = new Dictionary<string, string>
    {
        { "role", "Admin" }
    };

    // Executes the query via the /sql endpoint
    var response = await _client.Query<User>(sql, parameters);
    
    // Process results...
}

```

## API Reference

| Method | Description | SurrealDB Endpoint |
|-|-|-|
| Status() | Checks if the server is ready to accept connections. | [GET /status](https://surrealdb.com/docs/surrealdb/integration/http#status) |
| Health() | Checks the health of the database. | [GET /health](https://surrealdb.com/docs/surrealdb/integration/http#health) |
| Version() | Retrieves the version of the SurrealDB server. | [GET /version](https://surrealdb.com/docs/surrealdb/integration/http#version) |
| Get<T>() | Retrieves all records from the table corresponding to type T. | [GET /key/{table}](https://surrealdb.com/docs/surrealdb/integration/http#get-table) |
| Get<T>(id) | Retrieves a specific record by ID. | [GET /key/{table}/{id}](https://surrealdb.com/docs/surrealdb/integration/http#get-record) |
| Add<T>(record) | Creates a new record in the table. | [POST /key/{table}](https://surrealdb.com/docs/surrealdb/integration/http#post-table) |
| Update<T>(record) | Updates an existing record. | [PUT /key/{table}/{id}](https://surrealdb.com/docs/surrealdb/integration/http#put-record) |
| Delete<T>(id) | Deletes a record by ID. | [DELETE /key/{table}/{id}](https://surrealdb.com/docs/surrealdb/integration/http#delete-record) |
| Query<T>(sql, params) | Executes a custom SurrealQL query. | [POST /sql](https://surrealdb.com/docs/surrealdb/integration/http#sql) |