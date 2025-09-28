using LMWebAPI.Data;
namespace LMWebAPI.Models;

public abstract class Round : BaseEntity
{

    public int RoundNumber { get; set; }
    public bool IsComplete { get; set; }

    // Navigation
    public ICollection<Match> Matches { get; set; } = new List<Match>();
}