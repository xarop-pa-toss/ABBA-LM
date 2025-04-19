namespace BloodTourney;

public class RulesetBuilder
{
    private Core.Ruleset ruleset = new Core.Ruleset();

   

    public RulesetBuilder WithBannedStarPlayers(IEnumerable<string> bannedStarPlayers)
    {
        ruleset.BannedStarPlayers = bannedStarPlayers;
        return this;
    }

    public RulesetBuilder WithGuidelines(IEnumerable<string> guidelines)
    {
        ruleset.Guidelines = guidelines;
        return this;
    }

    public RulesetBuilder WithOtherRules(Core.OtherRules otherRules)
    {
        ruleset.OtherRules = otherRules;
        return this;
    }

    public Ruleset Build()
    {
        return ruleset;
    }
    
}