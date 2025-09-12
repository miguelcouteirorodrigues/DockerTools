using Docker.DotNet;
using Docker.DotNet.Models;
using miguelcouteirorodrigues.DockerTools.Containers.Templates;
using miguelcouteirorodrigues.DockerTools.Utils;

namespace miguelcouteirorodrigues.DockerTools.Operations;

internal static class CreateContainerOperations
{
    internal static async Task<string> CreateContainerAsync(DockerClient client, IContainerTemplate container, Guid instanceId, CancellationToken token)
    {
        var @params = ConfigureCreation(container, instanceId);

        var response = await client.Containers.CreateContainerAsync(@params, token).ConfigureAwait(false);

        return response.ID;
    }

    private static CreateContainerParameters ConfigureCreation(IContainerTemplate container, Guid instanceId)
    {
        var labels = new Dictionary<string, string>
        {
            { "de.kenbi.dockertools.instance", instanceId.ToString() },
            { "de.kenbi.dockertools", bool.TrueString }
        };
        
        if (container is Valkyrie)
        {
            labels.Add("de.kenbi.dockertools.valkyrie", bool.TrueString);
        }
        
        var environmentVariables = new List<string>(container.BaseEnvironmentVariables.Count + container.AdditionalEnvironmentVariables.Count);
        environmentVariables.AddRange(container.BaseEnvironmentVariables);
        environmentVariables.AddRange(container.AdditionalEnvironmentVariables);
        
        return new CreateContainerParameters
        {
            Image = string.Concat(container.Image, ":", container.Tag),
            ExposedPorts = container.Ports.ConvertToExposedPorts(),
            HostConfig = new HostConfig
            {
                PortBindings = container.Ports.ConvertToPortBindings(),
                RestartPolicy = new RestartPolicy
                {
                    Name = RestartPolicyKind.No
                },
                Binds = container.Volumes.ConvertToVolumeBinds()
            },
            Env = environmentVariables,
            Healthcheck = container.HealthCheck == null
                ? null
                : new HealthConfig
                {
                    Test = new List<string>
                    {
                        "CMD-SHELL",
                        container.HealthCheck.Command
                    },
                    Interval = container.HealthCheck.Interval,
                    Timeout = container.HealthCheck.Timeout,
                    Retries = container.HealthCheck.Retries,
                    StartPeriod = container.HealthCheck.StartPeriod * 1000000 * 1000 // Docker expects this in nanoseconds, DockerTools configures this in seconds
                },
            Labels = labels,
            Volumes = container.Volumes.ConvertToVolumes()
        };
    }
}