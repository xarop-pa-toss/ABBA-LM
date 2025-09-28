using System;

namespace LMWebAPI.Models
{
    public class PlayerInjury
    {
        public Guid PlayerId { get; set; }
        public Guid InjuryId { get; set; }

        // Navigation
        public Player Player { get; set; } = null!;
        public Injury Injury { get; set; } = null!;
        
        // Meta
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}