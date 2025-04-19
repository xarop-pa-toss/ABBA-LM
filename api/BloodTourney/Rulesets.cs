using System.Collections.Immutable;
using MongoDB.Driver.Authentication.Gssapi.Sspi;

namespace BloodTourney;

public partial class Core
{
    public enum RulesetPresets {
        EuroBowl2025,
        SardineBowl2025
    }

    public class Ruleset {
        public required IEnumerable<TierParameters> Tiers { get; init; }
        public required VictoryPoints VictoryPoints { get; init; }
        public required IEnumerable<string> TieBreakers { get; init; }
        public required TimeKeeping Timekeeping { get; init; }
        public required SkillStacking Skillstacking { get; init; }
        public required IEnumerable<Inducement> Inducements { get; init; }
        public required IEnumerable<string> BannedStarPlayers { get; init; }
        public required IEnumerable<string> Guidelines { get; init; }
        public required OtherRules OtherRules { get; init; }
        
        // Default constructor
        private Ruleset() { }
        
        public class Builder {
        
            public IEnumerable<TierParameters> tiers;
            public VictoryPoints victoryPoints;
            public IEnumerable<string> tieBreakers;
            public TimeKeeping timekeeping;
            public SkillStacking skillstacking;
            public IEnumerable<Inducement> inducements;
            public IEnumerable<string> bannedStarPlayers;
            public IEnumerable<string> guidelines;
            public OtherRules otherRules;
            public Builder WithTiers(IEnumerable<TierParameters> tiers)
            {
                this.tiers = tiers;
                return this;
            }

            public Builder WithVictoryPoints(VictoryPoints victoryPoints)
            {
                this.victoryPoints = victoryPoints;
                return this;
            }

            public Builder WithTieBreakers(IEnumerable<string> tieBreakers)
            {
                this.tieBreakers = tieBreakers;
                return this;
            }

            public Builder WithTimeKeeping(TimeKeeping timekeeping)
            {
                this.timekeeping = timekeeping;
                return this;
            }

            public Builder WithSkillStacking(SkillStacking skillStacking)
            {
                this.skillstacking = skillStacking;
                return this;
            }
            
            public Ruleset Build()
            {
                // Validation
                if (tiers == null)
                {
                    throw new InvalidOperationException("tiers must not be null");
                }
                if ()
            }
        }
    }

    public enum SkillStacks
    {
        NotAllowed,
        OnlyPrimaries,
        AllSkills
    }
    public struct SkillStacking()
    {
        public required SkillStacks StackingType { get; set; } = SkillStacks.OnlyPrimaries;
        /// <summary>
        /// Skill combinations that will consume extra slots from the alloted skill allowance (based on Tier)
        /// </summary>
        public Dictionary<ImmutableHashSet<string>, uint>? ExtraSkillstackCosts { get; init; }
    }
    public struct Inducement
    {
        public required uint Min { get; set; }
        public required uint Max { get; set; }
        public required string Name { get; set; }
        public required uint Cost { get; set; }

        /// <summary>
        /// Overrides default cost of this inducement for a specific team. Eg. Master Chef for Halflings
        /// </summary>
        public Dictionary<string, uint>? CostOverrideForTeam { get; set; }

        /// <summary>
        /// Overrides default cost of this inducement for teams with a specific trait. Eg. Bribes for Bribery & Corruption teams
        /// </summary>
        public Dictionary<string, uint>? CostOverrideForTeamTrait { get; set; }

        /// <summary>
        /// Team tiers allowed to purchase an inducement. All Tiers allowed if list is empty.
        /// </summary>
        public IEnumerable<int>? TiersAllowed { get; set; }
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

    public Ruleset GetPresetRuleset(RulesetPresets ruleset)
    {
        switch (ruleset)
        {
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
                        ExtraSkillstackCosts = new Dictionary<ImmutableHashSet<string>, uint>()
                        {
                            [ImmutableHashSet.Create("Tackle", "Mighty Blow")]= 2,
                            [ImmutableHashSet.Create("Claw", "Mighty Blow")]= 2,
                            [ImmutableHashSet.Create("Sneaky Git", "Dirty Player")]= 3
                        },
                    },
                    Inducements = ImmutableArray.Create<Inducement>(
                        new Inducement()
                        {
                            Min = 0,
                            Max = 1,
                            Name = "Halfling Master Chef",
                            Cost = 300_000,
                            CostOverrideForTeam = new Dictionary<string, uint>
                            {
                                ["halflings"] = 100_000
                            }
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
                            CostOverrideForTeamTrait = new Dictionary<string, uint>
                            {
                                ["Bribery & Corruption"] = 50_000
                            }
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
        }

        throw new Exception("The ruleset you requested is not defined.");
    }
}