using BloodTourney.Tournament;
using BloodTourney.Tournament.Formats;
using Xunit.Abstractions;

namespace BloodTourney.Tests
{
    public class SingleEliminationSixteenTeamTests
    {
        private readonly SingleEliminationStrategy _strategy;
        private readonly ITestOutputHelper _output;

        public SingleEliminationSixteenTeamTests(ITestOutputHelper output)
        {
            _strategy = new SingleEliminationStrategy();
            _output = output;
        }

        [Fact]
        public void CreateCompleteSixteenTeamTournament_ShowsFullBracket()
        {
            // Arrange - Create 16 teams with themed names
            var teamIds = new List<Guid>();
            var teamNames = new Dictionary<Guid, string>();

            string[] names = new[]
            {
                // Empire teams
                "Altdorf Eagles", "Middenheim Marauders", "Nuln Cannoneers", "Talabheim Tigers",
                // Skaven teams
                "Clan Eshin Assassins", "Clan Moulder Mutants", "Clan Skryre Warpfire", "Clan Pestilens Plague",
                // Chaos teams
                "Khorne Bloodthirsters", "Nurgle Plaguebearers", "Tzeentch Flamers", "Slaanesh Seducers",
                // Miscellaneous teams
                "Orc Stompers", "Dwarf Ironbreakers", "Elven Wardancers", "Undead Revenant Horrors"
            };

            for (int i = 0; i < 16; i++)
            {
                var teamId = Guid.NewGuid();
                teamIds.Add(teamId);
                teamNames.Add(teamId, names[i]);
            }

            // FIRST ROUND (Round of 16)
            _output.WriteLine("\nFIRST ROUND (Round of 16):");
            _output.WriteLine(new string('=', 70));

            var firstRound = ((ITournamentFormat)_strategy).CreateFirstRoundRandom(teamIds).ToList();
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(firstRound, teamNames));

            // Set winners for first round matches (alternate between TeamA and TeamB winning)
            SetAlternatingWinners(firstRound);

            // SECOND ROUND (Quarter-finals)
            _output.WriteLine("\nSECOND ROUND (Quarter-finals):");
            _output.WriteLine(new string('=', 70));

            var secondRound = ((ITournamentFormat)_strategy).CreateNextRound(firstRound).ToList();
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(secondRound, teamNames));

            // Set winners for second round
            SetAlternatingWinners(secondRound);

            // THIRD ROUND (Semi-finals)
            _output.WriteLine("\nTHIRD ROUND (Semi-finals):");
            _output.WriteLine(new string('=', 70));

