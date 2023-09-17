using Microsoft.AspNetCore.Mvc;
using ToDoList.Infrastructure.Data.Daos;

namespace ToDoList.Api.Controllers;

[Route("to-dos")]
public class ToDoController : ControllerBase
{
    private readonly ToDoDao _toDoDao;

    public ToDoController(ToDoDao toDoDao)
        => _toDoDao = toDoDao;
}

