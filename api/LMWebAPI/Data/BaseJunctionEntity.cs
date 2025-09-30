namespace LMWebAPI.Data;

public abstract class BaseJunctionEntity
{
    public DateTime CreatedAt { get; set; } 
    public Guid CreatedByUserId { get; set; }

    public bool IsDeleted { get; set; }
}