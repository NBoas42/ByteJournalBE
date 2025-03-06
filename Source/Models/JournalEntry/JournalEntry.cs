using System;

public class JournalEntry
{
    public Guid Id { get; set; }
    public Guid JournalId { get; set; }
    public string? Sprint { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
