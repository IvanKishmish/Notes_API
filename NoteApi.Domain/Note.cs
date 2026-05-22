namespace NoteApi.Domain;

public class Note
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string Title { get; set; } = string.Empty;
    public string Details { get;set; } = string.Empty;
    public DateTimeOffset CreationDate { get; set; } =  DateTimeOffset.UtcNow;
    public DateTimeOffset? EditDate { get; set; } =  DateTimeOffset.UtcNow;
}