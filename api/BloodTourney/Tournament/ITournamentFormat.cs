namespace BloodTourney.Tournament;

public interface ITournamentFormat
{
    /// <summary>
    /// Pseudo-randomizes a given list of teams.
    /// </summary>
    /// <param name="playerIds">Collection of all players in the tournament</param>
    /// <returns>Ordered pairs of players for first round matches</returns>

    IEnumerable<(Guid playerA, Guid playerB)> CreateFirstRoundPairingsRandom(IEnumerable<Guid> playerIds);
    
    /// <summary>
    /// Creates the initial rounds of a tournament based on the given players NAF Score
    /// </summary>
    /// <param name="playerIdsWithNafScore">Dictionary of player IDs and their rankings</param>
    /// <returns>Ordered pairs of players for first round matches</returns>

    IEnumerable<(Guid playerA, Guid playerB)> CreateFirstRoundPairingsSeeded(Dictionary<Guid, uint> playerIdsWithNafScore);
    
    /// <summary>
    /// Calculate teams' advancement through the rounds.
    /// </summary>
    /// <param name="playerStandings">Current tournament standings (points, wins, etc.)</param>
    /// <param name="completedMatches">History of completed matches to avoid rematches</param>
    /// <returns>List of player pairs for the next round</returns>

    IEnumerable<(Guid playerA, Guid playerB)> GetNextRoundPairings(
        Dictionary<Guid, (uint wins, uint losses, uint touchdowns)> playerStandings,
        IEnumerable<(Guid playerA, Guid playerB)> completedMatches);
}

public enum MatchResult
{
    Win,
    Loss,
    Draw
}