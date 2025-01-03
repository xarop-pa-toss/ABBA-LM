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
    public async Task<List<Player>> GetAllPlayersAsync()
    {
        return await _playerRepository.GetAllAsync();
    }

    public async Task<List<Player>> GetPlayersByTeamIdAsync(ObjectId teamId)
    {
        var players = await _playerRepository.GetByTeamIdAsync(teamId);
        return players;
    }

    public async Task<Player> GetPlayerByIdAsync(ObjectId playerId)
    {
        var player = await _playerRepository.GetByIdAsync(playerId);
        return player;
    }
    #endregion

    #region CREATE
    public async Task<Player> CreatePlayerAsync(Player player)
    {
        var createResult = _playerRepository.AddAsync(player);
        return await createResult;
    }
    #endregion
    
}