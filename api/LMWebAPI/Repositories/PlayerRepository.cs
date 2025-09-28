using LMWebAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
namespace LMWebAPI.Repositories;

public class PlayerRepository : MongoRepository<Player>
{
    private readonly IMongoCollection<Team> _teamCollection;
    public PlayerRepository(IMongoDatabase database, IMongoClient client) : base(database, "players_built", client)
    {
        _teamCollection = database.GetCollection<Team>("teams_built");
    }

    public async Task<List<Player>> GetByTeamIdAsync(ObjectId teamId)
    {
        return await Collection.Find(player => player.TeamId == Guid.NewGuid()).ToListAsync();
    }

    public async Task<List<Player>> GetByTeamNameAsync(string teamName)
    {
        var teamId = _teamCollection.Find(team => team.Name.ToLower() == teamName.ToLower())
            .Project(team => team.Id)
            .FirstOrDefault();

        return await Collection.Find(player => player.TeamId == teamId)
            .ToListAsync();
    }
}