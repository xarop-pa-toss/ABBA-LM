namespace LMWebAPI.Models.DTOs;

public class TeamWithPlayersDto
{
    public Team Team { get; set; }
    List<Player> Players { get; set; }
}