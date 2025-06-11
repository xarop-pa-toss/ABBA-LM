using BloodTourney.Tournament;

namespace BloodTourney;
//TODO 
// Implement a Lock system after tournament is validated and accepted by user.

public class Core
{
    private readonly Dictionary<RulesetPresets, Ruleset> _presetRulesets;
    public Core()
    {
        _presetRulesets = new Dictionary<RulesetPresets, Ruleset>();
        CreateBaseRulesets();
    }
    public struct BaseParameters
    {
        /// <summary>
        /// User ID of organiser.
        /// </summary>
        public string TournamentOrganizerId { get; set; }
        /// <summary>
        /// Display the name of the Tournament. Duplicate named tournaments are not advised.
        /// </summary>
        public string TournamentName { get; set; }
        public int PlayerLimit { get; set; }
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Arbitrary string for locale, no geolocation involved.
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// Rulesets do not influence Tournament Format
        /// </summary>
        public Ruleset Ruleset { get; set; }
        /// <summary>
        /// Round progression format. E.g. Round Robin, Swiss, etc.
        /// </summary>
        public required Tournament.Tournament.TournamentFormats TournamentFormat { get; set; }
        public required TournamentConfig TournamentSettings { get; set; }
    }
    
    /// <summary>
    /// Validate tournament's base parameters against given ruleset.
    /// </summary>
    /// <param name="ruleset"></param>
    /// <param name="baseParams"></param>
    /// <returns></returns>
    public static async Task<(BaseParameters baseParameters, string err)> ValidateBaseParams(RulesetPresets ruleset, BaseParameters baseParams)
    {
        string err = String.Empty;

        if (baseParams.PlayerLimit < 2) { err = "Player limit must be 2 or more."; }
        if (baseParams.TournamentSettings.TeamValueLimit < 0) { err = "Team limit must be greater than 0."; }
        if (baseParams.StartDate < DateTime.UtcNow) { err = "Start date must be today or in the future."; }
        

        return (baseParams, err);
    }

    private void CreateBaseRulesets()
    {
        
    }
}