﻿using BloodTourney;
using Microsoft.AspNetCore.Mvc;

namespace LMWebAPI.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class TournamentController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetPresetRuleset([FromBody] string presetRulesetName)
    {
        (var ruleset, string err) = await BloodTourney.Core.GetRuleset(presetRulesetName);

        if (!string.IsNullOrWhiteSpace(err))
        {
            return new NotFoundObjectResult("Could not get requested Ruleset: " + err);
        }

        return Ok(ruleset);
    }

    [HttpGet]
    public async Task<ActionResult> ValidateRuleset([FromBody] Core.Ruleset ruleset)
    {
        List<string> errors = await BloodTourney.Core.ValidateRuleset(ruleset);

        if (errors.Any())
        {
            return new BadRequestObjectResult(errors);
        }

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> CreateCustomRuleset([FromBody] Core.Ruleset ruleset)
    {
        
    }
}