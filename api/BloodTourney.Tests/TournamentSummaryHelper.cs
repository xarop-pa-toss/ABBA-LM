using BloodTourney.Tournament;
using System.Text;
using BloodTourney.Tournament.Formats;
using BloodTourney.Models;

namespace BloodTourney.Tests
{
    public static class TournamentSummaryHelper
    {
        public class TournamentResults
        {
            public required TournamentConfig Config { get; init; }
            public required List<List<MatchNode>> Rounds { get; init; }
            public required Dictionary<Guid, string> TeamNames { get; init; }
            public Guid? Champion { get; init; }
            public DateTime StartTime { get; init; } = DateTime.UtcNow;
            public DateTime? EndTime { get; init; }
        }

        public static string GenerateTournamentSummary(TournamentResults results)
        {
            var sb = new StringBuilder();

            // Header
            sb.AppendLine(new string('=', 80));
            sb.AppendLine("BLOOD BOWL TOURNAMENT SUMMARY");
            sb.AppendLine(new string('=', 80));

            // Tournament Configuration
            sb.AppendLine("\nTOURNAMENT CONFIGURATION:");
            sb.AppendLine(new string('-', 40));
            sb.AppendLine($"Format: {results.Config.TournamentFormat}");
            sb.AppendLine($"Ruleset: {results.Config.Ruleset}");
            sb.AppendLine($"First Round Sort: {(results.Config.FirstRoundRandomSort ? "Random" : "Seeded")}");
            sb.AppendLine($"Unspent Cash to Prayers: {results.Config.UnspentCashConvertedToPrayers}");
            sb.AppendLine($"Resurrection Mode: {results.Config.ResurrectionMode}");

            // Tournament Statistics
            sb.AppendLine("\nTOURNAMENT STATISTICS:");
            sb.AppendLine(new string('-', 40));
            sb.AppendLine($"Total Teams: {results.TeamNames.Count}");
            sb.AppendLine($"Number of Rounds: {results.Rounds.Count}");
            sb.AppendLine($"Total Matches: {results.Rounds.Sum(r => r.Count)}");
            sb.AppendLine($"Start Time: {results.StartTime:yyyy-MM-dd HH:mm:ss UTC}");
            if (results.EndTime.HasValue)
                sb.AppendLine($"End Time: {results.EndTime.Value:yyyy-MM-dd HH:mm:ss UTC}");
            if (results.EndTime.HasValue)
                sb.AppendLine($"Duration: {(results.EndTime.Value - results.StartTime).TotalMinutes:N0} minutes");

            // Teams
            sb.AppendLine("\nPARTICIPATING TEAMS:");
            sb.AppendLine(new string('-', 40));
            foreach (var team in results.TeamNames)
            {
                string status = "";
                if (results.Champion.HasValue && team.Key == results.Champion.Value)
                    status = " 🏆 CHAMPION";
                sb.AppendLine($"• {team.Value}{status}");
            }

            // Round-by-Round Results
            sb.AppendLine("\nROUND-BY-ROUND RESULTS:");
            sb.AppendLine(new string('-', 40));
            for (int i = 0; i < results.Rounds.Count; i++)
            {
                var round = results.Rounds[i];
                string roundName = GetRoundName(i, results.Rounds.Count);
                sb.AppendLine($"\n{roundName}:");

                foreach (var match in round)
                {
                    string teamA = results.TeamNames[match.TeamA.Value];
                    string teamB = match.TeamB.HasValue ? results.TeamNames[match.TeamB.Value] : "BYE";
                    string winner = match.Winner.HasValue ? $" - Winner: {results.TeamNames[match.Winner.Value]}" : "";

                    sb.AppendLine($"  {teamA} vs {teamB}{winner}");
                }
            }

            // Tournament Champion
            if (results.Champion.HasValue)
            {
                sb.AppendLine("\nTOURNAMENT CHAMPION:");
                sb.AppendLine(new string('-', 40));
                sb.AppendLine($"🏆 {results.TeamNames[results.Champion.Value]}");
            }

            sb.AppendLine("\n" + new string('=', 80));
            return sb.ToString();
        }

        private static string GetRoundName(int roundIndex, int totalRounds)
        {
            if (roundIndex == totalRounds - 1) return "Final";
            if (roundIndex == totalRounds - 2) return "Semi-Finals";
            if (roundIndex == totalRounds - 3) return "Quarter-Finals";
            if (roundIndex == 0 && totalRounds > 3) return "Round of 16";
            return $"Round {roundIndex + 1}";
        }
    }
}
