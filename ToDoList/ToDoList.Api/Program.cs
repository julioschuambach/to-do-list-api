using ToDoList.Infrastructure.Data;
using ToDoList.Infrastructure.Data.Contexts;
using ToDoList.Infrastructure.Data.Daos;
using ToDoList.Infrastructure.Interfaces.Data;

namespace ToDoList.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();

        builder.Services.AddScoped<IDbConnection, DbConnection>();
        builder.Services.AddTransient<ToDosDataContext>();
        builder.Services.AddTransient<ToDoDao>();

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}
