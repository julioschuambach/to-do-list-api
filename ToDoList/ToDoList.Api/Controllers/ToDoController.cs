using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.Extensions;
using ToDoList.Api.ViewModels;
using ToDoList.Common.Dtos.ToDoDtos;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Data.Daos;

namespace ToDoList.Api.Controllers;

[Route("to-dos")]
public class ToDoController : ControllerBase
{
    private readonly ToDoDao _toDoDao;

    public ToDoController(ToDoDao toDoDao)
        => _toDoDao = toDoDao;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateToDoDto createDto)
    {
        if (!ModelState.IsValid)
            return StatusCode(400, new ResultViewModel<string>(ModelState.GetErrors()));

        try
        {
            ToDo toDo = new(createDto);
            await _toDoDao.Insert(toDo);
            return StatusCode(201, new ResultViewModel<ToDo?>(await _toDoDao.SelectById(toDo.Id)));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<string>(ex.Message));
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            ToDo? toDo = await _toDoDao.SelectById(id);

            if (toDo == null)
                return StatusCode(404, new ResultViewModel<string>("Not found an entity with the given id."));

            return StatusCode(200, new ResultViewModel<ToDo>(toDo));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<string>(ex.Message));
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return StatusCode(200, new ResultViewModel<IEnumerable<ToDo>>(await _toDoDao.SelectAll()));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<string>(ex.Message));
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateToDoDto updateDto)
    {
        if (!ModelState.IsValid)
            return StatusCode(400, new ResultViewModel<string>(ModelState.GetErrors()));

        try
        {
            ToDo? toDo = await _toDoDao.SelectById(id);

            if (toDo == null)
                return StatusCode(404, new ResultViewModel<string>("Not found an entity with the given id."));

            await _toDoDao.Update(toDo, updateDto);
            return StatusCode(200, new ResultViewModel<ToDo?>(await _toDoDao.SelectById(id)));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<string>(ex.Message));
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            ToDo? toDo = await _toDoDao.SelectById(id);

            if (toDo == null)
                return StatusCode(404, new ResultViewModel<string>("Not found an entity with the given id."));

            await _toDoDao.Delete(id);
            return StatusCode(200, new ResultViewModel<ToDo>(toDo));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<string>(ex.Message));
        }
    }
}

