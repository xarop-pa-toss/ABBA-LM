using LMWebAPI.Data;
namespace LMWebAPI.Models;

public class PositionalRoster : BaseEntity
{
    public Guid PositionalId { get; set; }
    public Guid RosterId { get; set; }
    public int LimitMax { get; set; }
    public int Cost { get; set; }

    // Navigation
    public Positional Positional { get; set; } = null!;
    public Roster Roster { get; set; } = null!;
}