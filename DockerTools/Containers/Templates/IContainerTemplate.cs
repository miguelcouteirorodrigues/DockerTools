using Docker.DotNet;
using miguelcouteirorodrigues.DockerTools.Models;
using miguelcouteirorodrigues.DockerTools.Options.Container;

namespace miguelcouteirorodrigues.DockerTools.Containers.Templates;

/// <summary>
/// Reference for creating base container configurations.
/// </summary>
public interface IContainerTemplate
{
    /// <summary>
    /// The base image used.
    /// </summary>
    public string Image { get; }

    /// <summary>
    /// The version of the image.
    /// </summary>
    public string Tag { get; }
    
    /// <summary>
    /// Default database name.
    /// </summary>
    public string Database { get; }

    /// <summary>
    /// Default username.
    /// </summary>
    public string Username { get; }

    /// <summary>
    /// Default password.
    /// </summary>
    public string Password { get; }
    
    internal IEnumerable<PortConfiguration> Ports { get; }
    
    internal IList<string> EnvironmentVariables { get; }
    
    internal HealthCheck? HealthCheck { get; }
    
    internal Dictionary<string, string> Volumes { get; }

    internal void ReplaceDefaultParameters(DockerToolsContainerOptions options);

    internal Task<ScriptExecutionResult> PerformPostStartOperationsAsync(DockerClient client, string id, CancellationToken token);

    internal ConnectionString GetConnectionString(string hostPort);

    internal Task<ScriptExecutionResult> RunScriptAsync(DockerClient client, string id, string script, CancellationToken token);
}