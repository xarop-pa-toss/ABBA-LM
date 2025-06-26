using System.Text.Json;
using BloodTourney.Security;

namespace BloodTourney.Ruleset;

public interface IRulesetManager
{
    Models.Ruleset GetPresetRuleset(RulesetPresetType preset);
    byte[] SerializeAndEncrypt(Models.Ruleset ruleset);
    Models.Ruleset DecryptAndDeserialize(byte[] encryptedData);
}


public class RulesetManager : IRulesetManager
{
    private readonly IRulesetPresetFactory _presetFactory;

    public RulesetManager(IRulesetPresetFactory presetFactory)
    {
        _presetFactory = presetFactory ?? throw new ArgumentNullException(nameof(presetFactory));
    }

    public Models.Ruleset GetPresetRuleset(RulesetPresetType preset)
    {
        return _presetFactory.CreatePreset(preset);
    }

    public byte[] SerializeAndEncrypt(Models.Ruleset ruleset)
    {
        ArgumentNullException.ThrowIfNull(ruleset);
        
        var json = JsonSerializer.Serialize(ruleset);
        return Encryption.EncryptStringToFile(json);
    }

    public Models.Ruleset DecryptAndDeserialize(byte[] encryptedData)
    {
        ArgumentNullException.ThrowIfNull(encryptedData);
        
        var json = Encryption.DecryptFromFileToString(encryptedData);
        var ruleset = JsonSerializer.Deserialize<Models.Ruleset>(json);
        
        return ruleset ?? throw new InvalidOperationException("Failed to deserialize ruleset");
    }
}