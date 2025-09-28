using LMWebAPI.Data;
namespace LMWebAPI.Models;

public abstract class Round : BaseEntity
{

    public uint RoundNumber { get; set; }
    public bool IsComplete { get; set; }

    // Navigation
    public ICollection<Match> Matches { get; set; } = new List<Match>();
}