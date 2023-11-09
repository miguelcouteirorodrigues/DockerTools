﻿using Kenbi.DockerTools.Containers.Interfaces;

namespace Kenbi.DockerTools.Containers;

/// <summary>
/// 
/// </summary>
public class PostgisContainerParameters : IContainerParameters
{
    internal string Username;
    internal string Password;
    internal string Database;
    internal string Version;
    
    /// <summary>
    /// Allows for configuration of a default user.
    /// If not set, default "postgres" is used.
    /// </summary>
    /// <param name="username">The username to set.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If an empty or null value is supplied.</exception>
    public PostgisContainerParameters WithUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentNullException(
                nameof(username),
                "Username cannot be empty. If you wish to use the default value, do not invoke this method.");
        }

        this.Username = username;

        return this;
    }

    /// <summary>
    /// Allows for configuration of a default password.
    /// If not set, default "postgres" is used.
    /// </summary>
    /// <param name="password">The password to set.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If an empty or null value is supplied.</exception>
    public PostgisContainerParameters WithPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentNullException(
                nameof(password),
                "Password cannot be empty. If you wish to use the default value, do not invoke this method.");
        }
        
        this.Password = password;
        
        return this;
    }

    /// <summary>
    /// Allows for configuration of a default database.
    /// </summary>
    /// <param name="database">The database name to set.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If an empty or null value is supplied.</exception>
    public PostgisContainerParameters WithDefaultDatabase(string database)
    {
        if (string.IsNullOrWhiteSpace(database))
        {
            throw new ArgumentNullException(
                nameof(database),
                "Database cannot be empty. If you wish to use the default value, do not invoke this method.");
        }
        
        this.Database = database;
        
        return this;
    }
    
    /// <summary>
    /// Allows for configuration of a specific version of the container.
    /// </summary>
    /// <param name="version">The version to set.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If an empty or null value is supplied.</exception>
    public PostgisContainerParameters WithVersion(string version)
    {
        if (string.IsNullOrWhiteSpace(version))
        {
            throw new ArgumentNullException(
                nameof(version),
                "Version cannot be empty. If you wish to use the default value, do not invoke this method.");
        }
        
        this.Version = version;
        
        return this;
    }
}