using System.Collections.Immutable;
using BloodTourney.Models;
using BloodTourney.Models.Enums;
using static BloodTourney.Helpers;
// ReSharper disable UseCollectionExpression

namespace BloodTourney;

public class TierCreator
{
    public readonly static ImmutableArray<Tier> SardineBowl2025 = ImmutableArray.Create(
        new Tier
        {
            TierLevel = 1,
            Teams = new List<TeamCodeNames>
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
        new Tier
        {
            TierLevel = 2,
            Teams = new List<TeamCodeNames>
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
        new Tier
        {
            TierLevel = 3,
            MaxBaseTeamValue = 1_170_000,
            Teams = new List<TeamCodeNames>
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
        new Tier
        {
            TierLevel = 4,
            Teams = new List<TeamCodeNames>
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
        new Tier
        {
            TierLevel = 5,
            Teams = new List<TeamCodeNames>
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
        new Tier
        {
            TierLevel = 6,
            Teams = new List<TeamCodeNames>
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

    public readonly static ImmutableArray<Tier> EuroBowl2025 = ImmutableArray.Create(
        new Tier
        {
            TierLevel = 1,
            MaxBaseTeamValue = 1_150_000,
            Teams = new List<TeamCodeNames>
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
        new Tier
        {
            TierLevel = 2,
            MaxBaseTeamValue = 1_160_000,
            Teams = new List<TeamCodeNames>
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
        new Tier
        {
            TierLevel = 3,
            MaxBaseTeamValue = 1_170_000,
            Teams = new List<TeamCodeNames>
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
        new Tier
        {
            TierLevel = 4,
            MaxBaseTeamValue = 1_180_000,
            Teams = new List<TeamCodeNames>
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
        new Tier
        {
            TierLevel = 5,
            MaxBaseTeamValue = 1_200_000,
            Teams = new List<TeamCodeNames>
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
        new Tier
        {
            TierLevel = 6,
            MaxBaseTeamValue = 1_200_000,
            Teams = new List<TeamCodeNames>
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
    /// <returns>ValidationResult can return Valid or Failure (with error list)</returns>
    public ValidationResult ValidateCustomTierset(IEnumerable<Tier> tierParameters)
    {
        var errors = new List<string>();
        var tierParametersList = tierParameters.ToList();
        
        if (!tierParametersList.Any())
        {
            errors.Add("List of tiers is empty.");
        }
        
        List<uint> orderedTierLevels = tierParametersList.Select(t => t.TierLevel).Order().ToList();
        
        // Check if tier levels start at 1 and are numbered sequentially up to the number of tiers submitted
        if (orderedTierLevels.First() == 1 && orderedTierLevels.Last() == tierParametersList.Count())
        {
            errors.Add("Tier levels are not sequential.\n");
        }
        
        // Check for duplicate tier levels.
        if (orderedTierLevels.Distinct().Count() < tierParametersList.Count())
        {
            errors.Add("Duplicate tier levels found.\n");
        }
        
        // Check for duplicate teams
        var duplicateTeams = tierParametersList.SelectMany(t => t.Teams)
            .GroupBy(team => team)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key.ToString())
            .ToList();
        
        if (duplicateTeams.Any())
        {
            errors.Add($"Duplicate teams found: {string.Join(", ", duplicateTeams)}\n");
        }

        return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Valid();
    }
}