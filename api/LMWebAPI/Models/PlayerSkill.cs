using LMWebAPI.Models;

public class PlayerSkill
{
    public Guid PlayerId { get; set; }
    public Guid SkillId { get; set; }
    
    public string Value { get; set; } = null!;
    
    // Navigation
    public Player Player { get; set; } = null!;
    public Skill Skill { get; set; } = null!;
    
    // Meta
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}