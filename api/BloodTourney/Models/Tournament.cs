namespace BloodTourney.Models;

public class Tournament
{
    public required string OrganizerId { get; init; }
    public required string TournamentName { get; set; }
    public required uint PlayerLimit { get; set; }
    public required TournamentConfig Configuration { get; set; }
    public DateOnly? StartDate { get; set; }
    public TimeOnly? StartTime { get; set; }
    public DateOnly? EndDate { get; set; }
    public TimeOnly? EndTime { get; set; }
    public string Location { get; set; } = string.Empty;
}