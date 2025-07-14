using BT = BloodTourney;
using LMWebAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
namespace LMWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RulesetController(BT.Ruleset.RulesetManager rulesetManager) : ControllerBase
{
    /// <summary>
    /// Get list of all Preset Rulesets names.
    /// </summary>
    /// <returns>List of strings with preset ruleset names.</returns>
    [HttpGet("presets")]
    public ActionResult GetAllPresetsNames()
    {
        List<string> rulesetList = Enum.GetNames(typeof(BT.Ruleset.RulesetPresets)).ToList();
        
        return rulesetList.Count > 0 
            ? Ok(rulesetList) 
            : NotFound("No preset rulesets exist.");
    }

    [HttpGet("presets/{presetName}")]
    public ActionResult<RulesetDTO> GetPreset(string presetName)
    {
        if (!Enum.TryParse(presetName, ignoreCase: true, out BT.Ruleset.RulesetPresets rulesetPreset))
            return NotFound("Preset ruleset not found.");
        
        return Ok(rulesetManager.GetPresetRuleset(rulesetPreset));
    }

    [HttpGet("/validate")]
    public ActionResult ValidateRuleset([FromBody] RulesetDTO rulesetDto)
    {
        throw new NotImplementedException("Ruleset validation not implemented yet.");
    }
    
    [HttpPost("/create")]
    public ActionResult CreateCustomRuleset([FromBody] RulesetDTO rulesetDto)
    {
        throw new NotImplementedException("Ruleset creation not implemented yet.");
    }
}