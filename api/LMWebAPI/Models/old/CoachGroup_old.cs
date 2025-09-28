namespace LMWebAPI.Models;

public class TournamentCoachGroup
{
    public TournamentCoachGroup(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public List<CoachAndTeam> Teams { get; set; }

    internal async Task AddCoach(User_old userOld)
    {
    }

    public struct CoachAndTeam
    {
        private string CoachName;
        private Team Team;
    }
}