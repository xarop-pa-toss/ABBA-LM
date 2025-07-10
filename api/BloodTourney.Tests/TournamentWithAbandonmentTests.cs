using BloodTourney.Tournament.Formats;
using Xunit.Abstractions;

namespace BloodTourney.Tests
{
    public class TournamentWithAbandonmentTests(ITestOutputHelper output)
    {
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public void SingleElimination_WithTeamAbandonment()
        {
            // Arrange
            ITournamentFormat strategy = new SingleEliminationStrategy();
            var teams = Enumerable.Range(0, 8).Select(_ => Guid.NewGuid()).ToList();
            var teamNames = teams.ToDictionary(id => id, id => $"Team {teams.IndexOf(id) + 1}");

            // Act - Create first round
            var firstRound = strategy.CreateFirstRoundRandom(teams).ToList();
            _output.WriteLine("First Round:");
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(firstRound, teamNames));

            // Simulate a team abandonment in first match
            var abandonedTeam = firstRound[0].TeamB.Value;
            firstRound[0].TeamBAbandoned = true;
            firstRound[0].Winner = firstRound[0].TeamA.Value; // Team A wins by default
            firstRound[0].Loser = abandonedTeam;

            // Complete other matches normally
            for (int i = 1; i < firstRound.Count; i++)
            {
                firstRound[i].Winner = firstRound[i].TeamA.Value;
                firstRound[i].Loser = firstRound[i].TeamB.Value;
            }

            // Create list of teams that abandoned
            var abandonedTeams = new List<Guid> { abandonedTeam };

            // Act - Create second round with abandoned teams
            var secondRound = strategy.CreateNextRound(firstRound, abandonedTeams).ToList();
            _output.WriteLine("\nSecond Round (after abandonment):");
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(secondRound, teamNames));

            // Assert
            Assert.Equal(2, secondRound.Count);

            // Complete second round
            foreach (var match in secondRound)
            {
                match.Winner = match.TeamA.Value;
                match.Loser = match.TeamB.Value;
            }

            // Act - Create final round
            var finalRound = strategy.CreateNextRound(secondRound).ToList();
            _output.WriteLine("\nFinal Round:");
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(finalRound, teamNames));

            // Assert
            Assert.Single(finalRound);

            // Complete the tournament
            finalRound[0].Winner = finalRound[0].TeamA.Value;
            finalRound[0].Loser = finalRound[0].TeamB.Value;

            // Visualize complete bracket
            var allRounds = new List<List<MatchNode>> { firstRound, secondRound, finalRound };
            _output.WriteLine("\nComplete Tournament Bracket:");
            _output.WriteLine(TournamentTestHelpers.VisualizeCompleteBracket(allRounds.ToArray(), teamNames));
        }

        [Fact]
        public void Tournament_WithMultipleAbandonments()
        {
            // Arrange
            ITournamentFormat strategy = new SingleEliminationStrategy();
            var teams = Enumerable.Range(0, 16).Select(_ => Guid.NewGuid()).ToList();
            var teamNames = teams.ToDictionary(id => id, id => $"Team {teams.IndexOf(id) + 1}");

            // Act - Create first round
            var firstRound = strategy.CreateFirstRoundRandom(teams).ToList();

            // Simulate two teams abandoning
            var abandonedTeam1 = firstRound[0].TeamB.Value;
            var abandonedTeam2 = firstRound[1].TeamA.Value;

            firstRound[0].TeamBAbandoned = true;
            firstRound[0].Winner = firstRound[0].TeamA.Value;
            firstRound[0].Loser = abandonedTeam1;

            firstRound[1].TeamAAbandoned = true;
            firstRound[1].Winner = firstRound[1].TeamB.Value;
            firstRound[1].Loser = abandonedTeam2;

            // Complete other matches normally
            for (int i = 2; i < firstRound.Count; i++)
            {
                firstRound[i].Winner = firstRound[i].TeamA.Value;
                firstRound[i].Loser = firstRound[i].TeamB.Value;
            }

            // List of abandoned teams
            var abandonedTeams = new List<Guid> { abandonedTeam1, abandonedTeam2 };

            // Create second round
            var secondRound = strategy.CreateNextRound(firstRound, abandonedTeams).ToList();

            // Assert
            Assert.Equal(4, secondRound.Count);

            // Verify tournament can continue
            foreach (var match in secondRound)
            {
                match.Winner = match.TeamA.Value;
                match.Loser = match.TeamB.Value;
            }

            var thirdRound = strategy.CreateNextRound(secondRound).ToList();
            Assert.Equal(2, thirdRound.Count);
        }
    }
}
