using System.Diagnostics;
using Docker.DotNet;
using Docker.DotNet.Models;
using miguelcouteirorodrigues.DockerTools.Containers.Templates;
using miguelcouteirorodrigues.DockerTools.Models;
using miguelcouteirorodrigues.DockerTools.Utils;

namespace miguelcouteirorodrigues.DockerTools.Operations;

internal static class StartContainerOperations
{
    private const string Healthy = "healthy";
    private const string Unhealthy = "unhealthy";

    internal static Task<bool> TryStartContainerAsync(DockerClient client, string id, CancellationToken token)
    {
        return client.Containers.StartContainerAsync(id, new ContainerStartParameters(), token);
    }

    internal static async Task<bool> IsContainerHealthy(DockerClient client, string id, IContainerTemplate container, CancellationToken token)
    {
        bool? result = null;

        var stopWatch = new Stopwatch();
        stopWatch.Start();
        
        do
        {
            var response = await client.Containers.InspectContainerAsync(id, token).ConfigureAwait(false);
            var health = response.State.Health;

            if (health == null)
            {
                return true; // no healthcheck, no way of knowing if container is ready, assume so and move forward
            }

            /*
             * It seems that Docker Engine already handles most of this by itself, masking any failures
             * during bootstrap under the label of "starting"; as such, I'm merely checking for either
             * "healthy" or "unhealthy" labels on the healthcheck instead of trying to recreate its behavior
             */
            result = health.Status switch
            {
                Healthy => true,
                Unhealthy => false,
                _ => result
            };
        } while (result == null);

        return result.Value;
    }

    internal static async Task<IEnumerable<PortConfiguration>> GetRunningPortsAsync(DockerClient client, string id, CancellationToken token)
    {
        var response = await client.Containers.InspectContainerAsync(id, token).ConfigureAwait(false);

        var portConfiguration = response.NetworkSettings.Ports.ConvertToPortConfiguration();

        return portConfiguration;
    }
}