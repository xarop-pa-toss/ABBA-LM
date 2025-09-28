using System;

namespace LMWebAPI.Models
{
    public class PositionalRoster
    {
        public Guid PositionalId { get; set; }
        public Guid RosterId { get; set; }
        public int LimitMax { get; set; }
        public int Cost { get; set; }

        // Navigation
        public Positional Positional { get; set; } = null!;
        public Roster Roster { get; set; } = null!;
        
        // Meta
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}