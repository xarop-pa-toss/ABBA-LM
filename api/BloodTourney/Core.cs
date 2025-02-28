namespace BloodTourney;

public partial class Core  
{
    public struct BaseParameters
    {
        /// <summary>
        /// User ID of organizer.
        /// </summary>
        public string TournamentOrganizerId { get; set; }
        /// <summary>
        /// Display name of the Tournament. Duplicate named tournaments are not advised.
        /// </summary>
        public string TournamentName { get; set;}
        public int PlayerLimit { get; set;}
        public DateTime StartDate { get; set;}
        /// <summary>
        /// Arbitrary string for locale, no geolocation involved.
        /// </summary>
        public string Location { get; set;}
        /// <summary>
        /// Rulesets do not influence Tournament Format
        /// </summary>
        public Ruleset Ruleset { get; set; }
        /// <summary>
        /// Round progression format. e.g. Round Robin, Swiss, etc
        /// </summary>
        public TournamentFormats TournamentFormat { get; set; }
    }
    
    public struct 
    
    public enum TournamentFormats
    {
        RoundRobin,
        Swiss,
        SingleElimination,
        DoubleElimination,
        KingOfTheHill
    }
    public enum SkillStacks
    {
        None,
        OnlyPrimaries,
        AllSkills
    }
    public struct TournamentParameters
    {
        /// <summary>
        /// Base TV limit (before skills)
        /// </summary>
        public int TeamValueLimit { get; set;}
        
        /// <summary>
        /// If false, all excess cash leftover from the team creation process will be lost.
        /// Otherwise, it is converted into Prayers To Nuffle.
        /// </summary>
        public bool UnspentCashConvertedToPrayers { get; set; }
        /// <summary>
        /// Any injury or death suffered by a player will be cleared after each match, and each coach will start their matches with the registered rosters.
        /// </summary>
        public bool RessurectionMode { get; set;}
        
        public SkillStacks SkillStacking { get; set; }
        /// <summary>
        /// Defines the rounds format for the tournament.
        /// Some formats might be non-eliminatory such as Round Robin, while others will cause the team to not proceed if they lose.
        /// </summary>
        public TournamentFormats TournamentFormat { get; set; }
        
        /// <summary>
        /// If false, first round will be randomized as per the chosen format in TournamentFormat
        /// </summary>
        public bool FirstRoundRandomSort { get; set;}
    }
    public struct FormatParameters
    {
        private 
    }
    
    private TournamentParameters BloodBowl2020 = new TournamentParameters()
    {
        TournamentOrganizerId = "",
        TournamentName = "",
        PlayerLimit = 0,
        TeamValueLimit = 0,
        StartDate = new DateTime(2020, 01, 01),
        Location = "",
        IsInvitationOnly = false
    };
    
    private Dictionary<Rulesets, BaseParameters>  = new Dictionary<BaseParameters, BaseParameters>();
    
    /// <summary>
    /// Validate tournament's base parameters against given ruleset.
    /// </summary>
    /// <param name="tbd"></param>
    /// <returns></returns>
    public static async Task<(Rulesets ruleset, BaseParameters baseParameters, string err)> ValidateBaseParams(BaseParameters tbd)
    {
        string err = String.Empty;
        
        if (tbd.PlayerLimit < 2) {err = "Player limit must be greater than 2.";}
        if (tbd.TeamValueLimit < 0) {err = "Team limit must be greater than 0.";}
        if (tbd.StartDate < DateTime.UtcNow) {err = "Start date must be today or in the future.";};

        return (tbd, err);
    }
}