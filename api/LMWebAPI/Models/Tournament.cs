using System;

namespace LMWebAPI.Models
{
    public class Tournament
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Meta
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}