namespace BloodTourney.Tournament;

public class Tournament : ITournament
{
    public enum TournamentFormats
    {
        RoundRobin,
        Swiss,
        SingleElimination,
        DoubleElimination,
        KingOfTheHill
    }
}