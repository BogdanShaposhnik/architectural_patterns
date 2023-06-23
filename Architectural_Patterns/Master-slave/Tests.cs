using System.Collections.Concurrent;
using Xunit;

namespace MasterSlavePattern.Tests
{
    public class MasterSlaveTests
    {
        [Fact]
        public void TestMasterSlavePattern()
        {
            // Arrange
            var master = new Master();

            master.AddTask("Task 1");
            master.AddTask("Task 2");
            master.AddTask("Task 2");

            // Act
            master.ProcessTasks(3);

            // Assert
            Assert.True(master.tasksQueue.IsEmpty);
        }
    }
}
