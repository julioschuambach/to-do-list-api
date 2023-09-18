using System.Text;
using ToDoList.Common.Dtos.ToDoDtos;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Interfaces.Data;

namespace ToDoList.Infrastructure.Data.Contexts;

public class ToDosDataContext
{
    private readonly IDbConnection _connection;

    public ToDosDataContext(IDbConnection connection)
        => _connection = connection;

    public async Task Insert(ToDo toDo)
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
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }

    public async Task<ToDo?> SelectById(Guid id)
    {
        ReadToDoDto readDto = new();

        using (var connection = _connection.GetConnection())
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    SELECT [Id], [Description], [Done], [CreatedDate], [LastUpdatedDate], [CompletionDate], [ExpectedCompletionDate]
                    FROM [ToDos]
                    WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    await connection.OpenAsync();
                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        readDto.Fill(
                            (Guid)reader["Id"],
                            (string)reader["Description"],
                            (bool)reader["Done"],
                            reader["ExpectedCompletionDate"] as DateTime?,
                            reader["CompletionDate"] as DateTime?,
                            (DateTime)reader["CreatedDate"],
                            (DateTime)reader["LastUpdatedDate"]
                            );
                    }

                    await connection.CloseAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        if (readDto.Id == Guid.Empty)
            return null;
        else
            return new ToDo(readDto);
    }

    public async Task<IEnumerable<ToDo>> SelectAll()
    {
        List<ToDo> toDos = new();

        using (var connection = _connection.GetConnection())
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    SELECT [Id], [Description], [Done], [CreatedDate], [LastUpdatedDate], [CompletionDate], [ExpectedCompletionDate]
                    FROM [ToDos]";

                try
                {
                    await connection.OpenAsync();
                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        ReadToDoDto readDto = new();
                        readDto.Fill(
                            (Guid)reader["Id"],
                            (string)reader["Description"],
                            (bool)reader["Done"],
                            reader["ExpectedCompletionDate"] as DateTime?,
                            reader["CompletionDate"] as DateTime?,
                            (DateTime)reader["CreatedDate"],
                            (DateTime)reader["LastUpdatedDate"]
                            );

                        toDos.Add(new ToDo(readDto));
                    }

                    await connection.CloseAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        return toDos;
    }

    public async Task Update(ToDo toDo, UpdateToDoDto updateDto)
    {
        using (var connection = _connection.GetConnection())
        {
            using (var command = connection.CreateCommand())
            {
                List<string> criterias = new();
                if (toDo.Description != updateDto.Description) criterias.Add("[Description] = @Description");
                if (toDo.Done != updateDto.Done) criterias.Add("[Done] = @Done");
                if (toDo.ExpectedCompletionDate != updateDto.ExpectedCompletionDate) criterias.Add("[ExpectedCompletionDate] = @ExpectedCompletionDate");
                if (toDo.CompletionDate != updateDto.CompletionDate) criterias.Add("[CompletionDate] = @CompletionDate");
                criterias.Add("[LastUpdatedDate] = @LastUpdatedDate");

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UPDATE [ToDos]");
                sb.AppendLine("SET");
                sb.AppendLine(string.Join(", ", criterias));
                sb.AppendLine("WHERE [Id] = @Id");

                command.CommandText = sb.ToString();

                command.Parameters.AddWithValue("@Id", toDo.Id);
                if (toDo.Description != updateDto.Description) command.Parameters.AddWithValue("@Description", updateDto.Description);
                if (toDo.Done != updateDto.Done) command.Parameters.AddWithValue("@Done", updateDto.Done);
                if (toDo.ExpectedCompletionDate != updateDto.ExpectedCompletionDate) command.Parameters.AddWithValue("@ExpectedCompletionDate", updateDto.ExpectedCompletionDate);
                if (toDo.CompletionDate != updateDto.CompletionDate) command.Parameters.AddWithValue("@CompletionDate", updateDto.CompletionDate);
                command.Parameters.AddWithValue("@LastUpdatedDate", DateTime.Now);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }

    public async Task Delete(Guid id)
    {
        using (var connection = _connection.GetConnection())
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    DELETE FROM [ToDos]
                    WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
