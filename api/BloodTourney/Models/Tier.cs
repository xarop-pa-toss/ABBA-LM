using BloodTourney.Models.Enums;
namespace BloodTourney.Models;

public class Tier
{
    /// <summary>
    /// Represents the restrictions and parameters for a tournament Tier
    /// </summary>

    public required uint TierLevel { get; init; }
    public required List<TeamCodeNames> Teams { get; init; }

    /// <summary>
    /// Maximum Team Value (gold pieces) before skills and inducements are applied.
    /// </summary>
    public required uint MaxBaseTeamValue { get; init; }
    public required uint MaxPrimarySkills { get; init; }
    public required uint MaxSecondarySkills { get; init; }
    public required uint SkillStackingPlayerLimit { get; init; }
    public required uint MaxStarPlayers { get; init; }
    
    public bool IsTeamAllowed(TeamCodeNames team) => Teams.Contains(team);
}