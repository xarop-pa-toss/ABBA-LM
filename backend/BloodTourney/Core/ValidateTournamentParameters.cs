namespace BloodTourney.Core;

public static class ValidateTournamentParameters
{
    static ValidateTournamentParameters()
    {
    }
    
    public struct TournamentBaseData
    {
        public string TournamentOrganizerId { get; set; }
        public string TournamentName { get; set;}
        public int PlayerLimit { get; set;}
        public int TeamValueLimit { get; set;}
        public DateTime StartDate { get; set;}
        public string Location { get; set;}
        public bool IsInvitationOnly { get; set;}
    }
    
    public static async Task<TournamentBaseData> ValidateParameters(TournamentBaseData tbd)
    {
        if (tbd.PlayerLimit < 2) {throw new ArgumentException("Player limit must be greater than 2.");}
        if (tbd.TeamValueLimit < 0) {throw new ArgumentException("Team limit must be greater than 0.");}
        if (tbd.StartDate < DateTime.UtcNow) {throw new ArgumentException("Start date must be today or in the future.");} ;

        return tbd;
    }
}