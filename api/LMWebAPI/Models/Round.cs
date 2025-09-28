namespace LMWebAPI.Models;

public abstract class Round
{
    public Guid Id { get; set; }
    public uint RoundNumber { get; set; }
    public bool IsComplete { get; set; }
    
    // Navigation
    public ICollection<Match> Matches { get; set; } = new List<Match>();
    
    // Meta
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}