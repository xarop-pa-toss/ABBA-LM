using BloodTourney.Tournament;
using BloodTourney.Tournament.Formats;
using Xunit.Abstractions;

namespace BloodTourney.Tests
{
    public class TournamentVisualizationTests
    {
        private readonly SingleEliminationStrategy _strategy;
        private readonly ITestOutputHelper _output;

        public TournamentVisualizationTests(ITestOutputHelper output)
        {
            _strategy = new SingleEliminationStrategy();
            _output = output;
        }

        [Fact]
        public void VisualizeSixteenTeamTournament_CompleteBracket()
        {
            // Arrange - Create teams with Blood Bowl themed names
            var teamIds = new List<Guid>();
            var teamNames = new Dictionary<Guid, string>();

            string[] names = new[]
            {
                // Ranked in order (1-16) for better visualization
                "1. Reikland Reavers", "2. Orcland Raiders", "3. Darkside Cowboys", "4. The Lowdown Rats",
                "5. Chaos All-Stars", "6. Gouged Eye", "7. Grudgebearers", "8. Champions of Death",
                "9. Skavenblight Scramblers", "10. Underworld Creepers", "11. Naggaroth Nightmares", "12. Windrunners",
                "13. Thunder Valley Greenskins", "14. Lustria Croakers", "15. Greenboyz", "16. Lothern Sea Guard"
            };

            for (int i = 0; i < 16; i++)
            {
                var teamId = Guid.NewGuid();
                teamIds.Add(teamId);
                teamNames.Add(teamId, names[i]);
            }

            _output.WriteLine("SIMULATING A COMPLETE 16-TEAM TOURNAMENT:\n");

            // To make bracket display predictable and clear, we'll use the teams in order rather than randomizing
            // This simulates a seeded tournament where #1 plays #16, #2 plays #15, etc.
            var roundsResults = new List<List<MatchNode>>();

            // First Round (Round of 16)
            _output.WriteLine("ROUND 1 (Round of 16):\n");
            var firstRound = CreateSeededFirstRound(teamIds);
            roundsResults.Add(firstRound);
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(firstRound, teamNames));

            // Let higher seeds win each match (#1 beats #16, etc.)
            AssignHigherSeedWinners(firstRound);

            // Second Round (Quarter-Finals)
            _output.WriteLine("\nROUND 2 (Quarter-Finals):\n");
            var secondRound = ((ITournamentFormat)_strategy).CreateNextRound(firstRound).ToList();
            roundsResults.Add(secondRound);
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(secondRound, teamNames));

            // Higher seeds continue winning
            AssignHigherSeedWinners(secondRound);

