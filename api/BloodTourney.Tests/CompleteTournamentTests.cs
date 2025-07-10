using BloodTourney.Tournament;
using BloodTourney.Tournament.Formats;
using BloodTourney.Models;
using BloodTourney.Ruleset;
using Xunit.Abstractions;

namespace BloodTourney.Tests
{
    public class CompleteTournamentTests(ITestOutputHelper output)
    {
        private readonly SingleEliminationStrategy _strategy = new SingleEliminationStrategy();
        RulesetPresetFactory rulesetFactory = new RulesetPresetFactory((new RulesetBuilder()));

        [Fact]
        public void SixteenTeamTournament_GenerateCompleteSummary()
        {
            // Arrange - Create 16 teams
            var teamIds = new List<Guid>();
            var teamNames = new Dictionary<Guid, string>();

            string[] names = new[]
            {
                // Empire Teams
                "Altdorf Eagles", "Middenheim Marauders", "Nuln Thunderers", "Talabheim Tigers",
                // Chaos Teams
                "Khorne's Killers", "Nurgle's Rotters", "Tzeentch Twisters", "Slaanesh Dancers",
                // Undead Teams
                "Sylvania Spectres", "Tomb Kings", "Vampire Counts", "Necropolis Knights",
                // Mixed Teams
                "Skaven Stormvermin", "Dwarf Ironbreakers", "Wood Elf Wanderers", "Lizardmen Lurkers"
            };

            for (int i = 0; i < 16; i++)
            {
                var teamId = Guid.NewGuid();
                teamIds.Add(teamId);
                teamNames.Add(teamId, names[i]);
            }

            // Create tournament configuration
            var config = new TournamentConfig
            {
                Ruleset = rulesetFactory.CreatePreset(RulesetPresetType.SardineBowl2025),
                TournamentFormat = TournamentFormatType.SingleElimination,
                FirstRoundRandomSort = true,
                UnspentCashConvertedToPrayers = true,
                ResurrectionMode = true
            };

            var startTime = DateTime.UtcNow.AddHours(-3); // Simulate tournament started 3 hours ago
            var roundResults = new List<List<MatchNode>>();

            // First Round (Round of 16)
            var firstRound = ((ITournamentFormat)_strategy).CreateFirstRoundRandom(teamIds).ToList();
            output.WriteLine("\nRound of 16 Matches:");
            output.WriteLine(TournamentTestHelpers.VisualizeMatches(firstRound, teamNames));
            AssignRandomWinners(firstRound);
            roundResults.Add(firstRound);

            // Second Round (Quarter-Finals)
            var secondRound = ((ITournamentFormat)_strategy).CreateNextRound(firstRound).ToList();
            output.WriteLine("\nQuarter-Final Matches:");
            output.WriteLine(TournamentTestHelpers.VisualizeMatches(secondRound, teamNames));
            AssignRandomWinners(secondRound);
            roundResults.Add(secondRound);

            // Third Round (Semi-Finals)
            var thirdRound = ((ITournamentFormat)_strategy).CreateNextRound(secondRound).ToList();
            output.WriteLine("\nSemi-Final Matches:");
            output.WriteLine(TournamentTestHelpers.VisualizeMatches(thirdRound, teamNames));
            AssignRandomWinners(thirdRound);
            roundResults.Add(thirdRound);

            // Fourth Round (Final)
            var finalRound = ((ITournamentFormat)_strategy).CreateNextRound(thirdRound).ToList();
            output.WriteLine("\nFinal Match:");
            output.WriteLine(TournamentTestHelpers.VisualizeMatches(finalRound, teamNames));
            AssignRandomWinners(finalRound);
            roundResults.Add(finalRound);

            var endTime = DateTime.UtcNow; // Tournament just ended

            // Create tournament results
            var tournamentResults = new TournamentSummaryHelper.TournamentResults
            {
                Config = config,
                Rounds = roundResults,
                TeamNames = teamNames,
                Champion = finalRound[0].Winner,
                StartTime = startTime,
                EndTime = endTime
            };

            // Generate and output summary
            string summary = TournamentSummaryHelper.GenerateTournamentSummary(tournamentResults);
            output.WriteLine("\n\nCOMPLETE TOURNAMENT SUMMARY:");
            output.WriteLine(summary);

            // Assertions
            Assert.Equal(8, firstRound.Count);   // 8 matches in Round of 16
            Assert.Equal(4, secondRound.Count);  // 4 quarter-final matches
            Assert.Equal(2, thirdRound.Count);   // 2 semi-final matches
            Assert.Single(finalRound);           // 1 final match
            Assert.NotNull(finalRound[0].Winner); // We have a champion
        }

