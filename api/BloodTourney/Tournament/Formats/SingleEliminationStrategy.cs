using System;
using MongoDB.Bson;
namespace BloodTourney.Tournament;

public class SingleEliminationStrategy : ITournamentFormat
{
    public IEnumerable<MatchNode> CreateFirstRoundRandom(IEnumerable<Guid> teamGuid)
    {
        if (!teamGuid.Any())
        {
            throw new ArgumentOutOfRangeException("List of teams is empty.");
        }
        
        int totalTeams = teamGuid.Count();
        int nextPowerOfTwo = Helpers.GetNextPowerOfTwo(teamGuid.Count());
        
        int playingTeams = nextPowerOfTwo - totalTeams;
        int byes = nextPowerOfTwo - playingTeams;
        
        var random = new Random();
        int[] teamIndexesRandomized = Enumerable.Range(1, playingTeams).ToArray();
        random.Shuffle(teamIndexesRandomized);
        
        
        ///TODO: Check for byes and create list of MatchNode. Byes are Matchnodes with only PlayerA
    }
}