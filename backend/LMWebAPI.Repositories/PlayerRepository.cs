using LMWebAPI.Models;
using LMWebAPI.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LMWebAPI.Repositories;

public class PlayerRepository<T> : MongoRepository<T> where T : Player
{
    private readonly IMongoCollection<Team> _teamCollection;

    public PlayerRepository(IMongoDatabase database) : base(database, "players_built")
    {
        _teamCollection = database.GetCollection<Team>("teams_built");
    }

    public async Task<Player?> GetByIdAsync(ObjectId id)
    {       
        return await _collection.Find(player => player.Id== id).FirstOrDefaultAsync(); 
    }

    public async Task<List<T>> GetByTeamIdAsync(ObjectId teamId)
    {
        return await _collection.Find(player => player.TeamId == teamId)
            .ToListAsync(); 
    }

    public async Task<List<T>> GetByTeamNameAsync(string teamName)
    {
        ObjectId teamId = _teamCollection.Find(team => team.Name.ToLower() == teamName.ToLower())
            .Project(team => team.Id)
            .FirstOrDefault();

        return await _collection.Find(player => player.TeamId == teamId)
            .ToListAsync();
    }
    
    
}