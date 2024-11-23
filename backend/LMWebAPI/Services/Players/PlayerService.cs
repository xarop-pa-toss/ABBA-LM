using LMWebAPI.Models;
using LMWebAPI.Repositories;

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
        var players = await _playerRepository.GetAllAsync();
        return players;
    }
}