using System.Data;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace WSIST.Engine;

public class Database
{
    private readonly string connectionString;

    public Database(IOptions<DatabaseOption> options)
    {
        connectionString = options.Value.ConnectionString;
    }

    public DataTable Query(string sqlQuery, Dictionary<string, object>? parameters = null)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        using var command = new MySqlCommand(sqlQuery, connection);

        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue('@' + param.Key, param.Value);
            }
        }

        using var reader = command.ExecuteReader();

        var table = new DataTable();
        table.Load(reader);

        return table;
    }
}

public record DatabaseOption
{
    public string ConnectionString { get; set; } = null!;
}
