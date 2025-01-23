using LMWebAPI.Models;
using LMWebAPI.Repositories;
using MongoDB.Bson;

namespace LMWebAPI.Services.Players;

public class PlayerService
{
    private readonly PlayerRepository<Player> _playerRepository;
    public PlayerService(PlayerRepository<Player> playerRepository)
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
    
    public async Task<Player> GetByPlayerIdAsync(ObjectId playerId)
    {
        var player = await _playerRepository.GetByIdAsync(playerId);
        return player;
    }
    #endregion
    
    public async Task AddPlayerAsync(Player player)
    {
        await _playerRepository.AddOneAsync(player);
    }
    
    public async Task UpdatePlayerAsync(Player player)
    {
        await _playerRepository.ReplaceOneAsync(player);
    }
    
    public async Task UpdatePlayersAsync(List<Player> players)
    {
        await _playerRepository.ReplaceManyAsync(players);
    }

    public async Task DeletePlayerAsync(ObjectId playerId)
    {
        await _playerRepository.DeleteOneAsync(playerId);
    }
}