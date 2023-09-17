using Microsoft.Data.SqlClient;

namespace ToDoList.Infrastructure.Interfaces.Data;

public interface IDbConnection
{
    SqlConnection GetConnection();
}
