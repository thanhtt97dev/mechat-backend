using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MeChat.Infrastucture.Dapper;
public class ApplicationDbContext
{
    private readonly IConfiguration configuration;
    private readonly string connectionString;

    public ApplicationDbContext(IConfiguration configuration)
    {
        this.configuration = configuration;
        connectionString = configuration.GetConnectionString("SqlServer") ?? string.Empty;
    }

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(connectionString);
    }
}
