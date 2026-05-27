namespace NoteApi.Domain;

public class Note
{
    public Guid Id { get; set; } 
    public string Title { get; set; } = string.Empty;
    public string Details { get;set; } = string.Empty;
    public DateTimeOffset CreationDate { get; private init; } =  DateTimeOffset.UtcNow;
    public DateTimeOffset? EditDate { get; set; }

    private Note(){}//ef
    
    private Note(Guid id, string title, string details)
    {
        Id = id;
        Title = title;
        Details = details;
    }
    
    public static Note CreateNote(string title, string details)
    {
        return new Note(Guid.CreateVersion7(), title, details);
    }

    public void UpdateNote(string title, string details)
    {
        Title = title;
        Details = details;
        EditDate = DateTimeOffset.UtcNow;
    }
}