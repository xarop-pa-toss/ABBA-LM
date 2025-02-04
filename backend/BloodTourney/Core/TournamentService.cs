using MongoDB.Driver;

namespace BloodTourney.Core;


public class TournamentService
{
    private readonly IMongoCollection<Tournament> _tournamentCollection;

    public TournamentService(IMongoDatabase database)
    {
        _tournamentCollection = database.GetCollection<Tournament>("tournaments");
    }

    public async Task CreateTournament(Tournament tournament)
    {
        await _tournamentCollection.InsertOneAsync(tournament);
    }
}