using System.Collections.Immutable;
using BloodTourney.Models;
namespace LMWebAPI.Models.DTOs;

public class RulesetDTO
{
    public required IEnumerable<Tier> Tiers { get; init; }
    public required VictoryPoints MatchVictoryPoints { get; init; }
    public required IEnumerable<string> TieBreakers { get; init; }
    public required TimeKeeping Timekeeping { get; init; }
    public required SkillStacking Skillstacking { get; init; }
    public required ImmutableArray<Inducement> Inducements { get; init; }
    public required IEnumerable<string>? BannedStarPlayers { get; init; }
    public required IEnumerable<string>? Guidelines { get; init; }
    public required OtherRules? AdditionalRules { get; init; }
}