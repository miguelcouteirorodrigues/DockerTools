using Docker.DotNet;
using miguelcouteirorodrigues.DockerTools.Models;
using miguelcouteirorodrigues.DockerTools.Operations;
using miguelcouteirorodrigues.DockerTools.Options.Container;

namespace miguelcouteirorodrigues.DockerTools.Containers.Templates;

/// <summary>
/// Creates a new Postgres container.
/// </summary>
public sealed class Postgres : IContainerTemplate
{
    public string Image => "postgres";
    public string Tag { get; private set; } = "14.3";
    public string Database { get; private set; } = "postgres";
    public string Username { get; private set; } = "postgres";
    public string Password { get; private set; } = "postgres";
    Dictionary<string, string> IContainerTemplate.Volumes { get; } = new();

    IEnumerable<PortConfiguration> IContainerTemplate.Ports => new List<PortConfiguration>
    {
        new()
        {
            Container = "5432"
        }
    };

    public IList<string> BaseEnvironmentVariables => new List<string>
    {
        $"POSTGRES_USER={this.Username}",
        $"PGUSER={this.Username}",
        $"POSTGRES_DB={this.Database}",
        $"PGDATABASE={this.Database}",
        $"POSTGRES_PASSWORD={this.Password}",
        $"PGPASSWORD={this.Password}"
    };

    public List<string> AdditionalEnvironmentVariables { get; private set; } = new();

    HealthCheck IContainerTemplate.HealthCheck => new HealthCheck
    {
        Command = "pg_isready",
        Interval = new TimeSpan(0, 0, 30),
        Timeout = new TimeSpan(0, 0, 2),
        Retries = 5,
        StartPeriod = 60
    };

    void IContainerTemplate.ReplaceDefaultParameters(DockerToolsContainerOptions options)
    {
        if (!string.IsNullOrWhiteSpace(options.Tag))
        {
            this.Tag = options.Tag;
        }

        if (!string.IsNullOrWhiteSpace(options.Database))
        {
            this.Database = options.Database;
        }

        if (!string.IsNullOrWhiteSpace(options.Username))
        {
            this.Username = options.Username;
        }

        if (!string.IsNullOrWhiteSpace(options.Password))
        {
            this.Password = options.Password;
        }
        
        this.AdditionalEnvironmentVariables.AddRange(options.EnvironmentVariables);
    }
    
    Task<ScriptExecutionResult> IContainerTemplate.PerformPostStartOperationsAsync(DockerClient client, string id, CancellationToken token)
    {
        return Task.FromResult(new ScriptExecutionResult(null));
    }

    ConnectionString IContainerTemplate.GetConnectionString(string hostPort)
    {
        return new ConnectionString(
            $"Server=localhost;Port={hostPort};Database={this.Database};User Id={this.Username};Password={this.Password};Command Timeout=0;",
            hostPort,
            this.Database,
            this.Username,
            this.Password
        );
    }

    Task<ScriptExecutionResult> IContainerTemplate.RunScriptAsync(DockerClient client, string id, string script, CancellationToken token)
    {
        var command = $"psql -U {this.Username} -d {this.Database} -q -c";

        script = "SET client_min_messages TO WARNING; " + script;

        return CommandExecutionOperations.RunScriptAsync(client, id, command, script, token);
    }
}