using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace MeChat.Infrastucture.Dapper;
public class ApplicationDbContext
{
    private readonly IConfiguration configuration;
    private readonly string connectionString;
    private readonly SqlConnection connection;
    private DbTransaction? dbTransaction;

    public ApplicationDbContext(IConfiguration configuration)
    {
        this.configuration = configuration;
        this.connectionString = this.configuration.GetConnectionString("SqlServer") ?? string.Empty;
        connection = CreateConnection();
        dbTransaction = null;
    }

    private SqlConnection CreateConnection()
    {
        return new SqlConnection(connectionString);
    }

    public SqlConnection GetConnection()
    {
        return connection;
    }

    public async Task DisposeAsync() 
    {
        await connection.DisposeAsync();
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (dbTransaction != null) return;
        this.dbTransaction = await connection.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (dbTransaction == null) return;
        await dbTransaction.CommitAsync();
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (dbTransaction == null) return;
        await dbTransaction.RollbackAsync();
    }
}
