namespace BloodTourney.Tournament.Formats;

public interface ITournamentFormat
{
    /// <summary>
    /// Pseudo-randomizes a given list of teams.
    /// </summary>
    /// <param name="teamGuids">Collection of all players in the tournament</param>
    /// <returns>Ordered pairs of players for first round matches</returns>
    IEnumerable<MatchNode> CreateFirstRoundRandom(IEnumerable<Guid> teamGuids);
    
    /// <summary>
    /// Creates the initial rounds of a tournament based on the given players NAF Score
    /// </summary>
    /// <param name="teamGuidsWithRank">Dictionary of player IDs and their rankings</param>
    /// <returns>Ordered pairs of players for first round matches</returns>
    IEnumerable<MatchNode> CreateFirstRoundSeeded(IEnumerable<(Guid, uint)> teamGuidsWithRank);
    
    /// <summary>
    /// Calculate teams' advancement through the rounds.
    /// </summary>
    /// <param name="completedMatches">History of completed matches to avoid rematches</param>
    /// <returns>List of player pairs for the next round</returns>
    IEnumerable<MatchNode> CreateNextRound(IEnumerable<MatchNode> completedMatches, IEnumerable<Guid> teamsThatAbandoned = null);
}

public class MatchNode
{
    // public bool IsBye { get; set; } = false;
    public required Guid? TeamA { get; init; }
    public required Guid? TeamB { get; init; }
    public Guid? Winner { get; set; } = null;
    public Guid? Loser { get; set; } = null;
    public bool? TeamAAbandoned { get; set; } = false;
    public bool? TeamBAbandoned { get; set; } = false;
}

public enum MatchResult
{
    Win,
    Loss,
    Draw
}