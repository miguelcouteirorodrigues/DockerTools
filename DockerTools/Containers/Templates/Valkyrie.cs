using Docker.DotNet;
using miguelcouteirorodrigues.DockerTools.Models;
using miguelcouteirorodrigues.DockerTools.Options.Container;

namespace miguelcouteirorodrigues.DockerTools.Containers.Templates;

internal class Valkyrie : IContainerTemplate
{
    public string Image => "ghcr.io/kenbitech/dockertools-valkyrie";
    public string Tag => "1.0";
    public string Database { get; }
    public string Username { get; }
    public string Password { get; }
    private Guid _instanceId;
    Dictionary<string, string> IContainerTemplate.Volumes => new()
    {
        { "/var/run/docker.sock", "/var/run/docker.sock" }
    };

    IEnumerable<PortConfiguration> IContainerTemplate.Ports { get; }

    public List<string> EnvironmentVariables => new()
    {
        $"InstanceId={this._instanceId}"
    };

    HealthCheck IContainerTemplate.HealthCheck { get; }

    void IContainerTemplate.ReplaceDefaultParameters(DockerToolsContainerOptions options)
    {
        if (options.InstanceId != null)
        {
            this._instanceId = options.InstanceId.Value;
        }
    }

    Task<ScriptExecutionResult> IContainerTemplate.PerformPostStartOperationsAsync(DockerClient client, string id, CancellationToken token)
    {
        return Task.FromResult(new ScriptExecutionResult(null));
    }

    ConnectionString IContainerTemplate.GetConnectionString(string hostPort)
    {
        return new ConnectionString();
    }

    Task<ScriptExecutionResult> IContainerTemplate.RunScriptAsync(DockerClient client, string id, string script, CancellationToken token)
    {
        return Task.FromResult(new ScriptExecutionResult(null));
    }
}