namespace miguelcouteirorodrigues.DockerTools.Models;

/// <summary>
/// Holds information regarding the container's
/// database's connection string.
/// </summary>
public class ConnectionString
{
    private readonly string _value;
    
    public string Database { get; }

    public string Username { get; }

    public string Password { get; }

    public string Port { get; }

    internal ConnectionString(string value, string port, string database, string username, string password)
    {
        _value = value;
        Port = port;
        Database = database;
        Username = username;
        Password = password;
    }

    internal ConnectionString()
    {
        _value = string.Empty;
        Port = string.Empty;
        Database = string.Empty;
        Username = string.Empty;
        Password = string.Empty;
    }
    
    public override string ToString() => _value;
    
    public static implicit operator string(ConnectionString connectionString) => connectionString.ToString();
}