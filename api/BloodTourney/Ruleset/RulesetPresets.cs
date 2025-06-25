using System.Collections.Immutable;

namespace BloodTourney;

public class RulesetPresets
{
    public enum RulesetPresetsEnum
    {
        EuroBowl2025,
        SardineBowl2025
    }
    
    internal static Ruleset CreateSardineBowl2025Ruleset()
    {
        var skillstacking = new Ruleset.SkillStacking()
        {
            StackingType = Ruleset.SkillStacks.OnlyPrimaries,
            ExtraSkillstackCosts = new Dictionary<ImmutableHashSet<string>, uint>()
            {
                [ImmutableHashSet.Create("Tackle", "Mighty Blow")] = 2,
                [ImmutableHashSet.Create("Claw", "Mighty Blow")] = 2,
                [ImmutableHashSet.Create("Sneaky Git", "Dirty Player")] = 3
            },
            ExtraCostAppliesToDefaultSkills = false
        };

        var inducements = ImmutableArray.Create<Ruleset.Inducement>(
            new Ruleset.Inducement()
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
            new Ruleset.Inducement()
            {
                Min = 0,
                Max = 6,
                Name = "Assistant Coach",
                Cost = 10_000
            },
            new Ruleset.Inducement()
            {
                Min = 0,
                Max = 12,
                Name = "Cheerleaders",
                Cost = 10_000
            },
            new Ruleset.Inducement()
            {
                Min = 0,
                Max = 2,
                Name = "Bloodweiser Kegs",
                Cost = 50_000
            },
            new Ruleset.Inducement()
            {
                Min = 0,
                Max = 3,
                Name = "Bribes",
                Cost = 100_000,
                CostOverrideForTeamsWithTrait = new Dictionary<string, uint>
                {
                    ["Bribery & Corruption"] = 50_000
                }
            },
            new Ruleset.Inducement()
            {
                Min = 0,
                Max = 2,
                Name = "Wandering Apothecaries",
                Cost = 100_000
            },
            new Ruleset.Inducement()
            {
                Min = 0,
                Max = 1,
                Name = "Morgue Assistant",
                Cost = 100_000
            },
            new Ruleset.Inducement()
            {
                Min = 0,
                Max = 1,
                Name = "Plague Doctor",
                Cost = 100_000
            },
            new Ruleset.Inducement()
            {
                Min = 0,
                Max = 1,
                Name = "Riotous Rookies",
                Cost = 100_000
            },
            new Ruleset.Inducement()
            {
                Min = 0,
                Max = 1,
                Name = "Star Players",
                Cost = 0, // Variable cost based on star player
                TiersAllowed = new[] { 3, 4, 5, 6 }
            }
        );

        var timeKeeping = new Ruleset.TimeKeeping()
        {
            ChessClockAllowed = true,
            MatchTimelimitInMinutes = 150
        };

        var victoryPoints = new Ruleset.VictoryPoints();

        var bannedStarPlayers = ImmutableArray.Create<string>(
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
        );

        var tieBreakers = new[]
        {
            "Net TD's + Net CAS",
            "Most TD's",
            "Head to Head (Top 3)",
            "Random"
        };

        var additionalRules = new[]
        {
            "Undead, Necromantic and Nurgle teams are allowed to apply the Masters of Undeath and Plague Ridden special rules."
        };

        var tierParameters = Tiers.SardineBowl2025;
        var guidelines = ImmutableArray<string>.Empty;

        var otherRules = new Ruleset.OtherRules()
        {
            UseGpForNonPlayerExtras = true,
            RemovePlayersAddedDuringMatchAtGameEnd = true,
            AllowSameStarPlayerOnBothTeams = true,
            AdditionalRules =
            [
                "Undead, Necromantic and Nurgle teams are allowed to apply the Masters of Undeath and Plague Ridden special rules."
            ]
        };
        
        return Ruleset.Builder.Create()
            .WithTiers(tierParameters)
            .WithVictoryPoints(victoryPoints)
            .WithTieBreakers(tieBreakers)
            .WithTimeKeeping(timeKeeping)
            .WithSkillStacking(skillstacking)
            .WithInducements(inducements)
            .WithBannedStarPlayers(bannedStarPlayers)
            .WithGuidelines(guidelines)
            .WithAdditionalRules(new Ruleset.OtherRules
            {
                UseGpForNonPlayerExtras = true,
                RemovePlayersAddedDuringMatchAtGameEnd = true,
                AllowSameStarPlayerOnBothTeams = true,
                AdditionalRules = additionalRules
            })
            .Build();

    }
}