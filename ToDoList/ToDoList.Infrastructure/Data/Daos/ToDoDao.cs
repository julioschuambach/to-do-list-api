using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Data.Contexts;

namespace ToDoList.Infrastructure.Data.Daos;

public class ToDoDao
{
    private readonly ToDosDataContext _context;

    public ToDoDao(ToDosDataContext context)
        => _context = context;

    public void Insert(ToDo toDo)
    {
        _context.Insert(toDo);
    }
}
