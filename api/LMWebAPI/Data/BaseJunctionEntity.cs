namespace LMWebAPI.Data;

public class BaseJunctionEntity
{
    public DateTime CreatedAt { get; set; } 
    public Guid CreatedByUserId { get; set; }

    public bool IsDeleted { get; set; }
}