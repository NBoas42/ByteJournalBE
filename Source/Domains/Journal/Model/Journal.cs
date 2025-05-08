using System;

public class Journal
{
    public Guid Id { get; set; }
    public string? Color { get; set; }
    public Guid AccountId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? CurrentSprint { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
