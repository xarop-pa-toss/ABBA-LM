using System;
using System.Collections.Generic;

namespace LMWebAPI.Models
{
    public class League
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Coach> Coaches { get; set; } = new List<Coach>();
        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}