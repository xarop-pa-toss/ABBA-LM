using BloodTourney.Tournament;
using System.Text;
using BloodTourney.Tournament.Formats;

namespace BloodTourney.Tests
{
    public static class TournamentTestHelpers
    {
        /// <summary>
        /// Creates a simple visual representation of tournament matches
        /// </summary>
        public static string VisualizeMatches(IEnumerable<MatchNode> matches, Dictionary<Guid, string> teamNames = null)
        {
            var sb = new StringBuilder();
            var matchList = matches.ToList();

            sb.AppendLine($"Tournament Bracket ({matchList.Count} matches):");
            sb.AppendLine(new string('-', 50));

            for (int i = 0; i < matchList.Count; i++)
            {
                var match = matchList[i];
                string teamAName = GetTeamName(match.TeamA, teamNames, "Team A");
                string teamBName = match.TeamB.HasValue 
                    ? GetTeamName(match.TeamB, teamNames, "Team B") 
                    : "BYE";

                string winnerIndicator = "";
                if (match.Winner.HasValue)
                {
                    bool isTeamAWinner = match.Winner.Equals(match.TeamA);
                    winnerIndicator = isTeamAWinner ? " (Winner)" : "";
                    teamBName += !isTeamAWinner ? " (Winner)" : "";
                }

                sb.AppendLine($"Match {i + 1}: {teamAName}{winnerIndicator} vs {teamBName}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Creates a detailed visual representation of a tournament bracket organized by rounds
        /// </summary>
        public static string VisualizeBracket(List<MatchNode>[] rounds, Dictionary<Guid, string> teamNames)
        {
            var sb = new StringBuilder();

            sb.AppendLine("TOURNAMENT BRACKET OVERVIEW:");
            sb.AppendLine(new string('=', 100));

            for (int r = 0; r < rounds.Length; r++)
            {
                string roundName = GetRoundName(r, rounds.Length);
                sb.AppendLine($"\n{roundName}:");
                sb.AppendLine(new string('-', 50));

                var matches = rounds[r];
                for (int i = 0; i < matches.Count; i++)
                {
                    var match = matches[i];
                    string teamAName = GetTeamName(match.TeamA, teamNames, "Team A");
                    string teamBName = match.TeamB.HasValue 
                        ? GetTeamName(match.TeamB, teamNames, "Team B") 
                        : "BYE";

                    // Add visual indicator for winner
                    if (match.Winner.HasValue)
                    {
                        bool isTeamAWinner = match.Winner.Equals(match.TeamA);
                        teamAName = isTeamAWinner ? $"→ {teamAName}" : $"  {teamAName}";
                        teamBName = !isTeamAWinner ? $"→ {teamBName}" : $"  {teamBName}";
                    }

                    sb.AppendLine($"Match {i + 1}: {teamAName} vs {teamBName}");
                }
            }

            // Display the champion if tournament is complete
            if (rounds.Length > 0 && rounds[rounds.Length - 1].Count > 0)
            {
                var finalMatch = rounds[rounds.Length - 1][0];
                if (finalMatch.Winner.HasValue && teamNames != null && teamNames.ContainsKey(finalMatch.Winner.Value))
                {
                    sb.AppendLine("\nCHAMPION:");
                    sb.AppendLine(new string('-', 50));
                    sb.AppendLine($"🏆 {teamNames[finalMatch.Winner.Value]}");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Creates an enhanced ASCII art visualization of the complete tournament bracket
        /// </summary>
        public static string VisualizeCompleteBracket(List<MatchNode>[] rounds, Dictionary<Guid, string> teamNames)
        {
            var sb = new StringBuilder();

            sb.AppendLine("COMPLETE TOURNAMENT BRACKET:");
            sb.AppendLine(new string('=', 80));

            // Calculate the maximum team name length for consistent formatting
            int maxNameLength = 15; // Reasonable default
            if (teamNames != null && teamNames.Count > 0)
            {
                maxNameLength = Math.Min(20, teamNames.Values.Max(n => n.Length));
            }

            // Handle reasonably sized brackets (up to 32 teams, 5 rounds)
            if (rounds.Length <= 5)
            {
                // Process each round
                for (int r = 0; r < rounds.Length; r++)
                {
                    var matches = rounds[r];
                    
                    // Add round header
                    sb.AppendLine($"\nRound {r + 1} ({GetRoundName(r, rounds.Length)}):");
                    sb.AppendLine(new string('-', 40));

                    // Draw matches with consistent formatting
                    for (int i = 0; i < matches.Count; i++)
                    {
                        var match = matches[i];
                        string teamA = GetShortName(match.TeamA, teamNames, maxNameLength);
                        string teamB = match.TeamB.HasValue 
                            ? GetShortName(match.TeamB, teamNames, maxNameLength)
                            : "BYE".PadRight(maxNameLength);

                        // Mark winner with visual indicator
                        if (match.Winner.HasValue)
                        {
                            if (match.Winner.Equals(match.TeamA))
                            {
                                teamA = "→ " + teamA;
                                teamB = "  " + teamB;
                            }
                            else
                            {
                                teamA = "  " + teamA;
                                teamB = "→ " + teamB;
                            }
                        }

                        sb.AppendLine($"  {teamA} vs {teamB}");
                    }
                }

                // Display final champion
                if (rounds.Length > 0 && rounds[rounds.Length - 1].Count > 0)
                {
                    var finalMatch = rounds[rounds.Length - 1][0];
                    if (finalMatch.Winner.HasValue && teamNames != null && teamNames.ContainsKey(finalMatch.Winner.Value))
                    {
                        sb.AppendLine("\nCHAMPION:");
                        sb.AppendLine(new string('=', 30));
                        sb.AppendLine($"🏆 {teamNames[finalMatch.Winner.Value]}");
                    }
                }
            }
            else
            {
                sb.AppendLine("Tournament bracket is too large for detailed visual representation.");
                sb.AppendLine($"Tournament has {rounds.Length} rounds with {rounds.Sum(r => r.Count)} total matches.");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets the appropriate round name based on position and total rounds
        /// </summary>
        private static string GetRoundName(int roundIndex, int totalRounds)
        {
            if (roundIndex == totalRounds - 1) return "Final";
            if (roundIndex == totalRounds - 2) return "Semi-Finals";
            if (roundIndex == totalRounds - 3) return "Quarter-Finals";
            if (roundIndex == 0 && totalRounds > 3) return "Round of 16";
            if (roundIndex == 0 && totalRounds > 2) return "First Round";
            return $"Round {roundIndex + 1}";
        }

        /// <summary>
        /// Gets a shortened team name for consistent formatting in brackets
        /// </summary>
        private static string GetShortName(Guid? teamId, Dictionary<Guid, string> teamNames, int maxLength)
        {
            if (!teamId.HasValue) return "BYE".PadRight(maxLength);

            if (teamNames != null && teamNames.ContainsKey(teamId.Value))
            {
                string name = teamNames[teamId.Value];
                if (name.Length <= maxLength)
                    return name.PadRight(maxLength);
                else
                    return name.Substring(0, maxLength - 3) + "..."; 
            }

            return $"Team ({teamId.Value.ToString().Substring(0, 8)})".PadRight(maxLength);
        }

        /// <summary>
        /// Gets the display name for a team, with fallback options
        /// </summary>
        private static string GetTeamName(Guid? teamId, Dictionary<Guid, string> teamNames, string defaultPrefix)
        {
            if (!teamId.HasValue)
                return "[null]";

            if (teamNames != null && teamNames.ContainsKey(teamId.Value))
                return teamNames[teamId.Value];

            return $"{defaultPrefix} ({teamId.Value.ToString().Substring(0, 8)}...)";
        }
    }
}