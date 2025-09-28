using System.Collections.Immutable;

namespace BloodTourney.Models;

public class Ruleset
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

#region Types
public enum SkillStacks
{
    NotAllowed,
    OnlyPrimaries,
    AllSkills
}

public struct SkillStacking
{
    public required SkillStacks StackingType { get; set; }
    public Dictionary<ImmutableHashSet<string>, uint>? ExtraSkillstackCosts { get; init; }
    public required bool ExtraCostAppliesToDefaultSkills { get; init; }
}

public struct Inducement
{
    public required uint Min { get; set; }
    public required uint Max { get; set; }
    public required string Name { get; set; }
    public required uint Cost { get; set; }
    public Dictionary<string, uint>? CostOverrideForTeam { get; set; }
    public Dictionary<string, uint>? CostOverrideForTeamsWithTrait { get; set; }
    public IEnumerable<int>? TiersAllowed { get; set; }
}

public struct TimeKeeping
{
    public required uint MatchTimelimitInMinutes { get; set; }
    public required bool ChessClockAllowed { get; set; }
}

public struct VictoryPoints()
{
    public required uint Win { get; set; } = 3;
    public required uint Draw { get; set; } = 1;
    public required uint Loss { get; set; } = 0;
}

public struct OtherRules()
{
    public required bool UseTVForNonPlayerExtras { get; set; } = true;
    public required bool RemovePlayersAddedDuringMatchAtGameEnd { get; set; } = true;
    public required bool AllowSameStarPlayerOnBothTeams { get; set; } = true;
    public IEnumerable<string>? AdditionalRules { get; set; } = null;
}
#endregion
