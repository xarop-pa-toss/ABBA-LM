using System.Collections.Immutable;
using System.Data;
using System.Diagnostics;

namespace BloodTourney;

public class Tiers
{
    public enum TeamCodeNames
    {
        Amazons,
        BlackOrcs,
        Chaos,
        ChaosDwarves,
        ChaosRenegades,
        DarkElves,
        Dwarves,
        ElvenUnion,
        Gnomes,
        Goblins,
        Halflings,
        HighElves,
        Humans,
        ImperialNobility,
        Khorne,
        Lizardmen,
        Necromantic,
        Norse,
        Nurgle,
        Ogres,
        OldWorldAlliance,
        Orcs,
        Skaven,
        Slann,
        Snotlings,
        TombKings,
        Undead,
        Underworld,
        Vampires,
        WoodElves
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

    public static readonly ImmutableArray<TierParameters> SardineBowl2025TierParameters = ImmutableArray.Create(
        new TierParameters
        {
            TierNumber = 1,
            TeamCodes = new List<TeamCodeNames>
            {
                TeamCodeNames.Amazons,
                TeamCodeNames.DarkElves,
                TeamCodeNames.Dwarves,
                TeamCodeNames.Lizardmen,
                TeamCodeNames.Undead,
                TeamCodeNames.Underworld
            },
            Gps = 1_150_000,
            MaxPrimarySkills = 6,
            MaxSecondarySkills = 0,
            SkillStackingPlayerLimit = 0,
            MaxStarPlayers = 0
        },
        new TierParameters
        {
            TierNumber = 2,
            TeamCodes = new List<TeamCodeNames>
            {
                TeamCodeNames.ChaosDwarves,
                TeamCodeNames.Necromantic,
                TeamCodeNames.Norse,
                TeamCodeNames.Orcs,
                TeamCodeNames.Skaven,
                TeamCodeNames.Vampires,
                TeamCodeNames.WoodElves
            },
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
            TeamCodes = new List<TeamCodeNames>
            {
                TeamCodeNames.ElvenUnion,
                TeamCodeNames.Khorne,
                TeamCodeNames.HighElves,
                TeamCodeNames.Humans,
                TeamCodeNames.TombKings
            },
            MaxPrimarySkills = 8,
            MaxSecondarySkills = 1,
            SkillStackingPlayerLimit = 0,
            MaxStarPlayers = 1
        },
        new TierParameters
        {
            TierNumber = 4,
            TeamCodes = new List<TeamCodeNames>
            {
                TeamCodeNames.ChaosRenegades,
                TeamCodeNames.Gnomes,
                TeamCodeNames.ImperialNobility,
                TeamCodeNames.OldWorldAlliance,
                TeamCodeNames.Slann
            },
            Gps = 1_180_000,
            MaxPrimarySkills = 8,
            MaxSecondarySkills = 2,
            SkillStackingPlayerLimit = 2,
            MaxStarPlayers = 1
        },
        new TierParameters
        {
            TierNumber = 5,
            TeamCodes = new List<TeamCodeNames>
            {
                TeamCodeNames.BlackOrcs,
                TeamCodeNames.Chaos,
                TeamCodeNames.Nurgle
            },
            Gps = 1_200_000,
            MaxPrimarySkills = 9,
            MaxSecondarySkills = 2,
            SkillStackingPlayerLimit = 2,
            MaxStarPlayers = 1
        },
        new TierParameters
        {
            TierNumber = 6,
            TeamCodes = new List<TeamCodeNames>
            {
                TeamCodeNames.Goblins,
                TeamCodeNames.Halflings,
                TeamCodeNames.Ogres,
                TeamCodeNames.Snotlings
            },
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
            TeamCodes = new List<TeamCodeNames>
            {
                TeamCodeNames.Amazons,
                TeamCodeNames.DarkElves,
                TeamCodeNames.Dwarves,
                TeamCodeNames.Lizardmen,
                TeamCodeNames.Undead,
                TeamCodeNames.Underworld
            },
            MaxPrimarySkills = 6,
            MaxSecondarySkills = 0,
            SkillStackingPlayerLimit = 0,
            MaxStarPlayers = 0
        },
        new TierParameters
        {
            TierNumber = 2,
            Gps = 1_160_000,
            TeamCodes = new List<TeamCodeNames>
            {
                TeamCodeNames.ChaosDwarves,
                TeamCodeNames.Necromantic,
                TeamCodeNames.Norse,
                TeamCodeNames.Orcs,
                TeamCodeNames.Skaven,
                TeamCodeNames.Vampires,
                TeamCodeNames.WoodElves
            },
            MaxPrimarySkills = 7,
            MaxSecondarySkills = 1,
            SkillStackingPlayerLimit = 0,
            MaxStarPlayers = 0
        },
        new TierParameters
        {
            TierNumber = 3,
            Gps = 1_170_000,
            TeamCodes = new List<TeamCodeNames>
            {
                TeamCodeNames.ElvenUnion,
                TeamCodeNames.Khorne,
                TeamCodeNames.HighElves,
                TeamCodeNames.Humans,
                TeamCodeNames.TombKings
            },
            MaxPrimarySkills = 8,
            MaxSecondarySkills = 1,
            SkillStackingPlayerLimit = 0,
            MaxStarPlayers = 1
        },
        new TierParameters
        {
            TierNumber = 4,
            Gps = 1_180_000,
            TeamCodes = new List<TeamCodeNames>
            {
                TeamCodeNames.ChaosRenegades,
                TeamCodeNames.Gnomes,
                TeamCodeNames.ImperialNobility,
                TeamCodeNames.OldWorldAlliance,
                TeamCodeNames.Slann
            },
            MaxPrimarySkills = 8,
            MaxSecondarySkills = 2,
            SkillStackingPlayerLimit = 2,
            MaxStarPlayers = 1
        },
        new TierParameters
        {
            TierNumber = 5,
            Gps = 1_200_000,
            TeamCodes = new List<TeamCodeNames>
            {
                TeamCodeNames.BlackOrcs,
                TeamCodeNames.Chaos,
                TeamCodeNames.Nurgle
            },
            MaxPrimarySkills = 9,
            MaxSecondarySkills = 2,
            SkillStackingPlayerLimit = 2,
            MaxStarPlayers = 1
        },
        new TierParameters
        {
            TierNumber = 6,
            Gps = 1_200_000,
            TeamCodes = new List<TeamCodeNames>
            {
                TeamCodeNames.Goblins,
                TeamCodeNames.Halflings,
                TeamCodeNames.Ogres,
                TeamCodeNames.Snotlings
            },
            MaxPrimarySkills = 10,
            MaxSecondarySkills = 3,
            SkillStackingPlayerLimit = 4,
            MaxStarPlayers = 1
        }
    );

