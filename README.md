# DockerTools

DockerTools is a simple wrapper on top of [Docker.DotNet](https://github.com/dotnet/Docker.DotNet).

DockerTools was created as a means of abstracting the complexity of managing database containers when running automated tests. It provides a simple, fluent approach to creating, interacting, and disposing of containers.

## How to use
### Creating a container
Install the package, then simply perform the following:
```csharp
var container = await new DockerToolsContainer<Postgres>()
           .CreateAsync();
```

#### Tweaking the container
If necessary, certain default values can be replaced by using the `WithParameters()` method:

```csharp
var container = await new DockerTools<Postgres>()
           .WithParameters(options => options
               .WithDatabase("MyDatabase")
               .WithUsername("myUser")
               .WithPassword("myPassword")
               .WithTag("14.3"))
           .CreateAsync();
```
Note that you don't have to call all options; simply use the ones you need.

Additionally, you may also supply environment variables, such as enabling SQL Server Agent on SQL Server:

```csharp
var container = await new DockerTools<SqlServer>()
           .WithParameters(options => options
               .AddEnvironmentVariable("MSSQL_AGENT_ENABLED=True"))
           .CreateAsync();
```
(Please consult the image's documentation for supported variables; list of official images can be found at the [Supported Databases](#supported-databases) section.)

Finally, you may use a different image than the officially supported one (not recommended):

```csharp
var container = await new DockerTools<SqlServer>()
           .WithParameters(options => options
               .WithImage("vibs2006/sql_server_fts")) // adds full text support that the official image lacks
           .CreateAsync();
```
#### Connecting to the Docker Engine
By default, DockerTools will attempt to identify the best connection to the local Docker instance. if you want to force use of the Remote API, you can do so:

```csharp
var container = await new DockerTools<Postgres>()
           .WithConnection(options => options.ConnectionType = new RemoteApiConnection(new Uri("http://localhost:2375")))
           .CreateAsync();
```

### Using the container
You can obtain the container's connection string by checking its `ConnectionString` property like this:
```csharp
var connectionString = container.ConnectionString; // returns a string value
```

Inside the `ConnectionString` object, you can also directly access the database name, port, username, and password:

```csharp
var database = container.ConnectionString.Database;
var port = container.ConnectionString.Port;
var username = container.ConnectionString.Username;
var password = container.ConnectionString.Password;
```

You can also run scripts by calling the following method:
```csharp
var result = container.RunScriptAsync("SELECT * from users");
```
The method will return an object indicating if the script ran successfully, with an optional field
indicating the error if one exists.

### Clean up
When done with the container, simply invoke its `DisposeAsync` method:
```csharp
container.DisposeAsync();
```
This will remove the container from the Docker host and dispose of the underlying connection to it.

Note that the `CreateAsync()` method generates an `IAsyncDisposable` object, meaning you can wrap
the entire usage of it in an `await using` statement:
```csharp
await using (var container = await new DockerTools<Postgres>().CreateAsync())
{
    ...
}
```

Alternatively, if disposing is not an option, you can initialize the container with the `WithCleanUp(true)` method:

```csharp
var container = await new DockerTools<Postgres>()
           .WithCleanUp(true)
           .CreateAsync();
```

### Supported Databases
| Container | Image | Version     | Notes   |
|-----------|-------|-------------|---------|
| Postgres  | postgres | 14.3        | |
| Postgis   | postgis/postgis | 14-3.4      ||
| SQL Server | mcr.microsoft.com/mssql/server | 2022-latest ||
| SQL Server (Full Text Search) | vibs2006/sql_server_fts | 2022 | Will be deprecated in a future version in favor of the `WithImage()` option |