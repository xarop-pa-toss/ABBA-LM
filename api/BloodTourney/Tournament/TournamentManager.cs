using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using BloodTourney.Models;
using BloodTourney.Security;

namespace BloodTourney.Tournament;

public interface ITournamentManager
{
    ValidationResult ValidateTeams(Models.Ruleset tournament, IEnumerable<Team> teams);
    ValidationResult ValidateTournament(Models.Tournament tournament);
}

public class TournamentManager : ITournamentManager
{
    // Empty constructor to force use of builder
    private TournamentManager() { }

    /// <summary>
    /// Validate team against current configuration
    /// </summary>
    /// <param name="ruleset">Tournament ruleset object.</param>
    /// <param name="teams">List of teams to validate against chosen tournament ruleset.</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public ValidationResult ValidateTeams(Models.Ruleset TournamentRuleset , IEnumerable<Team> teams)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Check integrity of given .bloodtourney file
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public ValidationResult ValidateTournament(Models.Tournament tournament)
    {
        throw new NotImplementedException();
    }
    
    public byte[] SerializeAndEncrypt(Models.Tournament tournament)
    {
        ArgumentNullException.ThrowIfNull(tournament);
        
        var json = JsonSerializer.Serialize(tournament);
        return Encryption.EncryptStringToFile(json);
    }

    public Models.Tournament DecryptAndDeserialize(byte[] encryptedData)
    {
        ArgumentNullException.ThrowIfNull(encryptedData);
        
        var json = Encryption.DecryptFromFileToString(encryptedData);
        var tournament = JsonSerializer.Deserialize<Models.Tournament>(json);
        
        return tournament ?? throw new InvalidOperationException("Failed to deserialize tournament.");
    }
}