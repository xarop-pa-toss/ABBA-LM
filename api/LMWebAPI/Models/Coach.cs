using LMWebAPI.Data;
namespace LMWebAPI.Models;

public class Coach : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
    public DateTime? LastLogin { get; set; }

    // Navigation
    public ICollection<Team> Teams { get; set; } = new List<Team>();
}