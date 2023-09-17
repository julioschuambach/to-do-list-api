using ToDoList.Common.Dtos.ToDoDtos;
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

    public ToDo? SelectById(Guid id)
    {
        return _context.SelectById(id);
    }

    public IEnumerable<ToDo> SelectAll()
    {
        return _context.SelectAll();
    }

    public void Update(ToDo toDo, UpdateToDoDto updateDto)
    {
        _context.Update(toDo, updateDto);
    }

    public void Delete(Guid id)
    {
        _context.Delete(id);
    }
}
