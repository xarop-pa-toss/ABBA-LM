using LMWebAPI.Data;
namespace LMWebAPI.Models;

public class Team : BaseEntity
{
    public Guid Id { get; set; }
    public Guid CoachId { get; set; }
    public Guid RosterId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Win { get; set; }
    public int Loss { get; set; }
    public int Ties { get; set; }
    public int Delta { get; set; }
    public int TeamValue { get; set; }
    public int Treasury { get; set; }
    public int Rerolls { get; set; }
    public int DedicatedFans { get; set; }
    public int AssistantCoaches { get; set; }
    public int Cheerleaders { get; set; }
    public bool HasApothecary { get; set; }
    public bool IsExperienced { get; set; }
    public bool IsSuspended { get; set; }
    public bool IsDeactivated { get; set; }

    // Navigation
    public Coach Coach { get; set; } = null!;
    public Roster Roster { get; set; } = null!;
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<Match> HomeMatches { get; set; } = new List<Match>();
    public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
}