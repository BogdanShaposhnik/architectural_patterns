using BlackboardPattern;
using Xunit;

namespace BlackboardPattern.Tests
{
    public class KnowledgeSourceTests
    {
        [Fact]
        public void ProcessData_UpdatesBlackboardWithData()
        {
            // Arrange
            var knowledgeSource = new KnowledgeSource();
            var blackboard = new Blackboard();
            blackboard.SetData("Initial Data");

            // Act
            knowledgeSource.ProcessData(blackboard);

            // Assert
            // Verify that the blackboard data is correctly updated after processing
            Assert.Equal("Processed: Initial Data", blackboard.GetData());
        }
    }

    public class BlackboardTests
    {
        [Fact]
        public void SetData_UpdatesBlackboardWithData()
        {
            // Arrange
            var blackboard = new Blackboard();

            // Act
            blackboard.SetData("New Data");

            // Assert
            // Verify that the blackboard data is correctly set
            Assert.Equal("New Data", blackboard.GetData());
        }

        [Fact]
        public void GetData_ReturnsBlackboardData()
        {
            // Arrange
            var blackboard = new Blackboard();
            blackboard.SetData("Test Data");

            // Act
            string data = blackboard.GetData();

            // Assert
            // Verify that the correct data is returned from the blackboard
            Assert.Equal("Test Data", data);
        }
    }
}
