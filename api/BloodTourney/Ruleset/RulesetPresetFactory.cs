using System.Collections.Immutable;
using BloodTourney.Models;

namespace BloodTourney.Ruleset;

public interface IRulesetPresetFactory
{
    Models.Ruleset CreateSardineBowl2025();
    Models.Ruleset CreateEuroBowl2025();
    Models.Ruleset CreatePreset(RulesetPresets presetType);
}

public enum RulesetPresets
{
    SardineBowl2025,
    EuroBowl2025
}

internal class RulesetPresetFactory : IRulesetPresetFactory
{
    // private readonly IRulesetBuilder _builder = new RulesetBuilder() ?? throw new ArgumentNullException(nameof(_builder));

    public Models.Ruleset CreatePreset(RulesetPresets presetType)
    {
        return presetType switch
        {
            RulesetPresets.SardineBowl2025 => CreateSardineBowl2025(),
            // RulesetPresets.EuroBowl2025 => CreateEuroBowl2025(),
            _ => throw new ArgumentException($"Unknown preset type: {presetType}", nameof(presetType))
        };
    }

    public Models.Ruleset CreateSardineBowl2025()
    {
        var skillstacking = new SkillStacking
        {
            StackingType = SkillStacks.OnlyPrimaries,
            ExtraSkillstackCosts = new Dictionary<ImmutableHashSet<string>, uint>
            {
                [ImmutableHashSet.Create("Tackle", "Mighty Blow")] = 2,
                [ImmutableHashSet.Create("Claw", "Mighty Blow")] = 2,
                [ImmutableHashSet.Create("Sneaky Git", "Dirty Player")] = 3
            },
            ExtraCostAppliesToDefaultSkills = false
        };

        var inducements = ImmutableArray.Create<Inducement>(
            new() { Min = 0, Max = 1, Name = "Halfling Master Chef", Cost = 300_000, 
                CostOverrideForTeam = new Dictionary<string, uint> { ["halflings"] = 100_000 } },
            new() { Min = 0, Max = 6, Name = "Assistant Coach", Cost = 10_000 },
            new() { Min = 0, Max = 12, Name = "Cheerleaders", Cost = 10_000 },
            new() { Min = 0, Max = 2, Name = "Bloodweiser Kegs", Cost = 50_000 },
            new() { Min = 0, Max = 3, Name = "Bribes", Cost = 100_000,
                CostOverrideForTeamsWithTrait = new Dictionary<string, uint> { ["Bribery & Corruption"] = 50_000 } },
            new() { Min = 0, Max = 2, Name = "Wandering Apothecaries", Cost = 100_000 },
            new() { Min = 0, Max = 1, Name = "Morgue Assistant", Cost = 100_000 },
            new() { Min = 0, Max = 1, Name = "Plague Doctor", Cost = 100_000 },
            new() { Min = 0, Max = 1, Name = "Riotous Rookies", Cost = 100_000 },
            new() { Min = 0, Max = 1, Name = "Star Players", Cost = 0, TiersAllowed = new[] { 3, 4, 5, 6 } }
        );
        
        var timeKeeping = new TimeKeeping
        {
            ChessClockAllowed = true,
            MatchTimelimitInMinutes = 150
        };

        var victoryPoints = new VictoryPoints
        {
            Win = 3,
            Draw = 1,
            Loss = 0
        };
        var bannedStarPlayers = ImmutableArray.Create(
            "Morg'N'Thorg", "Griff Oberwald", "Deeproot Strongbranch", "Hakflem Skuttlespike",
            "Kreek Rustgouger", "Bomber Dribblesnot", "Estelle la Veneaux", "Cindy Piewhistle",
            "Wilhem Chaney", "Ivan \"The Animal\" Deathshroud", "Skitter Stab Stab", "Varag Ghoulchewer",
            "Dribl and Drill", "H'Thark the Unstoppable", "Lord Borak"
        );
        var tieBreakers = new[] { "Net TD's + Net CAS", "Most TD's", "Head to Head (Top 3)", "Random" };
        var additionalRules = new[] { "Undead, Necromantic and Nurgle teams are allowed to apply the Masters of Undeath and Plague Ridden special rules." };
        var otherRules = new OtherRules
        {
            UseTVForNonPlayerExtras = true,
            RemovePlayersAddedDuringMatchAtGameEnd = true,
            AllowSameStarPlayerOnBothTeams = true,
            AdditionalRules = additionalRules
        };

        return RulesetBuilder.Create()
            .WithTiers(TierCreator.SardineBowl2025)
            .WithVictoryPoints(victoryPoints)
            .WithTieBreakers(tieBreakers)
            .WithTimeKeeping(timeKeeping)
            .WithSkillStacking(skillstacking)
            .WithInducements(inducements)
            .WithBannedStarPlayers(bannedStarPlayers)
            .WithGuidelines(ImmutableArray<string>.Empty)
            .WithAdditionalRules(otherRules)
            .Build();
    }

    public Models.Ruleset CreateEuroBowl2025()
    {
        // Implementation for EuroBowl2025
        throw new NotImplementedException("EuroBowl2025 preset not yet implemented");
    }
}
