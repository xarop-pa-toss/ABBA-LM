namespace LMWebAPI.Models;

public class LeagueRound : Round
{
    public Guid LeagueId { get; set; }
    public League League { get; set; } = null!;
}