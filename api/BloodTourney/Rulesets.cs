using System.Collections.Immutable;
using System.Data;

namespace BloodTourney;

public partial class Core
{
    public enum Rulesets
    {
        BloodBowl2020,
        SardineBowl2025,
        Custom
    }

    public enum TeamCodeNames
    {
        Amazons, BlackOrcs, Chaos, ChaosDwarves, ChaosRenegades, DarkElves, Dwarves, ElvenUnion, Gnomes, Goblins,
        Halflings, HighElves, Humans, ImperialNobility, Khorne, Lizardmen, Necromantic, Norse,
        Nurgle, Ogres, OldWorldAlliance, Orcs, Skaven, Slann, Snotlings,
        TombKings, Undead, Underworld, Vampires, WoodElves
    }
    
    public struct TierParameters
    {
        public int TierNumber { get; set; }
        public List<TeamCodeNames> TeamCodes { get; set; }
        /// <summary>
        /// Maximum TV before skills and inducements are applied.
        /// </summary>
        public int Gps { get; set; }
        public int MaxPrimarySkills { get; set; }
        public int MaxSecondarySkills { get; set; }
        public int SkillStackingPlayerLimit { get; set; }
        public int MaxStarPlayers { get; set; }
    }

    public static readonly ImmutableArray<TierParameters> BloodBowl2020TierParameters = ImmutableArray.Create(
        new TierParameters
        {
            TierNumber = 1,
            TeamCodes = new List<TeamCodeNames>{ TeamCodeNames.Amazons, TeamCodeNames.DarkElves, TeamCodeNames.Dwarves, TeamCodeNames.Lizardmen, TeamCodeNames.Undead, TeamCodeNames.Underworld },
            Gps = 1_150_000,
            MaxPrimarySkills = 6,
            MaxSecondarySkills = 0,
            SkillStackingPlayerLimit = 0,
            MaxStarPlayers = 0
        },
        new TierParameters
        {
            TierNumber = 2,
            TeamCodes = new List<TeamCodeNames>{ TeamCodeNames.ChaosDwarves, TeamCodeNames.Necromantic, TeamCodeNames.Norse, TeamCodeNames.Orcs, TeamCodeNames.Skaven, TeamCodeNames.Vampires, TeamCodeNames.WoodElves },
            Gps = 1_160_000,
            MaxPrimarySkills = 7,
            MaxSecondarySkills = 1,
            SkillStackingPlayerLimit = 0,
            MaxStarPlayers = 0
        },
        new TierParameters
        {
            TierNumber = 3,
            Gps = 1_170_000,
            TeamCodes = new List<TeamCodeNames>{ TeamCodeNames.ElvenUnion, TeamCodeNames.Khorne, TeamCodeNames.HighElves, TeamCodeNames.Humans, TeamCodeNames.TombKings },
            MaxPrimarySkills = 8,
            MaxSecondarySkills = 1,
            SkillStackingPlayerLimit = 0,
            MaxStarPlayers = 1
        },
        new TierParameters
        {
            TierNumber = 4,
            TeamCodes = new List<TeamCodeNames>{ TeamCodeNames.ChaosRenegades, TeamCodeNames.Gnomes, TeamCodeNames.ImperialNobility, TeamCodeNames.OldWorldAlliance, TeamCodeNames.Slann },
            Gps = 1_180_000,
            MaxPrimarySkills = 8,
            MaxSecondarySkills = 2,
            SkillStackingPlayerLimit = 2,
            MaxStarPlayers = 1
        },
        new TierParameters
        {
            TierNumber = 5,
            TeamCodes = new List<TeamCodeNames>{ TeamCodeNames.BlackOrcs, TeamCodeNames.Chaos, TeamCodeNames.Nurgle },
            Gps = 1_200_000,
            MaxPrimarySkills = 9,
            MaxSecondarySkills = 2,
            SkillStackingPlayerLimit = 2,
            MaxStarPlayers = 1
        },
        new TierParameters
        {
            TierNumber = 6,
            TeamCodes = new List<TeamCodeNames>{ TeamCodeNames.Goblins, TeamCodeNames.Halflings, TeamCodeNames.Ogres, TeamCodeNames.Snotlings },
            Gps = 1_200_000,
            MaxPrimarySkills = 10,
            MaxSecondarySkills = 3,
            SkillStackingPlayerLimit = 4,
            MaxStarPlayers = 1
        }
    );
    
    public static readonly ImmutableArray<TierParameters> EuroBowl2025TierParameters = ImmutableArray.Create(
        new TierParameters
        {
            TierNumber = 1,
            Gps = 1_150_000,
            MaxPrimarySkills = 6,
            MaxSecondarySkills = 0,
            SkillStackingPlayerLimit = 0,
            MaxStarPlayers = 0
        },
        new TierParameters
        {
            TierNumber = 2,
            Gps = 1_160_000,
            MaxPrimarySkills = 7,
            MaxSecondarySkills = 1,
            SkillStackingPlayerLimit = 0,
            MaxStarPlayers = 0
        },
        new TierParameters
        {
            TierNumber = 3,
            Gps = 1_170_000,
            MaxPrimarySkills = 8,
            MaxSecondarySkills = 1,
            SkillStackingPlayerLimit = 0,
            MaxStarPlayers = 1
        },
        new TierParameters
        {
            TierNumber = 4,
            Gps = 1_180_000,
            MaxPrimarySkills = 8,
            MaxSecondarySkills = 2,
            SkillStackingPlayerLimit = 2,
            MaxStarPlayers = 1
        },
        new TierParameters
        {
            TierNumber = 5,
            Gps = 1_200_000,
            MaxPrimarySkills = 9,
            MaxSecondarySkills = 2,
            SkillStackingPlayerLimit = 2,
            MaxStarPlayers = 1
        },
        new TierParameters
        {
            TierNumber = 6,
            Gps = 1_200_000,
            MaxPrimarySkills = 10,
            MaxSecondarySkills = 3,
            SkillStackingPlayerLimit = 4,
            MaxStarPlayers = 1
        }
    );
    
    // public static GetTiersForRuleset  (Rulesets ruleset)
}