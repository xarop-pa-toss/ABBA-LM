namespace LMWebAPI.Data;

public class BaseJunctionEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid CreatedByUserId { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Guid UpdatedByUserId { get; set; }

    public bool IsDeleted { get; set; }
}