using System.ComponentModel.DataAnnotations;

namespace ToDoList.Common.Dtos.ToDoDtos;

public class UpdateToDoDto
{
    [StringLength(100, MinimumLength = 3)]
    public string Description { get; private set; }
    public bool Done { get; private set; }
    public DateTime? ExpectedCompletionDate { get; private set; }
    public DateTime? CompletionDate { get; private set; }

    public UpdateToDoDto(string description, bool done, DateTime? expectedCompletionDate, DateTime? completionDate)
    {
        Description = description;
        Done = done;
        ExpectedCompletionDate = expectedCompletionDate;
        CompletionDate = completionDate;
    }
}
