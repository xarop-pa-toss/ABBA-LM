using System.Collections.Immutable;
using MongoDB.Driver.Authentication.Gssapi.Sspi;

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
        public IEnumerable<TierParameters> Tiers { get; set; }
        public VictoryPoints VictoryPoints { get; set; }
        public IEnumerable<string> TieBreakers { get; set; }
        public TimeKeeping Timekeeping { get; set; }
        public SkillStacking Skillstacking { get; set; }
        public IEnumerable<Inducement> Inducements { get; set; }
        public IEnumerable<string> BannedStarPlayers { get; set; }
        public IEnumerable<string> Guidelines { get; set; }
        public OtherRules OtherRules { get; set; }
    }

    public enum SkillStacks
    {
        None,
        OnlyPrimaries,
        AllSkills
    }
    public struct SkillStacking
    {
        public required SkillStacks StackingType;
        public Dictionary<HashSet<string>, uint> ExtraSkillstackCosts { get; init; }
        public required bool ExtraCostAppliesToDefaultSkills { get; init; }
    }
    public struct Inducement
    {
        public required uint Min { get; set; }
        public required uint Max { get; set; }
        public required string Name { get; set; }
        public required uint Cost { get; set; }

        /// <summary>
        /// Overrides Cost. Eg. Master Chef for Halflings
        /// </summary>
        public Dictionary<string, uint> CostForTeam { get; set; }

        /// <summary>
        /// Overrides Cost. Eg. Bribes for Bribery & Corruption
        /// </summary>
        public Dictionary<string, uint> CostForTeamTrait { get; set; }

        /// <summary>
        /// Team tiers allowed to purchase an inducement. All Tiers allowed if list is empty.
        /// </summary>
        public IEnumerable<int> TiersAllowed { get; set; }
    }
    public struct TimeKeeping
    {
        public required uint MatchTimelimitInMinutes { get; set; }
        public required bool ChessClockAllowed { get; set; }
    }
    /// <summary>
    /// Default Win = 3, Draw = 1, Loss = 0
    /// </summary>
    public struct VictoryPoints()
    {
        public uint Win { get; set; } = 3;
        public uint Draw { get; set; } = 1;
        public uint Loss { get; set; } = 0;
    }
    public struct OtherRules()
    {
        /// <summary>
        /// Uses GP/TV for Rerolls, Assistant Coaches, Cheerleaders, Apothecary and Inducements
        /// </summary>
        public required bool UseGPForNonPlayerExtras = true;
        /// <summary>
        /// Players added to a roster during a match due to special rules are removed at the end of the match. Eg. Nurgle rotters
        /// </summary>
        public required bool RemovePlayersAddedDuringMatchAtGameEnd = true;
        /// <summary>
        /// If false, teams must have distinct Star Players.
        /// </summary>
        public required bool AllowSameStarPlayerOnBothTeams = true;
        /// <summary>
        /// Text only. Will not have mechanical effect.
        /// </summary>
        public IEnumerable<string>? AdditionalRules { get; set; }

    }
    
    public Ruleset GetBaseRuleset(RulesetPresets ruleset)
    {
        switch (ruleset) {
        case RulesetPresets.SardineBowl2025:
            return new Ruleset()
            {
                Tiers = SardineBowl2025TierParameters,
                VictoryPoints = new VictoryPoints(),
                TieBreakers = ["Net TD's + Net CAS", "Most TD's", "Head to Head (Top 3)", "Random"],
                Timekeeping = new TimeKeeping()
                {
                    MatchTimelimitInMinutes = 135,
                    ChessClockAllowed = true
                },
                Skillstacking = new SkillStacking()
                {
                    StackingType = SkillStacks.OnlyPrimaries,
                    ExtraSkillstackCosts = new Dictionary<HashSet<string>, uint>()
                    {
                        [new HashSet<string>
                        {
                            "Tackle",
                            "Mighty Blow"
                        }] = 2,
                        [new HashSet<string>
                        {
                            "Claw",
                            "Mighty Blow"
                        }] = 2,
                        [new HashSet<string>
                        {
                            "Sneaky Git",
                            "Dirty Player"
                        }] = 3
                    },
                    ExtraCostAppliesToDefaultSkills = false
                },
                Inducements = ImmutableArray.Create<Inducement>(
                    new Inducement()
                    {
                        Min = 0,
                        Max = 1,
                        Name = "Halfling Master Chef",
                        Cost = 300_000,
                        CostForTeam = new Dictionary<string, uint> { ["halflings"] = 100_000 }
                    },
                    new Inducement()
                    {
                        Min = 0,
                        Max = 6,
                        Name = "Assistant Coach",
                        Cost = 10_000
                    },
                    new Inducement()
                    {
                        Min = 0,
                        Max = 12,
                        Name = "Cheerleaders",
                        Cost = 10_000
                    },
                    new Inducement()
                    {
                        Min = 0,
                        Max = 2,
                        Name = "Bloodweiser Kegs",
                        Cost = 50_000
                    },
                    new Inducement()
                    {
                        Min = 0,
                        Max = 3,
                        Name = "Bribes",
                        Cost = 100_000,
                        CostForTeamTrait = new Dictionary<string, uint> { ["Bribery & Corruption"] = 50_000 }
                    },
                    new Inducement()
                    {
                        Min = 0,
                        Max = 2,
                        Name = "Wandering Apothecaries",
                        Cost = 100_000
                    },
                    new Inducement()
                    {
                        Min = 0,
                        Max = 1,
                        Name = "Morgue Assistant",
                        Cost = 100_000
                    },
                    new Inducement()
                    {
                        Min = 0,
                        Max = 1,
                        Name = "Plague Doctor",
                        Cost = 100_000
                    },
                    new Inducement()
                    {
                        Min = 0,
                        Max = 1,
                        Name = "Riotous Rookies",
                        Cost = 100_000
                    },
                    new Inducement()
                    {
                        Min = 0,
                        Max = 1,
                        Name = "Star Players",
                        Cost = 0, // Variable cost based on star player
                        TiersAllowed = new[]
                        {
                            3, 4, 5, 6
                        }
                    }
                ),
                BannedStarPlayers = ImmutableArray.Create<string>(
                    "Morg'N'Thorg",
                    "Griff Oberwald",
                    "Deeproot Strongbranch",
                    "Hakflem Skuttlespike",
                    "Kreek Rustgouger",
                    "Bomber Dribblesnot",
                    "Estelle la Veneaux",
                    "Cindy Piewhistle",
                    "Wilhem Chaney",
                    "Ivan \"The Animal\" Deathshroud",
                    "Skitter Stab Stab",
                    "Varag Ghoulchewer",
                    "Dribl and Drill",
                    "H'Thark the Unstoppable",
                    "Lord Borak"
                ),
                Guidelines = ImmutableArray<string>.Empty,
                OtherRules = new OtherRules()
                {
                    UseGPForNonPlayerExtras = true,
                    RemovePlayersAddedDuringMatchAtGameEnd = true,
                    AllowSameStarPlayerOnBothTeams = true,
                    AdditionalRules = ["Undead, Necromantic and Nurgle teams are allowed to apply the Masters of Undeath and Plague Ridden special rules."]
                }
            };
        default:
            return new Ruleset();
        }
    }
}