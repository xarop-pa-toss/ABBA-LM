using System;
using System.Collections.Generic;

namespace LMWebAPI.Models
{
    public class Roster
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int DedicatedFansCost { get; set; }
        public int AssistantCoachCost { get; set; }
        public int CheerleaderCost { get; set; }
        public int RerollCost { get; set; }
        public bool HasApothecary { get; set; }
        public int ApothecaryCost { get; set; }
        public bool HasNecromancer { get; set; }
        public int MaxBigGuys { get; set; }
        public string? Description { get; set; }
        public string? Pros { get; set; }
        public string? Cons { get; set; }

        // Navigation
        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public ICollection<PositionalRoster> PositionalRosters { get; set; } = new List<PositionalRoster>();
        
        // Meta
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}