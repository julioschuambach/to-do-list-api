using ToDoList.Common.Dtos.ToDoDtos;
using ToDoList.Domain.Entities.Base;

namespace ToDoList.Domain.Entities;

public class ToDo : EntityBase
{
    public string Description { get; private set; }
    public bool Done { get; private set; }
    public DateTime? ExpectedCompletionDate { get; private set; }
    public DateTime? CompletionDate { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime LastUpdatedDate { get; private set; }

    public ToDo(CreateToDoDto createDto)
    {
        Description = createDto.Description;
        Done = false;
        ExpectedCompletionDate = createDto.ExpectedCompletionDate;
        CompletionDate = null;
        CreatedDate = DateTime.Now;
        LastUpdatedDate = DateTime.Now;
    }

    public void MarkAsDone()
    {
        Done = true;
        CompletionDate = DateTime.Now;
    }

    public void MarkAsUndone()
    {
        Done = false;
        CompletionDate = null;
    }
}
