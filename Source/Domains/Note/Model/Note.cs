using System;

public class Note
{
    public Guid Id { get; set; }
    public Guid JournalEntryId { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
