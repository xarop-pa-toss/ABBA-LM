using BloodTourney;
using Microsoft.AspNetCore.Mvc;

namespace LMWebAPI.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class TournamentController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> Validate([FromBody]BloodTourney.Core.BaseParameters tournamentDetails)
    {
        (var validatedParams, string err) = await BloodTourney.Core.ValidateBaseParams(Core.RulesetPresets.SardineBowl2025, tournamentDetails);

        if (!string.IsNullOrWhiteSpace(err))
        {
            return UnprocessableEntity("Tournament data is invalid." + err);
        }
        return Ok(validatedParams);
    }
}