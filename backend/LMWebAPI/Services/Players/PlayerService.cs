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
    
    public async Task<List<Player>> GetAllPlayersAsync()
    {
        return await _playerRepository.GetAllAsync();
    }

    public async Task<List<Player>> GetPlayersByTeamIdAsync(ObjectId teamId)
    {
        var players = _playerRepository.GetByTeamIdAsync(teamId);
        return await players;
    }

    public async Task<Player> GetPlayerByIdAsync(ObjectId playerId)
    {
        var player = _playerRepository.GetByIdAsync(playerId);
        return await player;
    }

    #region CREATE
    public async Task<Player> CreatePlayerAsync(Player player)
    {
        var createResult = _playerRepository.AddAsync(player);
        return await createResult;
    }
    #endregion
    
}