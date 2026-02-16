using System.Data;
using Microsoft.Extensions.Options;

namespace WSIST.Engine;
using Microsoft.Data.SqlClient;

public class Database
{
    public readonly SqlConnection Connection;

    public Database(IOptions<DatabaseOption> options)
    {
        Connection = new SqlConnection(options.Value.ConnectionString);
        Connection.Open();
    }

    public DataTable Query(string sqlQuery, Dictionary<string, object>? parameters = null)
    {
        using var command = new SqlCommand(
            sqlQuery,
            Connection
        );

        if (parameters != null){
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