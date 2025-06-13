using BloodTourney.Tournament;
using BloodTourney.Tournament.Formats;
using System.Diagnostics;
using Xunit.Abstractions;

namespace BloodTourney.Tests
{
    public class SingleEliminationStrategyTests
    {
        private readonly SingleEliminationStrategy _strategy;
        private readonly ITestOutputHelper _output;

        public SingleEliminationStrategyTests(ITestOutputHelper output)
        {
            _strategy = new SingleEliminationStrategy();
            _output = output;
        }

        [Fact]
        public void CreateFirstRoundRandom_WithTwoTeams_ReturnsOneMatch()
        {
            // Arrange
            var team1 = Guid.NewGuid();
            var team2 = Guid.NewGuid();
            var teams = new List<Guid> { team1, team2 };
            var teamNames = new Dictionary<Guid, string>
            {
                { team1, "Red Rockets" },
                { team2, "Blue Bombers" }
            };

            // Act
            var result = ((ITournamentFormat)_strategy).CreateFirstRoundRandom(teams);
            var matches = result.ToList();

            // Output tournament visualization
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(matches, teamNames));

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
            var team1 = Guid.NewGuid();
            var team2 = Guid.NewGuid();
            var team3 = Guid.NewGuid();
            var teams = new List<Guid> { team1, team2, team3 };
            var teamNames = new Dictionary<Guid, string>
            {
                { team1, "Dwarven Diggers" },
                { team2, "Elven Archers" },
                { team3, "Orc Crushers" }
            };

            // Act
            var result = ((ITournamentFormat)_strategy).CreateFirstRoundRandom(teams);
            var matches = result.ToList();

            // Output tournament visualization
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(matches, teamNames));

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
            var team1 = Guid.NewGuid();
            var team2 = Guid.NewGuid();
            var team3 = Guid.NewGuid();
            var team4 = Guid.NewGuid();
            var teams = new List<Guid> { team1, team2, team3, team4 };
            var teamNames = new Dictionary<Guid, string>
            {
                { team1, "Skaven Sneakers" },
                { team2, "Human Heroes" },
                { team3, "Chaos Chosen" },
                { team4, "Undead Undertakers" }
            };

            // Act
            var result = ((ITournamentFormat)_strategy).CreateFirstRoundRandom(teams);
            var matches = result.ToList();

            // Output tournament visualization
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(matches, teamNames));

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