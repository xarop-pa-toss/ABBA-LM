using MongoDB.Driver;

namespace BloodTourney.Core;

public class TournamentService
{
    public TournamentService()
    {
        
    }

    public async Task CreateTournament(Tournament tournament)
    {
        await _tournamentCollection.InsertOneAsync(tournament);
    }
}