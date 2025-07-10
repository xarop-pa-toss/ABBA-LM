using BloodTourney.Tournament;
using BloodTourney.Tournament.Formats;
using BloodTourney.Models;
using System.Text;
using BloodTourney.Ruleset;
using Xunit.Abstractions;

namespace BloodTourney.Tests
{
    public class CompleteSixteenTeamTournamentTest
    {
        private readonly SingleEliminationStrategy _strategy;
        private readonly ITestOutputHelper _output;
        private readonly RulesetPresetFactory _rulesetPresetFactory = new RulesetPresetFactory((new RulesetBuilder()));
        public CompleteSixteenTeamTournamentTest(ITestOutputHelper output)
        {
            _strategy = new SingleEliminationStrategy();
            _output = output;
        }

        [Fact]
        public void RunCompleteTournament_WithAllProperties()
        {
            // Arrange - Create all teams
            var teamIds = new List<Guid>();
            var teamNames = new Dictionary<Guid, string>();

            string[] names = new[]
            {
                // Blood Bowl themed team names with team races
                "Reikland Reavers (Human)", "Gouged Eye (Orc)", "Grudgebearers (Dwarf)", "Darkside Cowboys (Chaos)",
                "Naggaroth Nightmares (Dark Elf)", "Skavenblight Scramblers (Skaven)", "Thunder Valley Greenskins (Goblin)", "Orcland Raiders (Orc)",
                "Chaos All-Stars (Chaos)", "Champions of Death (Undead)", "Lustria Croakers (Lizardmen)", "Underworld Creepers (Underworld)",
                "The Lowdown Rats (Skaven)", "Windrunners (Wood Elf)", "Greenboyz (Goblin)", "Lothern Sea Guard (High Elf)"
            };

            for (int i = 0; i < 16; i++)
            {
                var teamId = Guid.NewGuid();
                teamIds.Add(teamId);
                teamNames.Add(teamId, names[i]);
            }

            // Create team statistics dictionaries
            var teamCoaches = new Dictionary<Guid, string>();
            var teamRaces = new Dictionary<Guid, string>();
            var teamRecords = new Dictionary<Guid, (int Wins, int Losses, int Draws)>();
            var teamTouchdowns = new Dictionary<Guid, int>();
            var teamCasualties = new Dictionary<Guid, int>();

            // Sample coach names
            string[] coaches = {
                "Franz Hoffman", "Grumlok Skullsplitter", "Thorgrim Grimbeard", "Malik the Destroyer",
                "Lilith Darkblade", "Skritter Scarpaw", "Snazzgit Bloodeye", "Varag Ghoulchewer",
                "Arghul Deathbringer", "Heinrich Kemmler", "Tehenhauin", "Ikit Claw",
                "Queek Headtaker", "Orion", "Grom the Paunch", "Tyrion"
            };

            // Initialize team records and stats
            for (int i = 0; i < 16; i++)
            {
                var teamId = teamIds[i];
                var teamName = teamNames[teamId];

                // Extract race from team name
                var startIndex = teamName.LastIndexOf('(') + 1;
                var endIndex = teamName.LastIndexOf(')');
                var race = teamName.Substring(startIndex, endIndex - startIndex);

                teamCoaches[teamId] = coaches[i];
                teamRaces[teamId] = race;
                teamRecords[teamId] = (0, 0, 0);
                teamTouchdowns[teamId] = 0;
                teamCasualties[teamId] = 0;
            }

            // Create tournament configuration
            var config = new TournamentConfig
            {
                Ruleset = _rulesetPresetFactory.CreatePreset(RulesetPresetType.SardineBowl2025),
                TournamentFormat = TournamentFormatType.SingleElimination,
                FirstRoundRandomSort = true,
                UnspentCashConvertedToPrayers = true,
                ResurrectionMode = true
            };

            var startTime = DateTime.UtcNow.AddHours(-5); // Tournament started 5 hours ago
            var roundResults = new List<List<MatchNode>>();
            var matchResults = new List<MatchResult>();

            // Track match details
            var matchDetails = new Dictionary<string, (Guid TeamA, Guid TeamB, int ScoreA, int ScoreB, Guid Winner, string Details)>();

            // FIRST ROUND (Round of 16)
            _output.WriteLine("\nFIRST ROUND (Round of 16):");
            _output.WriteLine(new string('=', 70));
            var firstRound = ((ITournamentFormat)_strategy).CreateFirstRoundRandom(teamIds).ToList();
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(firstRound, teamNames));

