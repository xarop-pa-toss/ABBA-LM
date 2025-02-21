namespace LMWebAPI.Models;

public class TournamentCoachGroup
{
    public TournamentCoachGroup(string name)
    {
        Name = name;
    }

    internal async Task AddCoach(User user)
    {
        
    }
    
    public struct CoachAndTeam
    {
        private string CoachName;
        private Team Team;
    }
    
    public string Name { get; set; }
    public List<CoachAndTeam> Teams { get; set; }
    
    
}