using BloodTourney;

namespace BloodTourney.Tournament;

public interface ITournament
{
    /// <summary>
    /// Get the current configuration of the tournament
    /// </summary>
    TournamentConfig Configuration { get; }

    /// <summary>
    /// Validates if a team is legal for this tournament
    /// </summary>
    (bool isValid, string error) ValidateTeam(Models.Team team);

    /// <summary>
    /// Returns true if the tournament can begin (enough players, etc.)
    /// </summary>
    (bool canStart, string reason) CanStartTournament();
}