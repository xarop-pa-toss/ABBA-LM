using BloodTourney.Models;

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

    public TournamentConfig Configuration { get; }
    public (bool isValid, string error) ValidateTeam(Team team)
    {
        throw new NotImplementedException();
    }

    public (bool canStart, string reason) CanStartTournament()
    {
        throw new NotImplementedException();
    }
}