using LMWebAPI.Data;
using LMWebAPI.Models;

public class PlayerSkill : BaseEntity
{
    public Guid PlayerId { get; set; }
    public Guid SkillId { get; set; }

    public string Value { get; set; } = null!;

    // Navigation
    public Player Player { get; set; } = null!;
    public Skill Skill { get; set; } = null!;
}