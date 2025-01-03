using LMWebAPI.Models;
using LMWebAPI.Services.Players;
using Microsoft.AspNetCore.Mvc;

namespace LMWebAPI.Controllers;

[ApiController]
[Route("api/players")]
public class PlayerController : ControllerBase
{
    private readonly PlayerService _playerService;

    public PlayerController(PlayerService playerService)
    {
        _playerService = playerService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Player>>> GetAllPlayers()
    {
        var players = await _playerService.GetAllPlayersAsync();
        _playerService.
        if (players.Count == 0)
            return NotFound("No players found.");
        return Ok(players);
    }

    // [HttpGet]
    // public async Task<ActionResult<Player>> GetPlayerById(string id)
    // {
    //
    // }
    
}