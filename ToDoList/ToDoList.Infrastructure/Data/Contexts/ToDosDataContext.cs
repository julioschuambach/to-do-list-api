using System.Text;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Interfaces.Data;

namespace ToDoList.Infrastructure.Data.Contexts;

public class ToDosDataContext
{
    private readonly IDbConnection _connection;

    public ToDosDataContext(IDbConnection connection)
        => _connection = connection;

    public void Insert(ToDo toDo)
    {
        using (var connection = _connection.GetConnection())
        {
            using (var command = connection.CreateCommand())
            {
                StringBuilder sb = new();
                sb.AppendLine("INSERT INTO [ToDos]");
                sb.Append("([Id], [Description], [Done], [CreatedDate], [LastUpdatedDate]");
                sb.AppendLine(toDo.ExpectedCompletionDate.HasValue ? ", [ExpectedCompletionDate])" : ")");
                sb.AppendLine("VALUES");
                sb.Append("(@Id, @Description, @Done, @CreatedDate, @LastUpdatedDate");
                sb.AppendLine(toDo.ExpectedCompletionDate.HasValue ? ", @ExpectedCompletionDate)" : ")");

                command.CommandText = sb.ToString();

                command.Parameters.AddWithValue("@Id", toDo.Id);
                command.Parameters.AddWithValue("@Description", toDo.Description);
                command.Parameters.AddWithValue("@Done", toDo.Done);
                command.Parameters.AddWithValue("@CreatedDate", toDo.CreatedDate);
                command.Parameters.AddWithValue("@LastUpdatedDate", toDo.LastUpdatedDate);
                if (toDo.ExpectedCompletionDate.HasValue)
                    command.Parameters.AddWithValue("@ExpectedCompletionDate", toDo.ExpectedCompletionDate);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
