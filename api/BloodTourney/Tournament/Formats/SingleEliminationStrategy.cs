namespace BloodTourney.Tournament.Formats;

public class SingleEliminationStrategy : ITournamentFormat
{
    /// <summary>
    /// The first round will have 'byes' if the total number of teams is not a power of two.
    /// 'Byes' are represented by MatchNode objects that have TeamA set but TeamB as null.
    /// 'Byes' are attributed randomly.
    /// </summary>
    /// <param name="teamGuids"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    IEnumerable<MatchNode> ITournamentFormat.CreateFirstRoundRandom(IEnumerable<Guid> teamGuids)
    {
        var teamArray = teamGuids.ToArray();
        if (teamArray.Count() <= 1)
        {
            throw new ArgumentOutOfRangeException(nameof(teamGuids));
        }
     
        // Generate a pseudo-random list of indexes.
        new Random().Shuffle(teamArray);
        
        return CreateFirstRound(teamArray.ToList());
    }
    
    IEnumerable<MatchNode> ITournamentFormat.CreateFirstRoundSeeded(IEnumerable<(Guid, uint)> teamGuidsWithRank)
    {
        // TODO: Implement Seeding
        return new List<MatchNode>();
    }

    private IEnumerable<MatchNode> CreateFirstRound(IList<Guid> teamGuids)
    {
        if (!teamGuids.Any())
        {
            throw new ArgumentOutOfRangeException(nameof(teamGuids));
        }
        
        int totalTeams = teamGuids.Count();
        int nextPowerOfTwo = Helpers.GetNextPowerOfTwo(totalTeams);
        int amountOfByes = nextPowerOfTwo - totalTeams;
        int amountOfPlayingTeams = totalTeams - amountOfByes;
        
        // Split into playing teams and byes
        var playingTeams = teamGuids.Take(amountOfPlayingTeams).ToArray();
        var byes = teamGuids.Skip(amountOfPlayingTeams).ToArray();
        
        // Creating MatchNode pairs
        IEnumerable<Guid[]> playingTeamsChunks = playingTeams.Chunk(2);
        IEnumerable<Guid[]> byesChunks = byes.Chunk(2);
        
        List<MatchNode> matches = new List<MatchNode>();
        foreach (var chunk in playingTeamsChunks)
        {
            matches.Add(new MatchNode()
            {
                // IsBye = false,
                TeamA = chunk[0],
                TeamB = chunk[1]
            });
        }

        foreach (var chunk in byesChunks)
        {
            matches.Add(new MatchNode()
            {
                // IsBye = true,
                TeamA = chunk[0],
                TeamB = null
            });
        }
        return matches;
    }

    IEnumerable<MatchNode> ITournamentFormat.CreateNextRound(IEnumerable<MatchNode> completedRound)
    {
        // TODO: Account for players leaving mid tournament, creating Byes.
        // If player leaves on bracket finals, the last player is immediately champion
        List<MatchNode> completeRoundList = completedRound.ToList();

        if (completeRoundList.Any(w => w.Winner == null))
        {
            throw new ArgumentException("All completed rounds must have a winning team.");
        }

        List<MatchNode> matches = new List<MatchNode>();
        var previousMatchPairings = completeRoundList.Chunk(2);
        foreach (var matchPairs in previousMatchPairings)
        {
            matches.Add(new MatchNode()
            {
                // IsBye = false,
                TeamA = matchPairs[0].Winner,
                TeamB = matchPairs[1].Winner,
                Winner = null,
                Loser = null
            });
        }
        return matches;
    }
}