using System.Collections.Immutable;
using System.Data;
using System.Diagnostics;
using static BloodTourney.Helpers;

namespace BloodTourney;

public class Tiers
{
    /// <summary>
    /// Represents the restrictions and parameters for a tournament Tier
    /// </summary>
    public readonly struct TierParameters
    {
        public required uint TierNumber { get; init; }
        public required List<TeamCodeNames> AllowedTeams { get; init; }

        /// <summary>
        /// Maximum Team Value (gold pieces) before skills and inducements are applied.
        /// </summary>
        public required uint MaxBaseTeamValue { get; init; }
        
        public required uint MaxPrimarySkills { get; init; }
        public required uint MaxSecondarySkills { get; init; }
        public required uint SkillStackingPlayerLimit { get; init; }
        public required uint MaxStarPlayers { get; init; }
        
        public bool IsTeamAllowed(TeamCodeNames team) => AllowedTeams.Contains(team);
    }
    
    public readonly static ImmutableArray<TierParameters> SardineBowl2025 = ImmutableArray.Create(
        new TierParameters
        {
            TierNumber = 1,
            AllowedTeams = new List<TeamCodeNames>
            {
                TeamCodeNames.Amazons,
                TeamCodeNames.DarkElves,
                TeamCodeNames.Dwarves,
                TeamCodeNames.Lizardmen,
                TeamCodeNames.Undead,
                TeamCodeNames.Underworld
            },
            MaxBaseTeamValue = 1_150_000,
            MaxPrimarySkills = 6,
            MaxSecondarySkills = 0,
            SkillStackingPlayerLimit = 0,
            MaxStarPlayers = 0
        },
        new TierParameters
        {
            TierNumber = 2,
            AllowedTeams = new List<TeamCodeNames>
            {
                TeamCodeNames.ChaosDwarves,
                TeamCodeNames.Necromantic,
                TeamCodeNames.Norse,
                TeamCodeNames.Orcs,
                TeamCodeNames.Skaven,
                TeamCodeNames.Vampires,
                TeamCodeNames.WoodElves
            },
            MaxBaseTeamValue = 1_160_000,
            MaxPrimarySkills = 7,
            MaxSecondarySkills = 1,
            SkillStackingPlayerLimit = 0,
            MaxStarPlayers = 0
        },
        new TierParameters
        {
            TierNumber = 3,
            MaxBaseTeamValue = 1_170_000,
            AllowedTeams = new List<TeamCodeNames>
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
            AllowedTeams = new List<TeamCodeNames>
            {
                TeamCodeNames.ChaosRenegades,
                TeamCodeNames.Gnomes,
                TeamCodeNames.ImperialNobility,
                TeamCodeNames.OldWorldAlliance,
                TeamCodeNames.Slann
            },
            MaxBaseTeamValue = 1_180_000,
            MaxPrimarySkills = 8,
            MaxSecondarySkills = 2,
            SkillStackingPlayerLimit = 2,
            MaxStarPlayers = 1
        },
        new TierParameters
        {
            TierNumber = 5,
            AllowedTeams = new List<TeamCodeNames>
            {
                TeamCodeNames.BlackOrcs,
                TeamCodeNames.Chaos,
                TeamCodeNames.Nurgle
            },
            MaxBaseTeamValue = 1_200_000,
            MaxPrimarySkills = 9,
            MaxSecondarySkills = 2,
            SkillStackingPlayerLimit = 2,
            MaxStarPlayers = 1
        },
        new TierParameters
        {
            TierNumber = 6,
            AllowedTeams = new List<TeamCodeNames>
            {
                TeamCodeNames.Goblins,
                TeamCodeNames.Halflings,
                TeamCodeNames.Ogres,
                TeamCodeNames.Snotlings
            },
            MaxBaseTeamValue = 1_200_000,
            MaxPrimarySkills = 10,
            MaxSecondarySkills = 3,
            SkillStackingPlayerLimit = 4,
            MaxStarPlayers = 1
        }
    );

    public readonly static ImmutableArray<TierParameters> EuroBowl2025 = ImmutableArray.Create(
        new TierParameters
        {
            TierNumber = 1,
            MaxBaseTeamValue = 1_150_000,
            AllowedTeams = new List<TeamCodeNames>
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
            MaxBaseTeamValue = 1_160_000,
            AllowedTeams = new List<TeamCodeNames>
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
            MaxBaseTeamValue = 1_170_000,
            AllowedTeams = new List<TeamCodeNames>
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
            MaxBaseTeamValue = 1_180_000,
            AllowedTeams = new List<TeamCodeNames>
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
            MaxBaseTeamValue = 1_200_000,
            AllowedTeams = new List<TeamCodeNames>
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
            MaxBaseTeamValue = 1_200_000,
            AllowedTeams = new List<TeamCodeNames>
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
    /// Custom set of Tiers validation
    /// </summary>
    /// <param name="name"></param>
    /// <param name="tierParameters"></param>
    /// <returns>List Errors will be empty if tournament is valid.</returns>
    public ValidationResult ValidateCustomTiers(string name, IEnumerable<TierParameters> tierParameters)
    {
        var errors = new List<string>();
        
        if (!tierParameters.Any())
        {
            errors.Add("List of tiers is empty.");
        }
        
        List<uint> orderedTierNumbers = tierParameters.Select(t => t.TierNumber).Order().ToList();
        
        // Check for duplicate tier numbers/levels.
        if (orderedTierNumbers.Distinct().Count() < tierParameters.Count())
        {
            errors.Add("Duplicate tier numbers/levels found.");
        }
        
        // Check if Tier Numbers start at 1 and os numbered sequentially up to the number of tiers submitted
        if (orderedTierNumbers.First() == 1 && orderedTierNumbers.Last() == tierParameters.Count())
        {
            errors.Add("Tier numbers are not sequential.");
        }
        
        // TODO: Check if there are duplicate teams
        List<TeamCodeNames> orderedTeamValues = tierParameters.Select(t => t.AllowedTeams).Order().ToList();
        if (tierParameters.SelectMany(t => t.AllowedTeams).Distinct().Count() < tierParameters.Count())
        {
            
        }

        // TODO: Check if all teams in the AllowedTeams property are present in the tier list
        return valResult;
    }
}

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