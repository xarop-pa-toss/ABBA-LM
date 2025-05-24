namespace BloodTourney.Tournament;

public class TournamentConfig
{
    /// <summary>
    /// Game rules to be used. If using Custom (default), send in BloodTourney.Ruleset object through the optional CustomRuleset parameter 
    /// </summary>
    public required Ruleset Ruleset { get; init; }

    /// <summary>
    /// Defines the round format for the tournament.
    /// Some formats might be non-eliminatory such as Round Robin, while others will cause the team to not proceed if they lose.
    /// </summary>
    public required Tournament.TournamentFormats TournamentFormat { get; init; }

    /// <summary>
    /// Base TV limit (before skills)
    /// </summary>
    public required int TeamValueLimit { get; init; }
    
    /// <summary>
    /// If false, the first round will be randomized as per the chosen format in TournamentFormat
    /// </summary>
    public required bool FirstRoundRandomSort { get; init; }

    /// <summary>
    /// If false, all excess cash leftover from the team creation process will be lost.
    /// Otherwise, it is converted into Prayers To Nuffle.
    /// </summary>
    public bool UnspentCashConvertedToPrayers { get; init; } = false;

    /// <summary>
    /// If true, any injury or death suffered by a player will be cleared after each match, and each coach will start their matches with the registered rosters.
    /// </summary>
    public bool RessurectionMode { get; init; } = true;
}