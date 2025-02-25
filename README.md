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

By default, DockerTools will attempt to identify the best connection to the local Docker instance. if you want to force use of the Remote API, you can do so:

```csharp
var container = await new DockerTools<Postgres>()
           .WithConnection(options => options.ConnectionType = new RemoteApiConnection(new Uri("http://localhost:2375")))
           .CreateAsync();
```

If, for some reason, you cannot properly dispose of a container, you can use the `WithCleanUp(true)` option:

```csharp
var container = await new DockerTools<Postgres>()
           .WithCleanUp(true)
           .CreateAsync();
```

All methods can be combined:

```csharp
var container = await new DockerTools<Postgres>()
           .WithParameters(options => options
               .WithDatabase("myDatabase")
               .WithUsername("myUser")
               .WithPassword("myPassword")
               .WithTag("14.3"))
           .WithConnection(options => options.ConnectionType = new RemoteApiConnection(new Uri("http://localhost:2375")))
           .WithCleanUp(true)
           .CreateAsync();
```

### Using the container
You can obtain the container's connection string by checking its `ConnectionString` property like this:
```csharp
var connectionString = container.ConnectionString;
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

Alternatively, if disposing is not an option, you can initialize the container with the `WithCleanUp(true)` method (see the [Creating a Container](#creating-a-container) section).

### Supported Databases
| Container | Image | Version     |
|-----------|-------|-------------|
| Postgres  | postgres | 14.3        |
| Postgis   | postgis/postgis | 14-3.4      |
| SQL Server | mcr.microsoft.com/mssql/server | 2022-latest |
| SQL Server (Full Text Search) | vibs2006/sql_server_fts | 2022 |