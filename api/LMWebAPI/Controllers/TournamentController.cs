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
        (var ruleset, string err) = await BloodTourney.Rulesets.GetRuleset(presetRulesetName);

        if (!string.IsNullOrWhiteSpace(err))
        {
            return new NotFoundObjectResult("Could not get requested Ruleset: " + err);
        }

        return Ok(ruleset);
    }

    [HttpGet]
    public async Task<ActionResult> ValidateRuleset([FromBody] BloodTourney.Ruleset ruleset)
    {
        List<string> errors = await BloodTourney.Rulesets.ValidateRuleset(ruleset);

        if (errors.Any())
        {
            return new BadRequestObjectResult(errors);
        }

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> CreateCustomRuleset([FromBody] BloodTourney.RulesetDTO ruleset)
    {
        var builder = new BloodTourney.Core.Rulesets.Builder()
            .WithTiers(ruleset.Tiers)
            .WithVictoryPoints(ruleset.VictoryPoints)
            .WithTieBreakers(ruleset.TieBreakers)
            .WithSkillStacking(ruleset.Skillstacking)
            .WithTimeKeeping(ruleset.Timekeeping)
            .Build();
    }
}