namespace BloodTourney.Core;

public static class Validation  
{
    public struct BaseParameters
    {
        public string TournamentOrganizerId { get; set; }
        public string TournamentName { get; set;}
        public int PlayerLimit { get; set;}
        public int TeamValueLimit { get; set;}
        public DateTime StartDate { get; set;}
        public string Location { get; set;}
        public bool IsInvitationOnly { get; set;}
    }
    
    public static async Task<(BaseParameters baseParameters, string err)> ValidateTournamentParams(BaseParameters tbd)
    {
        string err = String.Empty;
        
        if (tbd.PlayerLimit < 2) {err = "Player limit must be greater than 2.";}
        if (tbd.TeamValueLimit < 0) {err = "Team limit must be greater than 0.";}
        if (tbd.StartDate < DateTime.UtcNow) {err = "Start date must be today or in the future.";};

        return (tbd, err);
    }
}