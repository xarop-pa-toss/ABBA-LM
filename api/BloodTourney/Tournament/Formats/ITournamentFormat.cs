namespace BloodTourney.Tournament;

internal interface ITournamentFormat
{
    /// <summary>
    /// Pseudo-randomizes a given list of teams.
    /// </summary>
    /// <param name="playerIds">Collection of all players in the tournament</param>
    /// <returns>Ordered pairs of players for first round matches</returns>
    IEnumerable<MatchNode> CreateFirstRoundRandom(IEnumerable<Guid> teamGuids);
    
    /// <summary>
    /// Creates the initial rounds of a tournament based on the given players NAF Score
    /// </summary>
    /// <param name="playerIdsWithNafScore">Dictionary of player IDs and their rankings</param>
    /// <returns>Ordered pairs of players for first round matches</returns>
    IEnumerable<MatchNode> CreateFirstRoundSeeded(Dictionary<Guid, uint> teamGuidsWithRank);
    
    /// <summary>
    /// Calculate teams' advancement through the rounds.
    /// </summary>
    /// <param name="playerStandings">Current tournament standings (points, wins, etc.)</param>
    /// <param name="completedMatches">History of completed matches to avoid rematches</param>
    /// <returns>List of player pairs for the next round</returns>
    IEnumerable<MatchNode> CreateNextRound(IEnumerable<MatchNode> completedMatches);
}

internal class MatchNode
{
    public bool IsBye { get; set; } = false;
    public Guid? TeamA { get; set; }
    public Guid? TeamB { get; set; }
    public Guid? Winner { get; set; } = null;
    public Guid? Loser { get; set; } = null;
}

public enum MatchResult
{
    Win,
    Loss,
    Draw
}