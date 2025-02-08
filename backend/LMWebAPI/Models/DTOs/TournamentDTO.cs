namespace LMWebAPI.Models.DTOs;

public class TournamentDTO
{
    public required string Id { get; set; }
    public required string CreatorUserId { get; set; }
    public required DateTime CreationDate { get; set; }
    public required DateTime StartDate { get; set; }
    public string Name {get; set;} = "Unnamed Tournament";
    public int TeamRatingMax { get; set; } = 0;
    public int NumberOfTeams { get; set; } = 0;
    public List<TournamentCoachDTO> RegisteredCoaches { get; set; } = new();
}