        [Fact]
        public void EightTeamTournament_GenerateCompleteSummary()
        {
            // Arrange - Create 8 teams
            var teamIds = new List<Guid>();
            var teamNames = new Dictionary<Guid, string>();

            string[] names = new[]
            {
                // Blood Bowl themed team names
                "Reikland Reavers", "Gouged Eye", "Grudgebearers", "Darkside Cowboys",
                "Naggaroth Nightmares", "Skavenblight Scramblers", "The Lowdown Rats", "Champions of Death"
            };

            for (int i = 0; i < 8; i++)
            {
                var teamId = Guid.NewGuid();
                teamIds.Add(teamId);
                teamNames.Add(teamId, names[i]);
            }

            // Create tournament configuration
            var config = new TournamentConfig
            {
                Ruleset = rulesetFactory.CreatePreset(RulesetPresetType.SardineBowl2025),
                TournamentFormat = TournamentFormatType.SingleElimination,
                FirstRoundRandomSort = false, // Seeded tournament
                UnspentCashConvertedToPrayers = false,
                ResurrectionMode = false // Injuries persist between matches
            };

            var startTime = DateTime.UtcNow.AddDays(-1); // Tournament started yesterday
            var roundResults = new List<List<MatchNode>>();

            // Create detailed match descriptions to track blood bowl injuries
            var matchDescriptions = new Dictionary<string, string>();

            // First Round (Quarter-Finals)
            var firstRound = ((ITournamentFormat)_strategy).CreateFirstRoundRandom(teamIds).ToList();

            // Add match descriptions
            for (int i = 0; i < firstRound.Count; i++)
            {
                var matchId = $"QF{i+1}";
                var teamA = teamNames[firstRound[i].TeamA.Value];
                var teamB = teamNames[firstRound[i].TeamB.Value];

                // Create a fake match description with injuries
                matchDescriptions[matchId] = $"{teamA} vs {teamB}: {GenerateMatchDescription()}";
            }

            AssignPredeterminedWinners(firstRound, new[] { 0, 1, 2, 3 }); // First team wins each match
            roundResults.Add(firstRound);

            // Second Round (Semi-Finals)
            var secondRound = ((ITournamentFormat)_strategy).CreateNextRound(firstRound).ToList();

            // Add match descriptions
            for (int i = 0; i < secondRound.Count; i++)
            {
                var matchId = $"SF{i+1}";
                var teamA = teamNames[secondRound[i].TeamA.Value];
                var teamB = teamNames[secondRound[i].TeamB.Value];

                matchDescriptions[matchId] = $"{teamA} vs {teamB}: {GenerateMatchDescription()}";
            }

            AssignPredeterminedWinners(secondRound, new[] { 0, 1 }); // First team wins each match
            roundResults.Add(secondRound);

            // Third Round (Final)
            var finalRound = ((ITournamentFormat)_strategy).CreateNextRound(secondRound).ToList();

            // Add final match description
            {
                var matchId = "F1";
                var teamA = teamNames[finalRound[0].TeamA.Value];
                var teamB = teamNames[finalRound[0].TeamB.Value];

                matchDescriptions[matchId] = $"{teamA} vs {teamB}: {GenerateMatchDescription(true)}";
            }

            AssignPredeterminedWinners(finalRound, new[] { 0 }); // First team wins
            roundResults.Add(finalRound);

            var endTime = DateTime.UtcNow; // Tournament just ended

            // Create tournament results
            var tournamentResults = new TournamentSummaryHelper.TournamentResults
            {
                Config = config,
                Rounds = roundResults,
                TeamNames = teamNames,
                Champion = finalRound[0].Winner,
                StartTime = startTime,
                EndTime = endTime
            };

            // Generate and output summary
            string summary = TournamentSummaryHelper.GenerateTournamentSummary(tournamentResults);
            output.WriteLine("\nEIGHT-TEAM TOURNAMENT SUMMARY:");
            output.WriteLine(summary);

            // Output detailed match reports
            output.WriteLine("\nDETAILED MATCH REPORTS:");
            output.WriteLine(new string('-', 40));
            foreach (var match in matchDescriptions)
            {
                output.WriteLine($"Match {match.Key}: {match.Value}");
            }

            // Assertions
            Assert.Equal(4, firstRound.Count);   // 4 quarter-final matches
            Assert.Equal(2, secondRound.Count);  // 2 semi-final matches
            Assert.Single(finalRound);           // 1 final match
            Assert.NotNull(finalRound[0].Winner); // We have a champion
        }

