using LMWebAPI.Data;
using LMWebAPI.Models.Enums;
namespace LMWebAPI.Models;

public class Match : BaseEntity
{
    public Guid Id { get; set; }
    
    public Guid CompetitionId { get; set; }
    public Competition Competition { get; set; } = null!;

    public Guid HomeTeamId { get; set; }
    public Team HomeTeam { get; set; } = null!;
    public Guid AwayTeamId { get; set; }
    public Team AwayTeam { get; set; } = null!;

    // Round info. Ignored is Match is of Exhibition type
    public int? RoundNumber { get; set; }

    // Stats
    public bool IsFinished { get; set; }
    public MatchResult? MatchResults { get; set; }

    // Scheduling
    public DateTime ScheduledTo { get; set; } = DateTime.UtcNow;
    public DateTime PlayedAt { get; set; } = DateTime.UtcNow;

    // Navigation
}