namespace LMWebAPI.Models;

public class TournamentRound : Round
{
    public Guid TournamentId { get; set; }
    public Tournament Tournament { get; set; }
}