        private void AssignRandomWinners(List<MatchNode> matches)
        {
            var random = new Random();
            foreach (var match in matches)
            {
                if (match.TeamB == null)
                {
                    // Bye match - TeamA automatically wins
                    match.Winner = match.TeamA;
                    match.Loser = null;
                    continue;
                }

                // Randomly choose winner
                bool teamAWins = random.Next(2) == 0;
                match.Winner = teamAWins ? match.TeamA : match.TeamB;
                match.Loser = teamAWins ? match.TeamB : match.TeamA;
            }
        }

        private void AssignPredeterminedWinners(List<MatchNode> matches, int[] teamAWinsIndexes)
        {
            for (int i = 0; i < matches.Count; i++)
            {
                var match = matches[i];

                if (match.TeamB == null)
                {
                    // Bye match - TeamA automatically wins
                    match.Winner = match.TeamA;
                    match.Loser = null;
                    continue;
                }

                bool teamAWins = teamAWinsIndexes.Contains(i);
                match.Winner = teamAWins ? match.TeamA : match.TeamB;
                match.Loser = teamAWins ? match.TeamB : match.TeamA;
            }
        }

        private string GenerateMatchDescription(bool isFinal = false)
        {
            var random = new Random();

            // Generate random score
            int teamAScore = random.Next(isFinal ? 1 : 0, 5); // Finals usually have at least 1 touchdown
            int teamBScore = random.Next(0, teamAScore + 1); // Ensure B doesn't exceed A's score

            if (teamAScore == teamBScore && !isFinal)
            {
                // In Blood Bowl, ties are usually broken by a random event
                teamAScore++; // Give team A an extra point for the win
            }

            // Generate random injuries
            string[] injuryTypes = { "Badly Hurt", "Broken Ribs", "Smashed Hip", "Broken Ankle", "Fractured Skull", "DEAD" };
            string[] playerPositions = { "Lineman", "Blitzer", "Thrower", "Catcher", "Blocker", "Wardancer", "Witch Elf" };

            int injuryCount = random.Next(0, 4); // 0-3 injuries
            var injuries = new List<string>();

            for (int i = 0; i < injuryCount; i++)
            {
                string team = random.Next(2) == 0 ? "Team A" : "Team B";
                string position = playerPositions[random.Next(playerPositions.Length)];
                string injury = injuryTypes[random.Next(injuryTypes.Length)];

                injuries.Add($"{team} {position} suffered {injury}");
            }

            // Combine into a match description
            string matchResult = $"Final Score {teamAScore}-{teamBScore}";
            string injuryReport = injuries.Count > 0 ? $" Injuries: {string.Join(", ", injuries)}" : " No injuries reported.";

            return matchResult + "." + injuryReport;
        }
    }
}
