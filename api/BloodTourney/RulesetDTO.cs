using BloodTourney;

namespace LMWebAPI.Models.DTOs;

public class RulesetDTO
{
    public IEnumerable<Core.TierParameters> Tiers { get; set; }
    public Core.VictoryPoints VictoryPoints { get; set; }
    public IEnumerable<string> TieBreakers { get; set; }
    public Core.TimeKeeping Timekeeping { get; set; }
    public Core.SkillStacking Skillstacking { get; set; }
    public IEnumerable<Core.Inducement> Inducements { get; set; }
    public IEnumerable<string> BannedStarPlayers { get; set; }
    public IEnumerable<string> Guidelines { get; set; }
    public Core.OtherRules OtherRules { get; set; }
}