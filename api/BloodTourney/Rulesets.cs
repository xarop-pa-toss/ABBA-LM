﻿using System.Collections.Immutable;
using System.Data;
using System.Diagnostics;

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
    
    public readonly struct TierParameters
    {
        public required uint TierNumber { get; init; }
        public required List<TeamCodeNames> TeamCodes { get; init; }
        /// <summary>
        /// Maximum TV before skills and inducements are applied.
        /// </summary>
        public required uint Gps { get; init; }
        public required uint MaxPrimarySkills { get; init; }
        public required uint MaxSecondarySkills { get; init; }
        public required uint SkillStackingPlayerLimit { get; init; }
        public required uint MaxStarPlayers { get; init; }
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
    
    public static readonly ImmutableArray<TierParameters> SardineBowl2025TierParameters = ImmutableArray.Create(
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

    public void CreateCustomRuleset(string name, IEnumerable<TierParameters> tierParameters)
    {
        tierParametersList = tierParameters.ToImmutableArray();
    }

    public ImmutableArray<TierParameters> GetTiersForRuleset(Rulesets ruleset)
    {
        switch (ruleset)
        {
            case Rulesets.SardineBowl2025:
                return SardineBowl2025TierParameters;
            case Rulesets.BloodBowl2020:
                return BloodBowl2020TierParameters;
            case Rulesets.Custom:
                return CustomTierParameters;
            default:
                return ImmutableArray<TierParameters>.Empty;
        }
        
    }
    
    // public static GetTiersForRuleset  (Rulesets ruleset)
}