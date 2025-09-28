namespace LMWebAPI.Models;

public class League : Competition
{
    public ICollection<Coach> Coaches { get; set; } = new List<Coach>();
    public ICollection<Team> Teams { get; set; } = new List<Team>();
}