            // Simulate first round matches
            for (int i = 0; i < firstRound.Count; i++)
            {
                var match = firstRound[i];
                var teamA = match.TeamA.Value;
                var teamB = match.TeamB.Value;

                // Simulate match result
                var (winner, scoreA, scoreB, details) = SimulateMatch(teamA, teamB, teamNames);

                // Update match with winner
                match.Winner = winner;
                match.Loser = winner.Equals(teamA) ? teamB : teamA;

                // Store match details
                string matchId = $"R1-M{i+1}";
                matchDetails[matchId] = (teamA, teamB, scoreA, scoreB, winner, details);

                // Update team statistics
                UpdateTeamStats(teamA, teamB, winner, scoreA, scoreB, teamRecords, teamTouchdowns, teamCasualties);
            }
            roundResults.Add(firstRound);

            // SECOND ROUND (Quarter-finals)
            _output.WriteLine("\nSECOND ROUND (Quarter-finals):");
            _output.WriteLine(new string('=', 70));
            var secondRound = ((ITournamentFormat)_strategy).CreateNextRound(firstRound).ToList();
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(secondRound, teamNames));

            // Simulate second round matches
            for (int i = 0; i < secondRound.Count; i++)
            {
                var match = secondRound[i];
                var teamA = match.TeamA.Value;
                var teamB = match.TeamB.Value;

                // Simulate match result
                var (winner, scoreA, scoreB, details) = SimulateMatch(teamA, teamB, teamNames);

                // Update match with winner
                match.Winner = winner;
                match.Loser = winner.Equals(teamA) ? teamB : teamA;

                // Store match details
                string matchId = $"R2-M{i+1}";
                matchDetails[matchId] = (teamA, teamB, scoreA, scoreB, winner, details);

                // Update team statistics
                UpdateTeamStats(teamA, teamB, winner, scoreA, scoreB, teamRecords, teamTouchdowns, teamCasualties);
            }
            roundResults.Add(secondRound);

