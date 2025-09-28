using LMWebAPI.Data;
using LMWebAPI.Models;

public class Skill : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsSpecialTrait { get; set; } = false;

    // Navigation properties
    public ICollection<PlayerSkill> PlayerSkills { get; set; } = new List<PlayerSkill>();
    public ICollection<PositionalSkill> PositionalSkills { get; set; } = new List<PositionalSkill>();
}