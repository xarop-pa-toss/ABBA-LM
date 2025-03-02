using System.Collections.Immutable;

namespace BloodTourney;

public partial class Core
{
    public enum RulesetPresets
    {
        EuroBowl2025,
        SardineBowl2025,
    }
    
    public struct Ruleset
    {
        public ImmutableArray<string> Timekeeping { get; set; }
        public ImmutableArray<string> Guidelines { get; set; }
        public ImmutableArray<string> Inducements { get; set; }
        public ImmutableArray<string> BannedStarPlayers { get; set; }
        public ImmutableArray<TierParameters> Tiers {get; set;}
        public ImmutableArray<string> OtherRules { get; set; }
        public bool UseGPForNonPlayerExtras { get; set; }
        public bool RemovePlayersAddedDuringMatchAtGameEnd { get; set; }
        public bool AllowSameStarPlayerOnBothTeams { get; set; }
    }

    public struct Inducements
    {
        public uint Min { get; set; }
        public uint Max { get; set; }
        public string Name { get; set; }
        public uint Cost { get; set; }
        public List<int> TiersAllowed  { get; set; }
        
    }

    public Ruleset GetBaseRuleset(RulesetPresets ruleset)
    {
        return new Ruleset()
        {
            Inducements = ImmutableArray<string>.Empty,
            BannedStarPlayers = ImmutableArray<string>.Empty,
            Tiers = ImmutableArray<TierParameters>.Empty,
            UseGPForNonPlayerExtras = false,
            RemovePlayersAddedDuringMatchAtGameEnd = false,
            AllowSameStarPlayerOnBothTeams = false,
            Timekeeping = ImmutableArray<string>.Empty,
            Guidelines = ImmutableArray<string>.Empty,
            OtherRules = ImmutableArray.Create(
                "Undead, Necromantic and Nurgle teams are allowed to apply the Masters of Undeath and Plague Ridden special rules.");
        };
        
    }
}