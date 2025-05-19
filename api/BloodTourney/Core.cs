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
        /// User ID of organizer.
        /// </summary>
        public string TournamentOrganizerId { get; set; }
        /// <summary>
        /// Display name of the Tournament. Duplicate named tournaments are not advised.
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
        /// Round progression format. e.g. Round Robin, Swiss, etc
        /// </summary>
        public required TournamentFormats TournamentFormat { get; set; }
        public required TournamentParameters TournamentSettings { get; set; }
    }

    public enum TournamentFormats
    {
        RoundRobin,
        Swiss,
        SingleElimination,
        DoubleElimination,
        KingOfTheHill
    }

    public struct TournamentParameters
    {
        /// <summary>
        /// Game rules to be used. If using Custom (default), send in BloodTourney.Ruleset object through the optional CustomRuleset parameter 
        /// </summary>
        public required RulesetPresets Ruleset { get; init; } = RulesetPresets.SardineBowl2025;
        public Ruleset CustomRuleset { get; init; }

        /// <summary>
        /// Defines the rounds format for the tournament.
        /// Some formats might be non-eliminatory such as Round Robin, while others will cause the team to not proceed if they lose.
        /// </summary>
        public required TournamentFormats TournamentFormat { get; init; }

        /// <summary>
        /// If false, first round will be randomized as per the chosen format in TournamentFormat
        /// </summary>
        public required bool FirstRoundRandomSort { get; init; }

        /// <summary>
        /// Base TV limit (before skills)
        /// </summary>
        public required int TeamValueLimit { get; set; }

        /// <summary>
        /// If false, all excess cash leftover from the team creation process will be lost.
        /// Otherwise, it is converted into Prayers To Nuffle.
        /// </summary>
        public required bool UnspentCashConvertedToPrayers { get; init; }

        /// <summary>
        /// Any injury or death suffered by a player will be cleared after each match, and each coach will start their matches with the registered rosters.
        /// </summary>
        public required bool RessurectionMode { get; init; }

        public TournamentParameters()
        {
        }
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
        ;

        return (baseParams, err);
    }

    private void CreateBaseRulesets()
    {
        
    }
}