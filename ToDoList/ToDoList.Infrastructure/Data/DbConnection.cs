using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ToDoList.Infrastructure.Interfaces.Data;

namespace ToDoList.Infrastructure.Data;

public class DbConnection : IDbConnection
{
    private readonly IConfiguration _configuration;
    private const string DEFAULT_CONNECTION = "Default";

    public DbConnection(IConfiguration configuration)
        => _configuration = configuration;

    public DbConnection() { }

    public SqlConnection GetConnection()
        => new SqlConnection(_configuration.GetConnectionString(DEFAULT_CONNECTION));
}
