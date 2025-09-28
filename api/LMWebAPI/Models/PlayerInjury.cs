using LMWebAPI.Data;
namespace LMWebAPI.Models;

public class PlayerInjury : BaseJunctionEntity
{
    public Guid PlayerId { get; set; }
    public Guid InjuryId { get; set; }

    // Navigation
    public Player Player { get; set; } = null!;
    public Injury Injury { get; set; } = null!;
}