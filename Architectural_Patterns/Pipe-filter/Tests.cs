using System.Collections.Generic;
using Xunit;

namespace PipeFilterPattern.Tests
{
    public class PipeFilterTests
    {
        [Fact]
        public void TestMultiplyFilter()
        {
            // Arrange
            var filter = new MultiplyFilter(3);
            int input = 5;

            // Act
            int result = filter.Process(input);

            // Assert
            Assert.Equal(15, result);
        }

        [Fact]
        public void TestSubtractFilter()
        {
            // Arrange
            var filter = new SubtractFilter(5);
            int input = 12;

            // Act
            int result = filter.Process(input);

            // Assert
            Assert.Equal(7, result);
        }

        [Fact]
        public void TestEmptyPipeline()
        {
            // Arrange
            var pipeline = new Pipeline<int>();
            var numbers = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            List<int> results = pipeline.Process(numbers);

            // Assert
            Assert.Equal(numbers, results);
        }

        [Fact]
        public void TestSingleFilterPipeline()
        {
            // Arrange
            var pipeline = new Pipeline<int>();
            var filter = new MultiplyFilter(2);
            pipeline.Register(filter);
            var numbers = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            List<int> results = pipeline.Process(numbers);

            // Assert
            Assert.Equal(new List<int> { 2, 4, 6, 8, 10 }, results);
        }

        [Fact]
        public void TestMultipleFiltersPipeline()
        {
            // Arrange
            var pipeline = new Pipeline<int>();
            var filter1 = new MultiplyFilter(2);
            var filter2 = new SubtractFilter(3);
            pipeline.Register(filter1);
            pipeline.Register(filter2);
            var numbers = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            List<int> results = pipeline.Process(numbers);

            // Assert
            Assert.Equal(new List<int> { -1, 1, 3, 5, 7 }, results);
        }
    }
}
