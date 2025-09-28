using LMWebAPI.Data;
namespace LMWebAPI.Models;

public class Player : BaseEntity
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public Guid PositionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Number { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool StarPlayer { get; set; }
    public int MA { get; set; }
    public int ST { get; set; }
    public int AG { get; set; }
    public int PA { get; set; }
    public int AV { get; set; }
    public int SPP { get; set; }
    public int Completions { get; set; }
    public int Touchdowns { get; set; }
    public int Interceptions { get; set; }
    public int Casualties { get; set; }
    public int MVP { get; set; }
    public int Passes { get; set; }
    public int Rushes { get; set; }
    public int Blocks { get; set; }
    public int Fouls { get; set; }
    public int MatchesPlayed { get; set; }

    // Navigation
    public Team Team { get; set; } = null!;
    public Positional Position { get; set; } = null!;
    public ICollection<PlayerSkill> PlayerSkills { get; set; } = new List<PlayerSkill>();
    public ICollection<PlayerInjury> PlayerInjuries { get; set; } = new List<PlayerInjury>();
}