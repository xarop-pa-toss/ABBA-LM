namespace LMWebAPI.Models.DTOs;

public class TournamentCoachDTO
{
    public required string Id { get; set; } = string.Empty;
    public required string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    // NAF Details from User model
    public string NafNickname { get; set; } = string.Empty;
    public int NafNumber { get; set; } = 0;
    
    // Team for this Coach in this Tournament
    public TournamentTeamDTO Team { get; set; }
}