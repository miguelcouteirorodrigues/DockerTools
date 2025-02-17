using Docker.DotNet;
using miguelcouteirorodrigues.DockerTools.Exceptions;

namespace miguelcouteirorodrigues.DockerTools.Options.Connection.ConnectionTypes;

public abstract class DockerToolsConnectionType
{
    protected DockerClient? Client;
    internal Uri? Uri { get; set; }
    
    internal async Task PingAsync(CancellationToken token)
    {
        try
        {
            await Client!.System.PingAsync(token).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new DockerUnreachableException(ex);
        }
    }
}