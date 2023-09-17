namespace ToDoList.Common.Dtos.ToDoDtos;

public class ReadToDoDto
{
    public Guid Id { get; private set; }
    public string Description { get; private set; }
    public bool Done { get; private set; }
    public DateTime? ExpectedCompletionDate { get; private set; }
    public DateTime? CompletionDate { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime LastUpdatedDate { get; private set; }

    public ReadToDoDto() { }

    public void Fill(Guid id, string description, bool done, DateTime? expectedCompletionDate, DateTime? completionDate, DateTime createdDate, DateTime lastUpdatedDate)
    {
        Id = id;
        Description = description;
        Done = done;
        ExpectedCompletionDate = expectedCompletionDate;
        CompletionDate = completionDate;
        CreatedDate = createdDate;
        LastUpdatedDate = lastUpdatedDate;
    }
}