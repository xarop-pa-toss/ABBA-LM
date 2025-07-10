namespace BloodTourney.Tests
{
    public class RandomExtensionsTests
    {
        [Fact]
        public void Shuffle_ReordersArray()
        {
            // Arrange
            var original = Enumerable.Range(1, 10).ToArray();
            var copy = (int[])original.Clone();

            // Act
            new Random(42).Shuffle(copy); // Use a fixed seed for predictability

            // Assert
            Assert.NotEqual(original.ToArray(), copy.ToArray());
        }

        [Fact]
        public void Shuffle_PreservesElements()
        {
            // Arrange
            var original = new[] { 1, 2, 3, 4, 5 };
            var copy = (int[])original.Clone();

            // Act
            new Random().Shuffle(copy);

            // Assert
            Assert.Equal(original.OrderBy(x => x), copy.OrderBy(x => x));
        }

        [Fact]
        public void Shuffle_HandlesEmptyArray()
        {
            // Arrange
            var empty = Array.Empty<int>();

            // Act & Assert (should not throw)
            new Random().Shuffle(empty);

            // Assert
            Assert.Empty(empty);
        }

        [Fact]
        public void Shuffle_HandlesSingleElementArray()
        {
            // Arrange
            var single = new[] { 42 };

            // Act
            new Random().Shuffle(single);

            // Assert
            Assert.Single(single);
            Assert.Equal(42, single[0]);
        }

        [Fact]
        public void Shuffle_DifferentRandomSeeds_ProduceDifferentResults()
        {
            // Arrange
            var original = Enumerable.Range(1, 20).ToArray();
            var copy1 = (int[])original.Clone();
            var copy2 = (int[])original.Clone();

            // Act - use different seeds
            new Random(1).Shuffle(copy1);
            new Random(2).Shuffle(copy2);

            // Assert
            Assert.NotEqual(copy1, copy2);
        }
    }
}
