namespace BloodTourney.Tournament;

public interface ITournament
{
    /// <summary>
    /// Game rules to be used. If using Custom (default), send in BloodTourney.Ruleset object through the optional CustomRuleset parameter 
    /// </summary>
    public Ruleset Ruleset { get; init; }

    /// <summary>
    /// Defines the round format for the tournament.
    /// Some formats might be non-eliminatory such as Round Robin, while others will cause the team to not proceed if they lose.
    /// </summary>
    public Tournament.TournamentFormats TournamentFormat { get; init; }

    /// <summary>
    /// If false, the first round will be randomized as per the chosen format in TournamentFormat
    /// </summary>
    public bool FirstRoundRandomSort { get; init; }

    /// <summary>
    /// Base TV limit (before skills)
    /// </summary>
    public int TeamValueLimit { get; init; }

    /// <summary>
    /// If false, all excess cash leftover from the team creation process will be lost.
    /// Otherwise, it is converted into Prayers To Nuffle.
    /// </summary>
    public bool UnspentCashConvertedToPrayers { get; init; }

    /// <summary>
    /// Any injury or death suffered by a player will be cleared after each match, and each coach will start their matches with the registered rosters.
    /// </summary>
    public bool RessurectionMode { get; init; }
    
    IEnumerable<Guid> CreateFirstRoundsRandom(IEnumerable<Guid> playerIds);
    IEnumerable<Guid> CreateFirstRoundsWithSeed(Dictionary<Guid, uint> playerIdWithNafScore);
}