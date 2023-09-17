using ToDoList.Infrastructure.Interfaces.Data;

namespace ToDoList.Infrastructure.Data.Contexts;

public class ToDosDataContext
{
    private readonly IDbConnection _connection;

    public ToDosDataContext(IDbConnection connection) 
        => _connection = connection;
}
