﻿using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        try
        {
            ToDo? toDo = _toDoDao.SelectById(id);

            if (toDo == null)
                return StatusCode(404);

            return StatusCode(200, toDo);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            return StatusCode(200, _toDoDao.SelectAll());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateToDoDto updateDto)
    {
        if (!ModelState.IsValid)
            return StatusCode(400, ModelState);

        try
        {
            ToDo? toDo = _toDoDao.SelectById(id);

            if (toDo == null)
                return StatusCode(404);

            _toDoDao.Update(toDo, updateDto);
            return StatusCode(200, _toDoDao.SelectById(id));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        try
        {
            ToDo? toDo = _toDoDao.SelectById(id);

            if (toDo == null)
                return StatusCode(404);

            _toDoDao.Delete(id);
            return StatusCode(200);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}

