﻿using Docker.DotNet;
using miguelcouteirorodrigues.DockerTools.Containers.Templates;
using miguelcouteirorodrigues.DockerTools.Models;

namespace miguelcouteirorodrigues.DockerTools.Containers;

/// <summary>
/// Represents an instance of a DockerTools container tracker.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IContainer<T> : IAsyncDisposable, IDisposable where T : IContainerTemplate
{
    internal DockerClient Client { get; }
    
    /// <summary>
    /// The identifier of the container on Docker.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// The connection string to the database instance.
    /// </summary>
    public string ConnectionString { get; }

    /// <summary>
    /// Run a script on the container's database.
    /// </summary>
    /// <param name="script">The script to run.</param>
    /// <param name="token">A cancellation token. Optional.</param>
    /// <returns>True if script ran successfully; false otherwise.</returns>
    Task<ScriptExecutionResult> RunScriptAsync(string script, CancellationToken token = default);
}