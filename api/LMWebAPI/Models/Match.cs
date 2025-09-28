using LMWebAPI.Data;
namespace LMWebAPI.Models;

public class Match : BaseEntity
{
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
    public DateTime ScheduledTo { get; set; }
    public DateTime PlayedAt { get; set; }

    // Navigation
}