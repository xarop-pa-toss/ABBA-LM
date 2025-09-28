using System.Text.Json;
using BloodTourney.Ruleset;
using LMWebAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
namespace LMWebAPI.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class TournamentController(RulesetManager rulesetManager) : ControllerBase
{
    [HttpGet]
    public ActionResult GetPresetRuleset([FromBody] string presetRulesetName)
    {
        throw new NotImplementedException("");
        // (var ruleset, string err) = rulesetManager.GetPresetRuleset(RulesetPresets.SardineBowl2025);
        //
        // if (!string.IsNullOrWhiteSpace(err))    
        // {
        //     return new NotFoundObjectResult("Could not get requested Ruleset: " + err);
        // }
        //
        // return Ok(ruleset);
    }

    [HttpGet]
    public async Task<ActionResult> ValidateRuleset([FromBody] RulesetDTO rulesetDto)
    {
        throw new NotImplementedException("");
        // List<string> errors = await BT.Ruleset.ValidateIntegrity(ruleset);
        //
        // if (errors.Any())
        // {
        //     return new BadRequestObjectResult(errors);
        // }
        //
        // return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<string>> CreateCustomRuleset([FromBody] RulesetDTO rulesetDto)
    {
        var newRuleset = new RulesetBuilder()
            .WithTiers(rulesetDto.Tiers)
            .WithVictoryPoints(rulesetDto.MatchVictoryPoints)
            .WithTieBreakers(rulesetDto.TieBreakers)
            .WithSkillStacking(rulesetDto.Skillstacking)
            .WithTimeKeeping(rulesetDto.Timekeeping)
            .Build();

        var serializedRuleset = JsonSerializer.Serialize(newRuleset);
        return !string.IsNullOrEmpty(serializedRuleset)
            ? Ok(serializedRuleset)
            : BadRequest("Could not serialize ruleset.");
    }
}