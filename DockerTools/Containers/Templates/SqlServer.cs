﻿using Docker.DotNet;
using miguelcouteirorodrigues.DockerTools.Models;
using miguelcouteirorodrigues.DockerTools.Operations;
using miguelcouteirorodrigues.DockerTools.Options.Container;

namespace miguelcouteirorodrigues.DockerTools.Containers.Templates;

/// <summary>
/// Creates a new SQL Server container.
/// </summary>
public sealed class SqlServer : IContainerTemplate
{
    private const string SqlCmd = "/opt/mssql-tools18/bin/sqlcmd";
    
    public string Image => "mcr.microsoft.com/mssql/server";
    public string Tag { get; private set; } = "2022-latest";
    public string Database { get; private set; } = "DockerTools";
    public string Username { get; private set; } = "sa";
    public string Password { get; private set; } = "ABc123$%";
    Dictionary<string, string> IContainerTemplate.Volumes { get; }

    IEnumerable<PortConfiguration> IContainerTemplate.Ports => new List<PortConfiguration>()
    {
        new()
        {
            Container = "1433"
        }
    };
    
    IList<string> IContainerTemplate.EnvironmentVariables => new List<string>
    {
        "ACCEPT_EULA=true",
        $"MSSQL_SA_PASSWORD={this.Password}",
        $"DB_USER={this.Username}",
        $"SA_PASSWORD={this.Password}"
        
    };
    HealthCheck IContainerTemplate.HealthCheck => new()
    {
        Command = $"{GetBaseCommand()} -Q 'select 1' -b -o /dev/null",
        Interval = new TimeSpan(0, 0, 2),
        Timeout = new TimeSpan(0, 0, 2),
        Retries = 5,
        StartPeriod = 10
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
    }
    
    Task<ScriptExecutionResult> IContainerTemplate.PerformPostStartOperationsAsync(DockerClient client, string id, CancellationToken token)
    {
        var command = $"{GetBaseCommand()} -Q";
        var script = $"CREATE DATABASE {this.Database};";
        
        return CommandExecutionOperations.RunScriptAsync(client, id, command, script, token);
    }

    string IContainerTemplate.GetConnectionString(string hostPort)
        => $"Server=::1,{hostPort};Database={this.Database};User Id={this.Username};Password={this.Password};Encrypt=false;";

    Task<ScriptExecutionResult> IContainerTemplate.RunScriptAsync(DockerClient client, string id, string script, CancellationToken token)
    {
        var command = $"{GetBaseCommand()} -d {this.Database} -r 1 -Q";
        
        return CommandExecutionOperations.RunScriptAsync(client, id, command, script, token);
    }

    private string GetBaseCommand()
    {
        return $"{SqlCmd} -U {this.Username} -P {this.Password} -C";
    }
}