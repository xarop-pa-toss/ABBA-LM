using LMWebAPI.Data;
namespace LMWebAPI.Models;

public class Injury : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? AffectedStat { get; set; }
    public int Modifier { get; set; }

    // Navigation
    public ICollection<PlayerInjury> PlayerInjuries { get; set; } = new List<PlayerInjury>();
}