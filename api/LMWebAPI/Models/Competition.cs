using LMWebAPI.Data;
namespace LMWebAPI.Models;

public class Competition : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid[] OrganizerIds { get; set; } = Array.Empty<Guid>();

    // Navigation
    public ICollection<Match> Matches { get; set; } = new List<Match>();
}