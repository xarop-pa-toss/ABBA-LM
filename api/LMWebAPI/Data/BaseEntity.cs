namespace LMWebAPI.Data;

public class BaseEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public Guid CreatedByUserId { get; set; }

    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedByUserId { get; set; }

    public bool IsDeleted { get; set; }
}