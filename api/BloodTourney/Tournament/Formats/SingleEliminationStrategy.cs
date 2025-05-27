namespace BloodTourney.Tournament.Formats;

internal class SingleEliminationStrategy : ITournamentFormat
{
    /// <summary>
    /// The first round will have 'byes' if total number of teams is not a power of two.
    /// 'Byes' are represented by MatchNode objects that have TeamA set but TeamB as null.
    /// </summary>
    /// <param name="teamGuids"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    IEnumerable<MatchNode> ITournamentFormat.CreateFirstRoundRandom(IEnumerable<Guid> teamGuids)
    {
        List<Guid> teamGuidList = teamGuids.ToList();
        if (!teamGuidList.Any())
        {
            throw new ArgumentOutOfRangeException(nameof(teamGuids));
        }
        
        int totalTeams = teamGuidList.Count();
        int nextPowerOfTwo = Helpers.GetNextPowerOfTwo(totalTeams);
        int previousPowerOfTwo = nextPowerOfTwo / 2;
        
        int amountOfByes = nextPowerOfTwo - totalTeams;
        int amountOfPlayingTeams = totalTeams - amountOfByes;
        int amountOfMatches = amountOfPlayingTeams * 2;
        
        
        List<int> randomizer = Enumerable.Range(1, totalTeams).ToArray().ToList();
        var playingTeamsRandomized = randomizer.Take(new Range(0, amountOfPlayingTeams - 1)).ToArray();
        var byesRandomized = randomizer.Take(new Range(amountOfPlayingTeams, randomizer.Count - 1)).ToArray();
        
        new Random().Shuffle(playingTeamsRandomized);
        new Random().Shuffle(byesRandomized);
        
        // Splitting the randomized teams indexes into chunks of 2, creating MatchNodes accordingly
        List<MatchNode> matches = new List<MatchNode>();
        IEnumerable<int[]> playingTeamsChunks = playingTeamsRandomized.Chunk(2);
        IEnumerable<int[]> byesChunks = byesRandomized.Chunk(2);
        
        foreach (var chunk in playingTeamsChunks)
        {
            matches.Add(new MatchNode()
            {
                IsBye = false,
                TeamA = teamGuidList[chunk[0]],
                TeamB = teamGuidList[chunk[1]]
            });
        }

        foreach (var chunk in byesChunks)
        {
            matches.Add(new MatchNode()
            {
                IsBye = true,
                TeamA = teamGuidList[chunk[0]],
                TeamB = teamGuidList[chunk[1]]
            });
        }
        return matches;
    }

    IEnumerable<MatchNode> ITournamentFormat.CreateFirstRoundSeeded(Dictionary<Guid, uint> teamGuidsWithRank)
    {
        // TODO: Implement Seeding
        return new List<MatchNode>();
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
                IsBye = false,
                TeamA = matchPairs[0].Winner,
                TeamB = matchPairs[1].Winner,
                Winner = null,
                Loser = null
            });
        }
        return matches;
    }
}