            // Third Round (Semi-Finals)
            _output.WriteLine("\nROUND 3 (Semi-Finals):\n");
            var thirdRound = ((ITournamentFormat)_strategy).CreateNextRound(secondRound).ToList();
            roundsResults.Add(thirdRound);
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(thirdRound, teamNames));

            // Higher seeds continue winning
            AssignHigherSeedWinners(thirdRound);

            // Fourth Round (Final)
            _output.WriteLine("\nROUND 4 (Final):\n");
            var finalRound = ((ITournamentFormat)_strategy).CreateNextRound(thirdRound).ToList();
            roundsResults.Add(finalRound);
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(finalRound, teamNames));

            // #1 seed wins the tournament
            finalRound[0].Winner = finalRound[0].TeamA; // Top seed is always TeamA in this test
            finalRound[0].Loser = finalRound[0].TeamB;

            // Show complete bracket visualization
            _output.WriteLine("\n" + new string('=', 100));
            _output.WriteLine(TournamentTestHelpers.VisualizeBracket(roundsResults.ToArray(), teamNames));

            // Verify we had the correct number of matches in each round
            Assert.Equal(8, firstRound.Count);   // 8 matches in Round of 16
            Assert.Equal(4, secondRound.Count);  // 4 quarter-final matches
            Assert.Equal(2, thirdRound.Count);   // 2 semi-final matches
            Assert.Single(finalRound);           // 1 final match
        }

        [Fact]
        public void VisualizeSixteenTeamTournament_WithUpsets()
        {
            // Arrange - Create teams with Blood Bowl themed names
            var teamIds = new List<Guid>();
            var teamNames = new Dictionary<Guid, string>();

            string[] names = new[]
            {
                // Ranked in order (1-16) for better visualization
                "1. Reikland Reavers", "2. Orcland Raiders", "3. Darkside Cowboys", "4. The Lowdown Rats",
                "5. Chaos All-Stars", "6. Gouged Eye", "7. Grudgebearers", "8. Champions of Death",
                "9. Skavenblight Scramblers", "10. Underworld Creepers", "11. Naggaroth Nightmares", "12. Windrunners",
                "13. Thunder Valley Greenskins", "14. Lustria Croakers", "15. Greenboyz", "16. Lothern Sea Guard"
            };

            for (int i = 0; i < 16; i++)
            {
                var teamId = Guid.NewGuid();
                teamIds.Add(teamId);
                teamNames.Add(teamId, names[i]);
            }

            _output.WriteLine("SIMULATING A 16-TEAM TOURNAMENT WITH UPSETS:\n");

            var roundsResults = new List<List<MatchNode>>();

            // First Round (Round of 16)
            _output.WriteLine("ROUND 1 (Round of 16):\n");
            var firstRound = CreateSeededFirstRound(teamIds);
            roundsResults.Add(firstRound);
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(firstRound, teamNames));

            // Create some upsets: #9 beats #8, #12 beats #5, #15 beats #2
            AssignWinnersWithUpsets(firstRound, new[] { 3, 4, 6 });

            // Second Round (Quarter-Finals)
            _output.WriteLine("\nROUND 2 (Quarter-Finals):\n");
            var secondRound = ((ITournamentFormat)_strategy).CreateNextRound(firstRound).ToList();
            roundsResults.Add(secondRound);
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(secondRound, teamNames));

            // More upsets: #9 continues their run, beating #1
            AssignWinnersWithUpsets(secondRound, new[] { 0 });

            // Third Round (Semi-Finals)
            _output.WriteLine("\nROUND 3 (Semi-Finals):\n");
            var thirdRound = ((ITournamentFormat)_strategy).CreateNextRound(secondRound).ToList();
            roundsResults.Add(thirdRound);
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(thirdRound, teamNames));

            // No upsets in the semis - favorites win
            AssignWinnersWithUpsets(thirdRound, new int[] { });

            // Fourth Round (Final)
            _output.WriteLine("\nROUND 4 (Final):\n");
            var finalRound = ((ITournamentFormat)_strategy).CreateNextRound(thirdRound).ToList();
            roundsResults.Add(finalRound);
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(finalRound, teamNames));

            // The Cinderella story continues! #9 wins it all!
            // We'll assume #9 is TeamA in the final
            finalRound[0].Winner = finalRound[0].TeamA;
            finalRound[0].Loser = finalRound[0].TeamB;

            // Show complete bracket visualization
            _output.WriteLine("\n" + new string('=', 100));
            _output.WriteLine(TournamentTestHelpers.VisualizeBracket(roundsResults.ToArray(), teamNames));

            // Verify we had the correct number of matches in each round
            Assert.Equal(8, firstRound.Count);   // 8 matches in Round of 16
            Assert.Equal(4, secondRound.Count);  // 4 quarter-final matches
            Assert.Equal(2, thirdRound.Count);   // 2 semi-final matches
            Assert.Single(finalRound);           // 1 final match
        }

        /// <summary>
        /// Creates a first round where team #1 plays #16, #2 plays #15, etc.
        /// This simulates a seeded tournament rather than random matching
        /// </summary>
        private List<MatchNode> CreateSeededFirstRound(List<Guid> teamIds)
        {
            var matches = new List<MatchNode>();

            // Create matches in seeded order: 1v16, 8v9, 5v12, 4v13, 3v14, 6v11, 7v10, 2v15
            int[][] seedPairs = new int[][] {
                new[] {0, 15}, new[] {7, 8}, new[] {4, 11}, new[] {3, 12},
                new[] {2, 13}, new[] {5, 10}, new[] {6, 9}, new[] {1, 14}
            };

            foreach (var pair in seedPairs)
            {
                matches.Add(new MatchNode
                {
                    TeamA = teamIds[pair[0]],
                    TeamB = teamIds[pair[1]],
                    Winner = null,
                    Loser = null
                });
            }

            return matches;
        }

        /// <summary>
        /// Sets the TeamA (higher seed) as the winner of each match
        /// </summary>
        private void AssignHigherSeedWinners(List<MatchNode> matches)
        {
            foreach (var match in matches)
            {
                match.Winner = match.TeamA;
                match.Loser = match.TeamB;
            }
        }

        /// <summary>
        /// Sets winners with upsets in specified match indexes (0-based)
        /// </summary>
        private void AssignWinnersWithUpsets(List<MatchNode> matches, int[] upsetIndexes)
        {
            for (int i = 0; i < matches.Count; i++)
            {
                if (upsetIndexes.Contains(i))
                {
                    // Upset - lower seed (TeamB) wins
                    matches[i].Winner = matches[i].TeamB;
                    matches[i].Loser = matches[i].TeamA;
                }
                else
                {
                    // Expected result - higher seed (TeamA) wins
                    matches[i].Winner = matches[i].TeamA;
                    matches[i].Loser = matches[i].TeamB;
                }
            }
        }
    }
}