            var thirdRound = ((ITournamentFormat)_strategy).CreateNextRound(secondRound).ToList();
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(thirdRound, teamNames));

            // Set winners for semi-finals
            SetAlternatingWinners(thirdRound);

            // FOURTH ROUND (Final)
            _output.WriteLine("\nFOURTH ROUND (Final):");
            _output.WriteLine(new string('=', 70));

            var finalRound = ((ITournamentFormat)_strategy).CreateNextRound(thirdRound).ToList();
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(finalRound, teamNames));

            // Set winner for final
            finalRound[0].Winner = finalRound[0].TeamA;
            finalRound[0].Loser = finalRound[0].TeamB;

            // Output champion
            var championId = finalRound[0].Winner.Value;
            _output.WriteLine("\nTOURNAMENT CHAMPION:");
            _output.WriteLine(new string('=', 70));
            _output.WriteLine($"{teamNames[championId]}");

            // Assert the correct number of matches in each round
            Assert.Equal(8, firstRound.Count);  // 8 matches in Round of 16
            Assert.Equal(4, secondRound.Count); // 4 quarter-final matches
            Assert.Equal(2, thirdRound.Count);  // 2 semi-final matches
            Assert.Single(finalRound);         // 1 final match

            // Verify all teams are used in the first round
            var usedTeams = firstRound
                .SelectMany(m => new[] { m.TeamA, m.TeamB })
                .Where(t => t.HasValue)
                .Select(t => t.Value)
                .ToList();
            Assert.Equal(16, usedTeams.Distinct().Count());
        }

        [Fact]
        public void CreateCompleteSixteenTeamTournament_WithBracketTree()
        {
            // Arrange - Create 16 teams with themed names
            var teamIds = new List<Guid>();
            var teamNames = new Dictionary<Guid, string>();

            string[] names = new[]
            {
                // Blood Bowl themed team names
                "Reikland Reavers", "Gouged Eye", "Grudgebearers", "Darkside Cowboys",
                "Naggaroth Nightmares", "Skavenblight Scramblers", "Thunder Valley Greenskins", "Orcland Raiders",
                "Chaos All-Stars", "Champions of Death", "Lustria Croakers", "Underworld Creepers",
                "The Lowdown Rats", "Windrunners", "Greenboyz", "Lothern Sea Guard"
            };

            for (int i = 0; i < 16; i++)
            {
                var teamId = Guid.NewGuid();
                teamIds.Add(teamId);
                teamNames.Add(teamId, names[i]);
            }

            // Create all rounds upfront with predetermined winners to show full bracket
            _output.WriteLine("\nCOMPLETE TOURNAMENT BRACKET VISUALIZATION:");
            _output.WriteLine(new string('=', 70));

            // First Round (Round of 16)
            var firstRound = ((ITournamentFormat)_strategy).CreateFirstRoundRandom(teamIds).ToList();
            // Set predetermined winners for better visualization
            for (int i = 0; i < firstRound.Count; i++)
            {
                // Make team A win in all matches for consistent bracket
                firstRound[i].Winner = firstRound[i].TeamA;
                firstRound[i].Loser = firstRound[i].TeamB;
            }

            // Second Round (Quarter-finals)
            var secondRound = ((ITournamentFormat)_strategy).CreateNextRound(firstRound).ToList();
            // Set predetermined winners
            for (int i = 0; i < secondRound.Count; i++)
            {
                secondRound[i].Winner = secondRound[i].TeamA;
                secondRound[i].Loser = secondRound[i].TeamB;
            }

            // Third Round (Semi-finals)
            var thirdRound = ((ITournamentFormat)_strategy).CreateNextRound(secondRound).ToList();
            // Set predetermined winners
            for (int i = 0; i < thirdRound.Count; i++)
            {
                thirdRound[i].Winner = thirdRound[i].TeamA;
                thirdRound[i].Loser = thirdRound[i].TeamB;
            }

            // Fourth Round (Final)
            var finalRound = ((ITournamentFormat)_strategy).CreateNextRound(thirdRound).ToList();
            // Set winner for final
            finalRound[0].Winner = finalRound[0].TeamA;
            finalRound[0].Loser = finalRound[0].TeamB;

            // Display full bracket tree
            _output.WriteLine("Round of 16:");
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(firstRound, teamNames));

            _output.WriteLine("\nQuarter-finals:");
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(secondRound, teamNames));

            _output.WriteLine("\nSemi-finals:");
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(thirdRound, teamNames));

            _output.WriteLine("\nFinal:");
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(finalRound, teamNames));

            _output.WriteLine("\nCHAMPION: " + teamNames[finalRound[0].Winner.Value]);

            // Create bracket progression visualization
            _output.WriteLine("\nBRACKET PROGRESSION:");
            _output.WriteLine(new string('=', 70));

            VisualizeBracketProgression(firstRound, secondRound, thirdRound, finalRound, teamNames);

            // Assertions
            Assert.Equal(8, firstRound.Count);
            Assert.Equal(4, secondRound.Count);
            Assert.Equal(2, thirdRound.Count);
            Assert.Single(finalRound);
        }

        /// <summary>
        /// Sets alternating winners (Team A wins in even matches, Team B in odd matches)
        /// </summary>
        private void SetAlternatingWinners(List<MatchNode> matches)
        {
            for (int i = 0; i < matches.Count; i++)
            {
                if (i % 2 == 0)
                {
                    matches[i].Winner = matches[i].TeamA;
                    matches[i].Loser = matches[i].TeamB;
                }
                else
                {
                    matches[i].Winner = matches[i].TeamB;
                    matches[i].Loser = matches[i].TeamA;
                }
            }
        }

        /// <summary>
        /// Creates a visual representation of how teams progress through the tournament bracket
        /// </summary>
        private void VisualizeBracketProgression(
            List<MatchNode> round1, 
            List<MatchNode> round2, 
            List<MatchNode> round3, 
            List<MatchNode> round4, 
            Dictionary<Guid, string> teamNames)
        {
            var sb = new System.Text.StringBuilder();

            // Get shortened team names (first 15 chars)
            Func<Guid?, string> getShortName = (guid) => {
                if (!guid.HasValue) return "BYE";
                var name = teamNames[guid.Value];
                return name.Length <= 15 ? name : name.Substring(0, 12) + "...";
            };

            // Draw Round 1 (16 teams - 8 matches)
            for (int i = 0; i < round1.Count; i++)
            {
                string teamA = getShortName(round1[i].TeamA).PadRight(15);
                string teamB = getShortName(round1[i].TeamB).PadRight(15);
                string winner = round1[i].Winner.Equals(round1[i].TeamA) ? "→" : " ";
                string winnerB = round1[i].Winner.Equals(round1[i].TeamB) ? "→" : " ";

                sb.AppendLine($"{teamA} {winner}|");
                sb.AppendLine($"{teamB} {winnerB}|--{getShortName(round1[i].Winner)}-→|");

                // Connect to round 2 matches
                if (i % 2 == 1)
                {
                    string r2Winner = round2[i/2].Winner.Equals(round1[i].Winner) ? "→" : " ";
                    string r2WinnerB = round2[i/2].Winner.Equals(round1[i-1].Winner) ? "→" : " ";

                    sb.AppendLine($"            {r2WinnerB}|--{getShortName(round2[i/2].Winner)}-→|");

                    // Connect to round 3 (semi-finals)
                    if (i % 4 == 3)
                    {
                        string r3Winner = round3[i/4].Winner.Equals(round2[i/2].Winner) ? "→" : " ";
                        string r3WinnerB = round3[i/4].Winner.Equals(round2[(i-2)/2].Winner) ? "→" : " ";

                        sb.AppendLine($"                        {r3WinnerB}|--{getShortName(round3[i/4].Winner)}-→|");

                        // Connect to final
                        if (i == 7)
                        {
                            string finalWinner = round4[0].Winner.Equals(round3[1].Winner) ? "→" : " ";
                            string finalWinnerB = round4[0].Winner.Equals(round3[0].Winner) ? "→" : " ";

                            sb.AppendLine($"                                    {finalWinnerB}|--{getShortName(round4[0].Winner)} (CHAMPION)");
                        }
                    }
                }

                if (i % 2 == 1) sb.AppendLine();
            }

            _output.WriteLine(sb.ToString());
        }
    }
}
