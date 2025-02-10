using LMWebAPI.Models;
using LMWebAPI.Resources.Errors;
using LMWebAPI.Services.Players;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LMWebAPI.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class TournamentController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> Validate([FromBody]BloodTourney.Core.Validation.BaseParameters tournamentDetails)
    {
        (var validatedParams, string err) = await BloodTourney.Core.Validation.ValidateTournamentParams(tournamentDetails);

        if (!string.IsNullOrWhiteSpace(err))
        {
            return UnprocessableEntity("Tournament data is invalid." + err);
        }
        return Ok(validatedParams);
    } 
}