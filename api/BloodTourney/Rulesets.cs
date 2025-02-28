using System.Collections.Immutable;
using System.Data;
using System.Diagnostics;

namespace BloodTourney;

public partial class Core
{
    public enum Rulesets
    {
        EuroBowl2025,
        SardineBowl2025,
        Custom
    }
    public struct Ruleset
    {
        public ImmutableArray<TeamRule> TeamRules { get; set; }
        public ImmutableArray<PlayerRule> PlayerRules { get; set; }
        public ImmutableArray<Inducement> Inducements { get; set; }
        public ImmutableArray<string> BannedStarPlayers { get; set; }
        public ImmutableArray<string> AdditionalRules { get; set; }
        public ImmutableArray<TierParameters> Tiers {get; set;}
    }
    
}