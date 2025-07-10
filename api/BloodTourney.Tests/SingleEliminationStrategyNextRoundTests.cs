using BloodTourney.Tournament;
using BloodTourney.Tournament.Formats;
using BloodTourney.Models;
using Xunit.Abstractions;

namespace BloodTourney.Tests
{
    public class SingleEliminationStrategyNextRoundTests
    {
        private readonly SingleEliminationStrategy _strategy;
        private readonly ITestOutputHelper _output;

        public SingleEliminationStrategyNextRoundTests(ITestOutputHelper output)
        {
            _strategy = new SingleEliminationStrategy();
            _output = output;
        }

        [Fact]
        public void CreateNextRound_WithFourTeams_CreatesSecondRoundWithTwoWinners()
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

            // Create first round
            var firstRound = ((ITournamentFormat)_strategy).CreateFirstRoundRandom(teams).ToList();

            // Set winners for first round
            firstRound[0].Winner = firstRound[0].TeamA;
            firstRound[0].Loser = firstRound[0].TeamB;
            firstRound[1].Winner = firstRound[1].TeamB;
            firstRound[1].Loser = firstRound[1].TeamA;

            // Output first round visualization
            _output.WriteLine("\nFIRST ROUND:");
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(firstRound, teamNames));

            // Act
            var secondRound = ((ITournamentFormat)_strategy).CreateNextRound(firstRound).ToList();

            // Output second round visualization
            _output.WriteLine("\nSECOND ROUND (FINAL):");
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(secondRound, teamNames));

            // Assert
            Assert.Single(secondRound);
            Assert.Equal(firstRound[0].Winner, secondRound[0].TeamA);
            Assert.Equal(firstRound[1].Winner, secondRound[0].TeamB);
        }

        [Fact]
        public void CreateCompleteEightTeamTournament_ShowsFullBracket()
        {
            // Arrange - Create 8 teams with names
            var teamIds = new List<Guid>();
            var teamNames = new Dictionary<Guid, string>();

            string[] names = new[] {
                "Altdorf Eagles", "Middenheim Marauders", "Nuln Cannoneers", "Talabheim Tigers",
                "Stirland Stompers", "Ostermark Overlords", "Averland Archers", "Wissenland Warriors"
            };

            for (int i = 0; i < 8; i++)
            {
                var teamId = Guid.NewGuid();
                teamIds.Add(teamId);
                teamNames.Add(teamId, names[i]);
            }

            // Act - Create first round
            var firstRound = ((ITournamentFormat)_strategy).CreateFirstRoundRandom(teamIds).ToList();

            // Output first round visualization
            _output.WriteLine("\nFIRST ROUND (QUARTER-FINALS):");
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(firstRound, teamNames));

            // Set winners for first round matches
            for (int i = 0; i < firstRound.Count; i++)
            {
                // Alternate between TeamA and TeamB winning
                if (i % 2 == 0)
                {
                    firstRound[i].Winner = firstRound[i].TeamA;
                    firstRound[i].Loser = firstRound[i].TeamB;
                }
                else
                {
                    firstRound[i].Winner = firstRound[i].TeamB;
                    firstRound[i].Loser = firstRound[i].TeamA;
                }
            }

            // Create second round (semi-finals)
            var secondRound = ((ITournamentFormat)_strategy).CreateNextRound(firstRound).ToList();

            // Output second round visualization
            _output.WriteLine("\nSECOND ROUND (SEMI-FINALS):");
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(secondRound, teamNames));

            // Set winners for second round
            secondRound[0].Winner = secondRound[0].TeamA;
            secondRound[0].Loser = secondRound[0].TeamB;
            secondRound[1].Winner = secondRound[1].TeamB;
            secondRound[1].Loser = secondRound[1].TeamA;

            // Create final round
            var finalRound = ((ITournamentFormat)_strategy).CreateNextRound(secondRound).ToList();

            // Output final round visualization
            _output.WriteLine("\nFINAL ROUND:");
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(finalRound, teamNames));

            // Set winner for final round
            finalRound[0].Winner = finalRound[0].TeamA;
            finalRound[0].Loser = finalRound[0].TeamB;

            // Output champion
            var championId = finalRound[0].Winner.Value;
            _output.WriteLine($"\nTOURNAMENT CHAMPION: {teamNames[championId]}");

            // Assert
            Assert.Equal(4, firstRound.Count); // 4 quarter-final matches
            Assert.Equal(2, secondRound.Count); // 2 semi-final matches
            Assert.Single(finalRound); // 1 final match
        }
    }
}
