namespace LMWebAPI.Models.DTOs;

public class TeamWithPlayersDto
{
    public Team Team { get; set; }
    private List<Player> Players { get; set; }
}