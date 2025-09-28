using System;
using System.Collections.Generic;

namespace LMWebAPI.Models
{
    public class Positional
    {
        public Guid Id { get; set; }
        public Guid RosterId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int MA { get; set; }
        public int ST { get; set; }
        public int AG { get; set; }
        public int PA { get; set; }
        public int AV { get; set; }
        public bool IsThrall { get; set; }
        public bool IsUndead { get; set; }
        public bool IsSecretWeapon { get; set; }
        public bool IsStarPlayer { get; set; }
        public string[] PrimarySkillCategories { get; set; } = Array.Empty<string>();
        public string[] SecondarySkillCategories { get; set; } = Array.Empty<string>();

        // Navigation
        public ICollection<Player> Players { get; set; } = new List<Player>();
        public ICollection<PositionalRoster> PositionalRosters { get; set; } = new List<PositionalRoster>();
        public ICollection<PositionalSkill> PositionalSkills { get; set; } = new List<PositionalSkill>();
        
        // Meta
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}