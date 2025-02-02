using LMWebAPI.Models;
using LMWebAPI.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LMWebAPI.Services.Players;

public class PlayerService
{
    private readonly PlayerRepository _playerRepository;
    public PlayerService(PlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }
    
    #region GET
    public async Task<List<Player>> GetAllAsync()
    {
        return await _playerRepository.GetAllAsync();
    }
    
    public async Task<List<Player>> GetByTeamIdAsync(ObjectId teamId)
    {
        var players = await _playerRepository.GetByTeamIdAsync(teamId);
        return players;
    }
    
    public async Task<List<Player>> GetByTeamNameAsync(string teamName)
    {
        var players = await _playerRepository.GetByTeamNameAsync(teamName);
        return players;
    }
    
    public async Task<Player> GetByPlayerIdAsync(ObjectId playerId)
    {
        var player = await _playerRepository.GetByIdAsync(playerId);
        return player;
    }
    #endregion
    
    public async Task AddOneAsync(Player player)
    {
        await _playerRepository.AddOneAsync(player);
    }
    
    public async Task AddManyAsync(List<Player> players)
    {
        await _playerRepository.AddOneAsync(player);
    }
    
    public async Task ReplaceOneAsync(Player player)
    {
        await _playerRepository.ReplaceOneAsync(player);
    }
    
    public async Task ReplaceManyAsync(List<Player> players)
    {
        await _playerRepository.ReplaceManyAsync(players);
    }

    public async Task DeleteOneAsync(ObjectId playerId)
    {
        await _playerRepository.DeleteOneAsync(playerId);
    }
}