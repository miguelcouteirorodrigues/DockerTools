namespace miguelcouteirorodrigues.DockerTools.Options.Container;

/// <summary>
/// Options for setting container configurations.
/// </summary>
public class DockerToolsContainerOptions
{
    internal string? Image;
    internal string? Tag;
    internal string? Database;
    internal string? Username;
    internal string? Password;
    internal readonly IList<string> EnvironmentVariables = new List<string>();
    internal Guid? InstanceId;

    /// <summary>
    /// Allows using a different image than the default.
    /// It's recommended that the image used match the default in behavior.
    /// Otherwise, issues such as incorrect health checks may occur.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <returns>The <see cref="DockerToolsContainerOptions"/> instance.</returns>
    public DockerToolsContainerOptions WithImage(string image)
    {
        this.Image = image;

        return this;
    }

    /// <summary>
    /// Allows using a specific version of the container image.
    /// </summary>
    /// <param name="value">The version to use; check image repository for valid versions.</param>
    /// <returns>The <see cref="DockerToolsContainerOptions"/> instance.</returns>
    public DockerToolsContainerOptions WithTag(string value)
    {
        this.Tag = value;

        return this;
    }
    
    /// <summary>
    /// Replaces the default database.
    /// </summary>
    /// <param name="value">The new database name.</param>
    /// <returns>The <see cref="DockerToolsContainerOptions"/> instance.</returns>
    public DockerToolsContainerOptions WithDatabase(string value)
    {
        this.Database = value;
        
        return this;
    }

    /// <summary>
    /// Replaces the default username.
    /// </summary>
    /// <param name="value">The new username.</param>
    /// <returns>The <see cref="DockerToolsContainerOptions"/> instance.</returns>
    public DockerToolsContainerOptions WithUsername(string value)
    {
        this.Username = value;

        return this;
    }

    /// <summary>
    /// Replaces the default password.
    /// </summary>
    /// <param name="value">The new password.</param>
    /// <returns>The <see cref="DockerToolsContainerOptions"/> instance.</returns>
    public DockerToolsContainerOptions WithPassword(string value)
    {
        this.Password = value;

        return this;
    }

    /// <summary>
    /// Adds an environment variable.
    /// Check the image's documentation for a list of supported variables.
    /// </summary>
    /// <param name="value">The environment variable to add to the default collection of the container.</param>
    /// <returns>The <see cref="DockerToolsContainerOptions"/> instance.</returns>
    public DockerToolsContainerOptions AddEnvironmentVariable(string value)
    {
        this.EnvironmentVariables.Add(value);

        return this;
    }

    internal DockerToolsContainerOptions WithInstanceId(Guid value)
    {
        this.InstanceId = value;

        return this;
    }
}