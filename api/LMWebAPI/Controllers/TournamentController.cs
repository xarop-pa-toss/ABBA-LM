using Microsoft.AspNetCore.Mvc;

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