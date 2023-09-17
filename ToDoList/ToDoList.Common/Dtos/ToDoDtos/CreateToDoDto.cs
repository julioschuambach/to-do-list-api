using System.ComponentModel.DataAnnotations;

namespace ToDoList.Common.Dtos.ToDoDtos;

public class CreateToDoDto
{
    [StringLength(100, MinimumLength = 3)]
    public string Description { get; private set; }
    public DateTime? ExpectedCompletionDate { get; private set; }

    public CreateToDoDto(string description, DateTime? expectedCompletionDate)
    {
        Description = description;
        ExpectedCompletionDate = expectedCompletionDate;
    }
}
