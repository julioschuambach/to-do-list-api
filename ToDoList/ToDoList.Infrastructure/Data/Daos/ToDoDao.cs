using ToDoList.Common.Dtos.ToDoDtos;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Data.Contexts;

namespace ToDoList.Infrastructure.Data.Daos;

public class ToDoDao
{
    private readonly ToDosDataContext _context;

    public ToDoDao(ToDosDataContext context)
        => _context = context;

    public async Task Insert(ToDo toDo)
    {
        await _context.Insert(toDo);
    }

    public async Task<ToDo?> SelectById(Guid id)
    {
        return await _context.SelectById(id);
    }

    public async Task<IEnumerable<ToDo>> SelectAll()
    {
        return await _context.SelectAll();
    }

    public async Task Update(ToDo toDo, UpdateToDoDto updateDto)
    {
        await _context.Update(toDo, updateDto);
    }

    public async Task Delete(Guid id)
    {
        await _context.Delete(id);
    }
}
