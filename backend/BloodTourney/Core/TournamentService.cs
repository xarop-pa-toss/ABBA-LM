using MongoDB.Driver;

namespace BloodTourney.Core;

public class TournamentService
{
    public TournamentService()
    {
        
    }
    
    public struct TournamentBaseData
    {
        public string TournamentName { get; set;}
        public int PlayerLimit { get; set;}
        public int TeamValueLimit { get; set;}
        public DateTime StartDate { get; set;}
        public string Location { get; set;}
        public bool IsInvitationOnly { get; set;}
    }
    
    public async Task<TournamentBaseData> CreateTournament(
        string tournamentName,
        int playerLimit,
        int teamValueLimit,
        DateTime startDate,
        string location,
        bool isInvitationOnly)
    {
        
        if (playerLimit < 2) {throw new ArgumentException("Player limit must be greater than 2.");}
        if (teamValueLimit < 0) {throw new ArgumentException("Team limit must be greater than 0.");}
        if (startDate < DateTime.Today) {throw new ArgumentException("Start date must be today or in the future.");}

        TournamentBaseData tournament = new TournamentBaseData()
        {
            TournamentName = tournamentName,
            PlayerLimit = playerLimit,
            TeamValueLimit = teamValueLimit,
            StartDate = startDate,
            Location = location,
            IsInvitationOnly = isInvitationOnly
        };
        return tournament;
    }
    
    private void PropertyValidator() 
}