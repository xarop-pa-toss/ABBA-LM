namespace LMWebAPI.Models.DTOs;

public class PlayerDto
{
    public string Id { get; set; }
    public string TeamId { get; set; }
    public string Name { get; set; }
    public PlayerPositionEnum Position { get; set; }
    public PlayerStats Stats { get; set; }
    public List<string> Skills { get; set; }
    public int SppAvailable { get; set; }
    public int SppSpent { get; set; }
    public string Rank { get; set; }
    public int Value { get; set; }
    public int Number { get; set; }
    public List<Injury_old> Injuries { get; set; }
}