    /// <summary>
    /// Custom ruleset validation,
    /// </summary>
    /// <param name="name"></param>
    /// <param name="tierParameters"></param>
    /// <returns>List Errors will be empty if tournament is valid.</returns>
    public ValidationResult ValidateCustomRuleset(string name, IEnumerable<TierParameters> tierParameters)
    {
        var valResult = new ValidationResult();

        if (!tierParameters.Any())
        {
            valResult.Errors.Add("Given list of Tier Parameters has no elements");
        }
        var tierParametersList = tierParameters.ToImmutableArray();

        HashSet<int> givenTiers = new HashSet<int>();
        foreach (int tierNumber in tierParameters.Select(tp => tp.TierNumber))
        {
            givenTiers.Add(tierNumber);
        }

        if (givenTiers.Count < tierParametersList.Length)
        {
            valResult.Errors.Add("Different tiers must not share a level/number.");
        }

        valResult.IsValid = valResult.Errors.Count() > 0 ? false : true;

        return valResult;
    }

    public struct ValidationResult()
    {
        public bool IsValid = false;
        public List<string> Errors = new List<string>();
    }

    public ImmutableArray<TierParameters> GetTiersForRuleset(RulesetPresets ruleset)
    {
        switch (ruleset)
        {
            case RulesetPresets.SardineBowl2025:
                return SardineBowl2025TierParameters;
            case RulesetPresets.EuroBowl2025:
                return EuroBowl2025TierParameters;
            default:
                return ImmutableArray<TierParameters>.Empty;
        }
    }
}