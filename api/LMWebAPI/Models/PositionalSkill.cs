using System;

namespace LMWebAPI.Models
{
    public class PositionalSkill
    {
        public Guid PositionalId { get; set; }
        public Guid SkillId { get; set; }
        public int Cost { get; set; }

        // Navigation
        public Positional Positional { get; set; } = null!;
        public Skill Skill { get; set; } = null!;
        
        // Meta
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}