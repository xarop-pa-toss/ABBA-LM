using System.Text.Json.Nodes;
using BloodTourney;
using LMWebAPI.Models.DTOs;
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
        (var ruleset, string err) = await BloodTourney.Ruleset.GetPresetRuleset(RulesetPresets.RulesetPresetsEnum.SardineBowl2025);

        if (!string.IsNullOrWhiteSpace(err))
        {
            return new NotFoundObjectResult("Could not get requested Ruleset: " + err);
        }

        return Ok(ruleset);
    }

    [HttpGet]
    public async Task<ActionResult> ValidateRuleset([FromBody] BloodTourney.Ruleset ruleset)
    {
        List<string> errors = await BloodTourney.Ruleset.ValidateRuleset(ruleset);

        if (errors.Any())
        {
            return new BadRequestObjectResult(errors);
        }

        return Ok();
    }

    [HttpPost]
    public async Task<JsonObject> CreateCustomRuleset([FromBody] BloodTourney.Ruleset ruleset)
    {
        var newRuleset = new BloodTourney.Ruleset.Builder()
            .WithTiers(ruleset.Tiers)
            .WithVictoryPoints(ruleset.MatchVictoryPoints)
            .WithTieBreakers(ruleset.TieBreakers)
            .WithSkillStacking(ruleset.Skillstacking)
            .WithTimeKeeping(ruleset.Timekeeping)
            .Build();
        
        return newRuleset;
    }
}