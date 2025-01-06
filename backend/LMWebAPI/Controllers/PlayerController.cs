using LMWebAPI.Models;
using LMWebAPI.Services.Players;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LMWebAPI.Controllers;

[Authorize]
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
        if (players.Count == 0)
            return NotFound("No players found.");
        return Ok(players);    
    }

    // [HttpGet]
    // public async Task<ActionResult<Player>> GetPlayer(string id)
    // {
    //
    // }
    
}