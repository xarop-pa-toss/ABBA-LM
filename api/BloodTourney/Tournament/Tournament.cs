using System.ComponentModel.DataAnnotations;
using BloodTourney.Models;

namespace BloodTourney.Tournament;

public interface ITournament
{
    /// <summary>
    /// User ID of organiser.
    /// </summary>
    public string TournamentOrganizerId { get; init; }
    
    /// <summary>
    /// Display the name of the Tournament. Duplicate named tournaments are not advised.
    /// </summary>
    public string TournamentName { get; set; }
    public int PlayerLimit { get; set; }
    public DateOnly StartDate { get; set; }
    public TimeOnly StartTime { get; set; }
    
    /// <summary>
    /// Place where the tournament takes place (no geolocation involved).
    /// </summary>
    public string Location { get; set; }
    
    /// <summary>
    /// Get the current configuration of the tournament
    /// </summary>
    TournamentConfig GetConfiguration { get; }
    
    /// <summary>
    /// Set the configuration of the tournament
    /// </summary>
    TournamentConfig SetConfiguration { init; }
    
    /// <summary>
    /// Validates if a team is legal for this tournament
    /// </summary>
    Helpers.ValidationResult ValidateTeam(Models.Team team);

    /// <summary>
    /// Returns true if the tournament can begin (enough players, etc.)
    /// </summary>
    Helpers.ValidationResult CheckIntegrityOfTournamentFile();
}

public class Tournament : ITournament
{
    public string TournamentOrganizerId { get; init; }
    public string TournamentName { get; set; }
    public int PlayerLimit { get; set; }
    public DateOnly StartDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public string Location { get; set; }
    private TournamentConfig _configuration;
    
    // Empty constructor to force use of builder
    private Tournament() { }

    /// <summary>
    /// Validate team against current configuration
    /// </summary>
    /// <param name="team"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Helpers.ValidationResult ValidateTeam(Team team)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Check integrity of given .bloodtourney file
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Helpers.ValidationResult CheckIntegrityOfTournamentFile()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Get configuration for this tournament (ruleset and additional rules)
    /// </summary>
    public TournamentConfig GetConfiguration { get => _configuration; }
    /// <summary>
    /// Set configuration for this tournament (ruleset and additional rules)
    /// </summary>
    public TournamentConfig SetConfiguration { init => _configuration = value; }

    /// <summary>
    /// Builder for creating Tournament instances. Allows for creation of custom rulesets.
    /// Use WithExistingRuleset to send in a BloodTourney.Ruleset (enum) choice, ConfigureRuleset to use Ruleset.Builder, or FromFile to import a previously created Ruleset..
    /// </summary>
    public class RulesetBuilder
    {
        private Ruleset.Builder? _rulesetBuilder;
        private Ruleset? _ruleset;
        private TournamentFormats _format;
        private int _teamValueLimit;
        private bool _firstRoundRandomSort = true;
        private bool _unspentCashConvertedToPrayers;
        private bool _resurrectionMode = true;
        /// <summary>
        /// Sets the tournament format
        /// </summary>
        public RulesetBuilder WithFormat(TournamentFormats format)
        {
            _format = format;
            return this;
        }

        /// <summary>
        /// Sets the team value limit
        /// </summary>
        public RulesetBuilder WithTeamValueLimit(int limit)
        {
            _teamValueLimit = limit;
            return this;
        }

        /// <summary>
        /// Sets whether the first round should be randomly sorted
        /// </summary>
        public RulesetBuilder WithFirstRoundRandomSort(bool randomSort = true)
        {
            _firstRoundRandomSort = randomSort;
            return this;
        }

        /// <summary>
        /// Sets whether unspent cash should be converted to Prayers to Nuffle or lost entirely
        /// </summary>
        public RulesetBuilder WithUnspentCashConvertedToPrayers(bool convert = true)
        {
            _unspentCashConvertedToPrayers = convert;
            return this;
        }

        /// <summary>
        /// Sets whether resurrection mode is enabled.
        /// Resurrection mode means that injury or death suffered by a player will be cleared after each match, and each coach will start their matches with the registered rosters. 
        /// </summary>
        public RulesetBuilder WithResurrectionMode(bool resurrect = true)
        {
            _resurrectionMode = resurrect;
            return this;
        }

        /// <summary>
        /// Uses a default ruleset for the tournament. Takes a BloodTourney.Ruleset enum object as parameter.
        /// </summary>
        public RulesetBuilder WithExistingRuleset(Ruleset ruleset)
        {
            _ruleset = ruleset;
            _rulesetBuilder = null;
            return this;
        }

        /// <summary>
        /// Starts configuring a new ruleset for the tournament with the Ruleset.Builder.
        /// </summary>
        /// <returns>A Ruleset.Builder for configuring the ruleset</returns>
        public Ruleset.Builder BuildRuleset()
        {
            _rulesetBuilder = Ruleset.Builder.Create();
            return _rulesetBuilder;
        }

        /// <summary>
        /// TODO: Implement custom ruleset file opening and decryption
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Ruleset WithRulesetFromFile(string path)
        {
            throw new NotImplementedException();
            _ruleset = null;
            return _ruleset;
        }

        /// <summary>
        /// Creates a new tournament instance
        /// </summary>
        public Tournament Build()
        {
            if (_ruleset == null && _rulesetBuilder == null)
            {
                throw new InvalidOperationException("Either configure a ruleset or provide an existing one");
            }

            var config = new TournamentConfig
            {
                Ruleset = _ruleset ?? _rulesetBuilder!.Build(),
                TournamentFormat = _format,
                FirstRoundRandomSort = _firstRoundRandomSort,
                UnspentCashConvertedToPrayers = _unspentCashConvertedToPrayers,
                ResurrectionMode = _resurrectionMode
            };

            return new Tournament
            {
                _configuration = config
            };
        }

        /// <summary>
        /// Creates a new Tournament builder
        /// </summary>
        public static RulesetBuilder CreateBuilder() => new RulesetBuilder();
    }
}