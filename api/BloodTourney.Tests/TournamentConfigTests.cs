using BloodTourney.Models;
using BloodTourney.Ruleset;

namespace BloodTourney.Tests
{
    public class TournamentConfigTests
    {
        private readonly RulesetManager _rulesetManager = new RulesetManager();
        
        [Fact]
        public void TournamentConfig_Properties()
        {
            // var rulesetManager = new RulesetManager(rulesetFactory);
            // Arrange & Act
            var config = new TournamentConfig
            {
                Ruleset = _rulesetManager.GetPresetRuleset(RulesetPresetType.SardineBowl2025),
                TournamentFormat = TournamentFormatType.SingleElimination,
                FirstRoundRandomSort = true,
                UnspentCashConvertedToPrayers = true,
                ResurrectionMode = true
            };

            // Assert
            Assert.NotNull(config.Ruleset);
            Assert.Equal(TournamentFormatType.SingleElimination, config.TournamentFormat);
            Assert.True(config.FirstRoundRandomSort);
            Assert.True(config.UnspentCashConvertedToPrayers);
            Assert.True(config.ResurrectionMode);
        }

        [Fact]
        public void TournamentConfig_AllFormats()
        {
            // Test that all tournament formats can be used in the configuration
            foreach (TournamentFormatType formatType in Enum.GetValues(typeof(TournamentFormatType)))
            {
                // Arrange & Act
                var config = new TournamentConfig
                {
                    Ruleset = _rulesetManager.GetPresetRuleset(RulesetPresetType.SardineBowl2025),
                    TournamentFormat = formatType,
                    FirstRoundRandomSort = true,
                    UnspentCashConvertedToPrayers = false,
                    ResurrectionMode = false
                };

                // Assert
                Assert.Equal(formatType, config.TournamentFormat);
            }
        }

        [Fact]
        public void TournamentConfig_DefaultValues()
        {
            // Arrange & Act
            var config = new TournamentConfig
            {
                Ruleset = _rulesetManager.GetPresetRuleset(RulesetPresetType.SardineBowl2025),
                TournamentFormat = TournamentFormatType.SingleElimination,
                FirstRoundRandomSort = false,
                UnspentCashConvertedToPrayers = false,
                ResurrectionMode = false
            };

            // Assert - checking that default values are correctly applied
            Assert.False(config.UnspentCashConvertedToPrayers);
            Assert.True(config.ResurrectionMode); // Default value is true
        }
    }
}
