using LMWebAPI.Models;
using LMWebAPI.Resources.Errors;
using LMWebAPI.Services.Players;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;


namespace LMWebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
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
        return Ok(players);    
    }
    
    [HttpGet]
    public async Task<ActionResult<Player>> GetPlayerById([FromBody]string playerId)
    {
        if (!ObjectId.TryParse(playerId, out ObjectId id))
        {
            throw new ProblemNotFoundException("Player ID is not a valid ID.");
        }
        
        var player = await _playerService.GetPlayerByIdAsync(id);
        return Ok(player);    
    }

    [HttpGet]
    public async Task<ActionResult<List<Player>>> GetPlayersByTeamId([FromBody]string teamId)
    {
        if (!ObjectId.TryParse(teamId, out ObjectId id))
        {
            throw new ProblemNotFoundException("Team ID is not a valid ID.");
        }
        
        var players = await _playerService.GetPlayersByTeamIdAsync(id);
        return Ok(players);    
    }

    [HttpPost]
    public async Task<ActionResult<Player>> AddPlayer([FromBody] Player player)
    {
        var result = await _playerService.AddPlayerAsync()
            //
    }
    
}