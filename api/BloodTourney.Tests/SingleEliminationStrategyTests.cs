using BloodTourney.Tournament;
using BloodTourney.Tournament.Formats;

namespace BloodTourney.Tests
{
    public class SingleEliminationStrategyTests
    {
        private readonly SingleEliminationStrategy _strategy;

        public SingleEliminationStrategyTests()
        {
            _strategy = new SingleEliminationStrategy();
        }

        [Fact]
        public void CreateFirstRoundRandom_WithTwoTeams_ReturnsOneMatch()
        {
            // Arrange
            var teams = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            // Act
            var result = ((ITournamentFormat)_strategy).CreateFirstRoundRandom(teams);
            var matches = result.ToList();

            // Assert
            Assert.Single(matches);
            Assert.NotNull(matches[0].TeamA);
            Assert.NotNull(matches[0].TeamB);
            Assert.Contains(matches[0].TeamA.Value, teams);
            Assert.Contains(matches[0].TeamB.Value, teams);
            Assert.NotEqual(matches[0].TeamA, matches[0].TeamB);
        }

        [Fact]
        public void CreateFirstRoundRandom_WithThreeTeams_ReturnsTwoMatchesWithOneBye()
        {
            // Arrange
            var teams = new List<Guid> 
            { 
                Guid.NewGuid(), 
                Guid.NewGuid(), 
                Guid.NewGuid() 
            };

            // Act
            var result = ((ITournamentFormat)_strategy).CreateFirstRoundRandom(teams);
            var matches = result.ToList();

            // Assert
            Assert.Equal(2, matches.Count);
            
            // One match should have both teams
            var fullMatch = matches.FirstOrDefault(m => m.TeamA.HasValue && m.TeamB.HasValue);
            Assert.NotNull(fullMatch);
            
            // One match should be a bye (TeamB is null)
            var byeMatch = matches.FirstOrDefault(m => m.TeamA.HasValue && !m.TeamB.HasValue);
            Assert.NotNull(byeMatch);

            // All team GUIDs should be unique
            var usedTeams = matches
                .SelectMany(m => new[] { m.TeamA, m.TeamB })
                .Where(t => t.HasValue)
                .Select(t => t.Value)
                .ToList();
            Assert.Equal(3, usedTeams.Distinct().Count());
        }

        [Fact]
        public void CreateFirstRoundRandom_WithFourTeams_ReturnsTwoFullMatches()
        {
            // Arrange
            var teams = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            // Act
            var result = ((ITournamentFormat)_strategy).CreateFirstRoundRandom(teams);
            var matches = result.ToList();

            // Assert
            Assert.Equal(2, matches.Count);
            Assert.All(matches, match =>
            {
                Assert.NotNull(match.TeamA);
                Assert.NotNull(match.TeamB);
                Assert.NotEqual(match.TeamA, match.TeamB);
            });

            // All teams should be used exactly once
            var usedTeams = matches
                .SelectMany(m => new[] { m.TeamA.Value, m.TeamB.Value })
                .ToList();
            Assert.Equal(4, usedTeams.Distinct().Count());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void CreateFirstRoundRandom_WithInvalidTeamCount_ThrowsArgumentOutOfRangeException(int teamCount)
        {
            // Arrange
            var teams = Enumerable.Range(0, teamCount).Select(_ => Guid.NewGuid());

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => 
                ((ITournamentFormat)_strategy).CreateFirstRoundRandom(teams));
        }

        [Fact]
        public void CreateFirstRoundRandom_WithNullInput_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                ((ITournamentFormat)_strategy).CreateFirstRoundRandom(null));
        }

        [Fact]
        public void CreateFirstRoundRandom_EnsuresRandomization()
        {
            // Arrange
            var teams = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            // Act
            var results = new List<List<MatchNode>>();
            for (int i = 0; i < 10; i++)
            {
                var result = ((ITournamentFormat)_strategy).CreateFirstRoundRandom(teams);
                results.Add(result.ToList());
            }

            // Assert
            // Check if we have at least two different arrangements
            // Note: There's a very small chance this test could fail even with correct randomization
            var differentArrangements = results
                .Select(r => string.Join(",", r.Select(m => $"{m.TeamA},{m.TeamB}")))
                .Distinct()
                .Count();
            Assert.True(differentArrangements > 1, 
                "Multiple runs should produce different team arrangements");
        }
    }
}