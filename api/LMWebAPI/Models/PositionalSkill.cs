using LMWebAPI.Data;
namespace LMWebAPI.Models;

public class PositionalSkill : BaseJunctionEntity
{
    public Guid PositionalId { get; set; }
    public Guid SkillId { get; set; }
    public int Cost { get; set; }

    // Navigation
    public Positional Positional { get; set; } = null!;
    public Skill Skill { get; set; } = null!;
}