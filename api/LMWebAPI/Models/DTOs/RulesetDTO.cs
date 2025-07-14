using System.Collections.Immutable;
using BT = BloodTourney;
namespace LMWebAPI.Models.DTOs;

public class RulesetDTO
{
    public required IEnumerable<BT.Tiers.TierParameters> Tiers { get; init; }
    public required BT.Models.VictoryPoints MatchVictoryPoints { get; init; }
    public required IEnumerable<string> TieBreakers { get; init; }
    public required BT.Models.TimeKeeping Timekeeping { get; init; }
    public required BT.Models.SkillStacking Skillstacking { get; init; }
    public required ImmutableArray<BT.Models.Inducement> Inducements { get; init; }
    public required IEnumerable<string>? BannedStarPlayers { get; init; }
    public required IEnumerable<string>? Guidelines { get; init; }
    public required BT.Models.OtherRules? AdditionalRules { get; init; }
}