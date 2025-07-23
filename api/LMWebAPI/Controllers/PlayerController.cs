using LMWebAPI.Models;
using LMWebAPI.Resources.Errors;
using LMWebAPI.Services.Players;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace LMWebAPI.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class PlayerController(PlayerService playerService) : ControllerBase
{
    private readonly PlayerService _playerService = playerService;

    [HttpGet]
    public async Task<ActionResult<List<Player>>> GetAll()
    {
        var players = await _playerService.GetAllAsync();
        return Ok(players);    
    }
    
    [HttpGet]
    public async Task<ActionResult<Player>> GetById([FromBody]string playerId)
    {
        if (!ObjectId.TryParse(playerId, out ObjectId id))
        {
            throw new Problem400BadRequestException("Player ID is not a valid ObjectId.");
        }
        
        var player = await _playerService.GetByPlayerIdAsync(id);
        return Ok(player);
    }

    [HttpGet]
    public async Task<ActionResult<List<Player>>> GetPlayersByTeamId([FromBody]string teamId)
    {
        if (!ObjectId.TryParse(teamId, out ObjectId id))
        {
            throw new Problem400BadRequestException("Team ID is not a valid ID.");
        }
        
        var players = await _playerService.GetByTeamIdAsync(id);
        return Ok(players);    
    }

    [HttpPost]
    public async Task<ActionResult<Player>> AddOne([FromBody] Player player)
    {
        await _playerService.AddOneAsync(player);
        return Ok(player);
    }
    
    [HttpPost]
    public async Task<ActionResult<List<Player>>> AddMany([FromBody] List<Player> players)
    {
        await _playerService.AddManyAsync(players);
        return Ok(players);
    }
    
    [HttpPut]
    public async Task<ActionResult<Player>> UpdateOne([FromBody] Player player)
    {
        await _playerService.ReplaceOneAsync(player);
        return Ok(player);
    }

    [HttpPut]
    public async Task<ActionResult<List<Player>>> UpdateMany([FromBody] List<Player> players)
    {
        await _playerService.ReplaceManyAsync(players);
        return Ok(players);
    }

    [HttpDelete]
    public async Task<ActionResult<Player>> DeleteOne([FromBody] ObjectId playerId)
    {
        await _playerService.DeleteOneAsync(playerId);
        return Ok(playerId);
    }
}