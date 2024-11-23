using LMWebAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LMWebAPI.Repositories;

public class PlayerRepository<T>(IMongoDatabase database) : MongoRepository<T>(database, "players_built")
    where T : Player
{
    private readonly IMongoCollection<Team> _teamCollection = database.GetCollection<Team>("teams_built");
    
    public async Task<List<T>> GetByTeamIdAsync(ObjectId teamId)
    {
        return await Collection.Find(player => player.TeamId == teamId)
            .ToListAsync(); 
    }

    public async Task<List<T>> GetByTeamNameAsync(string teamName)
    {
        ObjectId teamId = _teamCollection.Find(team => team.Name.ToLower() == teamName.ToLower())
            .Project(team => team.Id)
            .FirstOrDefault();

        return await Collection.Find(player => player.TeamId == teamId)
            .ToListAsync();
    }
}