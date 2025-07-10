using BloodTourney.Tournament.Formats;

namespace BloodTourney.Tests
{
    public class MatchNodeTests
    {
        [Fact]
        public void MatchNode_BasicProperties()
        {
            // Arrange
            var teamA = Guid.NewGuid();
            var teamB = Guid.NewGuid();

            // Act
            var matchNode = new MatchNode
            {
                TeamA = teamA,
                TeamB = teamB
            };

            // Assert
            Assert.Equal(teamA, matchNode.TeamA);
            Assert.Equal(teamB, matchNode.TeamB);
            Assert.Null(matchNode.Winner);
            Assert.Null(matchNode.Loser);
            Assert.False(matchNode.TeamAAbandoned);
            Assert.False(matchNode.TeamBAbandoned);
        }

        [Fact]
        public void MatchNode_SetWinnerAndLoser()
        {
            // Arrange
            var teamA = Guid.NewGuid();
            var teamB = Guid.NewGuid();
            var matchNode = new MatchNode
            {
                TeamA = teamA,
                TeamB = teamB
            };

            // Act
            matchNode.Winner = teamA;
            matchNode.Loser = teamB;

            // Assert
            Assert.Equal(teamA, matchNode.Winner);
            Assert.Equal(teamB, matchNode.Loser);
        }

        [Fact]
        public void MatchNode_TeamAbandonment()
        {
            // Arrange
            var teamA = Guid.NewGuid();
            var teamB = Guid.NewGuid();
            var matchNode = new MatchNode
            {
                TeamA = teamA,
                TeamB = teamB
            };

            // Act
            matchNode.TeamAAbandoned = true;

            // Assert
            Assert.True(matchNode.TeamAAbandoned);
            Assert.False(matchNode.TeamBAbandoned);

            // When a team abandons, the other team should win by default
            // but this is handled by tournament logic, not the MatchNode itself
        }

        [Fact]
        public void MatchNode_WithBye()
        {
            // Arrange
            var teamA = Guid.NewGuid();

            // Act - Create a bye match (TeamB is null)
            var matchNode = new MatchNode
            {
                TeamA = teamA,
                TeamB = null
            };

            // Assert
            Assert.Equal(teamA, matchNode.TeamA);
            Assert.Null(matchNode.TeamB);
        }
    }
}
