using BloodTourney.Models;
using System.Collections.Immutable;

namespace BloodTourney.Ruleset;

public interface IRulesetBuilder
{
    IRulesetBuilder WithTiers(IEnumerable<Tiers.TierParameters> tiers);
    IRulesetBuilder WithVictoryPoints(VictoryPoints victoryPoints);
    IRulesetBuilder WithTieBreakers(IEnumerable<string> tieBreakers);
    IRulesetBuilder WithTimeKeeping(TimeKeeping timekeeping);
    IRulesetBuilder WithSkillStacking(SkillStacking skillStacking);
    IRulesetBuilder WithInducements(IEnumerable<Inducement> inducements);
    IRulesetBuilder WithBannedStarPlayers(IEnumerable<string> bannedStarPlayers);
    IRulesetBuilder WithGuidelines(IEnumerable<string> guidelines);
    IRulesetBuilder WithAdditionalRules(OtherRules otherRules);
    Models.Ruleset Build();
}

public class RulesetBuilder : IRulesetBuilder
{
    private IEnumerable<Tiers.TierParameters>? _tiers;
    private VictoryPoints? _victoryPoints;
    private IEnumerable<string>? _tieBreakers;
    private TimeKeeping? _timekeeping;
    private SkillStacking? _skillstacking;
    private IEnumerable<Inducement>? _inducements;
    private IEnumerable<string>? _bannedStarPlayers;
    private IEnumerable<string>? _guidelines;
    private OtherRules? _otherRules;

    public static IRulesetBuilder Create() => new RulesetBuilder();

    public IRulesetBuilder WithTiers(IEnumerable<Tiers.TierParameters> tiers)
    {
        var tierParametersList = tiers.ToList();
        Helpers.CheckIfCollectionNullOrEmpty(tierParametersList).ThrowIfHasErrors(message: "Tiers not provided.");
        
        _tiers = tierParametersList;
        return this;
}

    public IRulesetBuilder WithVictoryPoints(VictoryPoints victoryPoints)
    {
        _victoryPoints = victoryPoints;
        return this;
    }

    public IRulesetBuilder WithTieBreakers(IEnumerable<string> tieBreakers)
    {
        var tieBreakersList = tieBreakers.ToList();
        Helpers.CheckIfCollectionNullOrEmpty(tieBreakersList).ThrowIfHasErrors(message:"TieBreakers not provided.");
        
        _tieBreakers = tieBreakersList;
        return this;
    }
    
    public IRulesetBuilder WithTimeKeeping(TimeKeeping timekeeping)
    {
        _timekeeping = timekeeping;
        return this;
    }
    
    public IRulesetBuilder WithSkillStacking(SkillStacking skillStacking)
    {
        _skillstacking = skillStacking;
        return this;
    }
    
    public IRulesetBuilder WithInducements(IEnumerable<Inducement> inducements)
    {
        _inducements = inducements;
        return this;
    }
    
    public IRulesetBuilder WithBannedStarPlayers(IEnumerable<string> bannedStarPlayers)
    {
        var bannedStarPlayersList = bannedStarPlayers.ToList();
        Helpers.CheckIfCollectionNullOrEmpty(bannedStarPlayersList).ThrowIfHasErrors(message:"Banned Star Players not provided.");
        
        _bannedStarPlayers = bannedStarPlayersList;
        return this;
    }
    
    public IRulesetBuilder WithGuidelines(IEnumerable<string> guidelines)
    {
        var guidelinesList = guidelines.ToList();
        Helpers.CheckIfCollectionNullOrEmpty(guidelinesList).ThrowIfHasErrors(message:"Guidelines not provided");

        _guidelines = guidelinesList;
        return this;
    }
    
    public IRulesetBuilder WithAdditionalRules(OtherRules otherRules)
    {
        _otherRules = otherRules;
        return this;
    }
    
    public Models.Ruleset Build()
    {
        ValidateRequiredFields();

        return new Models.Ruleset
        {
            Tiers = _tiers!,
            MatchVictoryPoints = _victoryPoints!.Value,
            TieBreakers = _tieBreakers!,
            Timekeeping = _timekeeping!.Value,
            Skillstacking = _skillstacking!.Value,
            Inducements = _inducements?.ToImmutableArray() ?? ImmutableArray<Inducement>.Empty,
            BannedStarPlayers = _bannedStarPlayers,
            Guidelines = _guidelines,
            AdditionalRules = _otherRules ?? GetDefaultOtherRules()
        };
    }

    private void ValidateRequiredFields()
    {
        if (_tiers == null || !_tiers.Any())
            throw new InvalidOperationException("Tiers must be provided.");
        
        if (_victoryPoints == null)
            throw new InvalidOperationException("VictoryPoints must be provided.");
        
        if (_tieBreakers == null)
            throw new InvalidOperationException("TieBreakers must be provided.");
        
        if (_timekeeping == null)
            throw new InvalidOperationException("Timekeeping must be provided.");
        
        if (_skillstacking == null)
            throw new InvalidOperationException("Skillstacking must be provided.");
    }

    private static OtherRules GetDefaultOtherRules()
    {
        return new OtherRules
        {
            UseTVForNonPlayerExtras = false,
            AllowSameStarPlayerOnBothTeams = true,
            RemovePlayersAddedDuringMatchAtGameEnd = true
        };
    }
}