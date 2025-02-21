namespace BloodTourney;

public partial class Core
{
    public enum Rulesets
    {
        BloodBowl2020,
        SardineBowl2025,
        Custom
    }
    
    var SardineBowl2025Ruleset = new RulesetParameters()
    {
        
    }

    public static Rulesets GetPremadeRuleset(Rulesets ruleset)
    {
        switch (ruleset)
        {
            case Rulesets.BloodBowl2020:
                
            
        }
    }
}