            // THIRD ROUND (Semi-finals)
            _output.WriteLine("\nTHIRD ROUND (Semi-finals):");
            _output.WriteLine(new string('=', 70));
            var thirdRound = ((ITournamentFormat)_strategy).CreateNextRound(secondRound).ToList();
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(thirdRound, teamNames));

            // Simulate third round matches
            for (int i = 0; i < thirdRound.Count; i++)
            {
                var match = thirdRound[i];
                var teamA = match.TeamA.Value;
                var teamB = match.TeamB.Value;

                // Simulate match result
                var (winner, scoreA, scoreB, details) = SimulateMatch(teamA, teamB, teamNames);

                // Update match with winner
                match.Winner = winner;
                match.Loser = winner.Equals(teamA) ? teamB : teamA;

                // Store match details
                string matchId = $"R3-M{i+1}";
                matchDetails[matchId] = (teamA, teamB, scoreA, scoreB, winner, details);

                // Update team statistics
                UpdateTeamStats(teamA, teamB, winner, scoreA, scoreB, teamRecords, teamTouchdowns, teamCasualties);
            }
            roundResults.Add(thirdRound);

            // FOURTH ROUND (Final)
            _output.WriteLine("\nFOURTH ROUND (Final):");
            _output.WriteLine(new string('=', 70));
            var finalRound = ((ITournamentFormat)_strategy).CreateNextRound(thirdRound).ToList();
            _output.WriteLine(TournamentTestHelpers.VisualizeMatches(finalRound, teamNames));

            // Simulate final match
            {
                var match = finalRound[0];
                var teamA = match.TeamA.Value;
                var teamB = match.TeamB.Value;

                // Simulate match result (finals are often more exciting with more scoring)
                var (winner, scoreA, scoreB, details) = SimulateMatch(teamA, teamB, teamNames, true);

                // Update match with winner
                match.Winner = winner;
                match.Loser = winner.Equals(teamA) ? teamB : teamA;

                // Store match details
                string matchId = "FINAL";
                matchDetails[matchId] = (teamA, teamB, scoreA, scoreB, winner, details);

                // Update team statistics
                UpdateTeamStats(teamA, teamB, winner, scoreA, scoreB, teamRecords, teamTouchdowns, teamCasualties);
            }
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
            _output.WriteLine("\nCOMPLETE TOURNAMENT SUMMARY:");
            _output.WriteLine(summary);

            // Output detailed team statistics
            _output.WriteLine("\nTEAM STATISTICS:");
            _output.WriteLine(new string('=', 70));
            _output.WriteLine(GenerateTeamStatistics(teamIds, teamNames, teamCoaches, teamRaces, teamRecords, teamTouchdowns, teamCasualties));

            // Output detailed match reports
            _output.WriteLine("\nDETAILED MATCH REPORTS:");
            _output.WriteLine(new string('=', 70));
            _output.WriteLine(GenerateMatchReports(matchDetails, teamNames));

            // Output tournament bracket visualization
            _output.WriteLine("\nTOURNAMENT BRACKET VISUALIZATION:");
            _output.WriteLine(new string('=', 70));
            _output.WriteLine(TournamentTestHelpers.VisualizeCompleteBracket(roundResults.ToArray(), teamNames));

            // Assertions
            Assert.Equal(8, firstRound.Count);   // 8 matches in Round of 16
            Assert.Equal(4, secondRound.Count);  // 4 quarter-final matches
            Assert.Equal(2, thirdRound.Count);   // 2 semi-final matches
            Assert.Single(finalRound);           // 1 final match
            Assert.NotNull(finalRound[0].Winner); // We have a champion

            // Verify all teams played at least one match
            foreach (var teamId in teamIds)
            {
                var record = teamRecords[teamId];
                Assert.True(record.Wins + record.Losses > 0, $"Team {teamNames[teamId]} should have played at least one match");
            }
        }

        private (Guid Winner, int ScoreA, int ScoreB, string Details) SimulateMatch(Guid teamA, Guid teamB, Dictionary<Guid, string> teamNames, bool isFinal = false)
        {
            var random = new Random();

            // Generate random score (finals tend to have more scoring)
            int maxScore = isFinal ? 5 : 3;
            int scoreA = random.Next(0, maxScore + 1);
            int scoreB = random.Next(0, maxScore + 1);

            // Ensure no ties in elimination tournament
            while (scoreA == scoreB)
            {
                // In Blood Bowl, ties are broken by random events or overtime
                if (random.Next(2) == 0)
                    scoreA++;
                else
                    scoreB++;
            }

            // Determine winner
            Guid winner = scoreA > scoreB ? teamA : teamB;

            // Generate match details
            StringBuilder details = new StringBuilder();
            details.AppendLine($"{teamNames[teamA]} {scoreA} - {scoreB} {teamNames[teamB]}");

            // Add some random events
            string[] eventTypes = { "Touchdown", "Casualty", "Interception", "Perfect Defense", "Brilliant Pass", "Fumble", "Crowd Invasion" };
            string[] playerTypes = { "Blitzer", "Thrower", "Catcher", "Lineman", "Blocker", "Witch Elf", "Gutter Runner" };

            int numEvents = random.Next(2, 6); // 2-5 notable events
            for (int i = 0; i < numEvents; i++)
            {
                Guid team = random.Next(2) == 0 ? teamA : teamB;
                string eventType = eventTypes[random.Next(eventTypes.Length)];
                string playerType = playerTypes[random.Next(playerTypes.Length)];

                details.AppendLine($"- {teamNames[team]} {playerType}: {eventType}");
            }

            // Add injuries if any
            string[] injuryTypes = { "Badly Hurt", "Broken Ribs", "Smashed Hip", "Broken Ankle", "Fractured Skull", "DEAD" };

            int injuryCount = random.Next(0, 3); // 0-2 injuries
            if (injuryCount > 0)
            {
                details.AppendLine("\nInjuries:");
                for (int i = 0; i < injuryCount; i++)
                {
                    Guid team = random.Next(2) == 0 ? teamA : teamB;
                    string playerType = playerTypes[random.Next(playerTypes.Length)];
                    string injury = injuryTypes[random.Next(injuryTypes.Length)];

                    details.AppendLine($"- {teamNames[team]} {playerType}: {injury}");
                }
            }

            return (winner, scoreA, scoreB, details.ToString());
        }

        private void UpdateTeamStats(
            Guid teamA, 
            Guid teamB, 
            Guid winner, 
            int scoreA, 
            int scoreB, 
            Dictionary<Guid, (int Wins, int Losses, int Draws)> teamRecords,
            Dictionary<Guid, int> teamTouchdowns,
            Dictionary<Guid, int> teamCasualties)
        {
            // Update win/loss records
            if (winner.Equals(teamA))
            {
                var recordA = teamRecords[teamA];
                teamRecords[teamA] = (recordA.Wins + 1, recordA.Losses, recordA.Draws);

                var recordB = teamRecords[teamB];
                teamRecords[teamB] = (recordB.Wins, recordB.Losses + 1, recordB.Draws);
            }
            else
            {
                var recordA = teamRecords[teamA];
                teamRecords[teamA] = (recordA.Wins, recordA.Losses + 1, recordA.Draws);

                var recordB = teamRecords[teamB];
                teamRecords[teamB] = (recordB.Wins + 1, recordB.Losses, recordB.Draws);
            }

            // Update touchdowns
            teamTouchdowns[teamA] += scoreA;
            teamTouchdowns[teamB] += scoreB;

            // Update casualties (random number based on Blood Bowl's physical nature)
            var random = new Random();
            teamCasualties[teamA] += random.Next(0, 3); // 0-2 casualties caused by team A
            teamCasualties[teamB] += random.Next(0, 3); // 0-2 casualties caused by team B
        }

        private string GenerateTeamStatistics(
            List<Guid> teamIds,
            Dictionary<Guid, string> teamNames,
            Dictionary<Guid, string> teamCoaches,
            Dictionary<Guid, string> teamRaces,
            Dictionary<Guid, (int Wins, int Losses, int Draws)> teamRecords,
            Dictionary<Guid, int> teamTouchdowns,
            Dictionary<Guid, int> teamCasualties)
        {
            var sb = new StringBuilder();

            // Sort teams by wins
            var sortedTeams = teamIds
                .OrderByDescending(id => teamRecords[id].Wins)
                .ThenByDescending(id => teamTouchdowns[id])
                .ToList();

            // Table header
            sb.AppendLine("Team                  | Race      | Coach              | W-L-D | TD | CAS");
            sb.AppendLine(new string('-', 80));

            foreach (var teamId in sortedTeams)
            {
                var name = teamNames[teamId].Split('(')[0].Trim().PadRight(22);
                var race = teamRaces[teamId].PadRight(10);
                var coach = teamCoaches[teamId].PadRight(18);
                var record = teamRecords[teamId];
                var recordStr = $"{record.Wins}-{record.Losses}-{record.Draws}".PadRight(5);
                var touchdowns = teamTouchdowns[teamId].ToString().PadRight(3);
                var casualties = teamCasualties[teamId].ToString().PadRight(3);

                sb.AppendLine($"{name}| {race}| {coach}| {recordStr}| {touchdowns}| {casualties}");
            }

            return sb.ToString();
        }

        private string GenerateMatchReports(Dictionary<string, (Guid TeamA, Guid TeamB, int ScoreA, int ScoreB, Guid Winner, string Details)> matchDetails, 
                                            Dictionary<Guid, string> teamNames)
        {
            var sb = new StringBuilder();

            foreach (var match in matchDetails)
            {
                sb.AppendLine($"Match: {match.Key}");
                sb.AppendLine(new string('-', 40));
                sb.AppendLine(match.Value.Details);
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
