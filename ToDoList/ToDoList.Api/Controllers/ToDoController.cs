using Microsoft.AspNetCore.Mvc;
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
    public IActionResult Create([FromBody] CreateToDoDto createDto)
    {
        if (!ModelState.IsValid)
            return StatusCode(400, ModelState);

        try
        {
            _toDoDao.Insert(new ToDo(createDto));
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}

