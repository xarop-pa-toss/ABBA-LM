namespace BloodTourney.Tournament;

public class TournamentConfig
{
    /// <summary>
    /// Game rules to be used. Rulesets can be retrieved by using the Ruleset.GetPresetRuleset or Ruleset.Builder.Create methods which allows custom rulesets.
    /// Rulesets can be downloaded as encrypted .ruleset files using Ruleset.EncryptRulesetIntoFile and decrypted with Ruleset.DecryptRulesetFile.
    /// </summary>
    public required Ruleset Ruleset { get; init; }

    /// <summary>
    /// Defines the round format for the tournament.
    /// Some formats might be non-eliminatory such as Round Robin, while others will cause the team to not proceed if they lose.
    /// </summary>
    public required TournamentFormats TournamentFormat { get; init; }
    
    /// <summary>
    /// If false, the first round will be randomized as per the chosen format in TournamentFormat
    /// </summary>
    public required bool FirstRoundRandomSort { get; init; }

    /// <summary>
    /// If false, all excess cash leftover from the team creation process will be lost.
    /// Otherwise, it is converted into Prayers To Nuffle.
    /// </summary>
    public bool UnspentCashConvertedToPrayers { get; init; } = false;

    /// <summary>
    /// If true, any injury or death suffered by a player will be cleared after each match, and each coach will start their matches with the registered rosters.
    /// </summary>
    public bool ResurrectionMode { get; init; } = true;
}

public enum TournamentFormats
{
    RoundRobin,
    Swiss,
    SingleElimination,
    DoubleElimination,
    KingOfTheHill
}