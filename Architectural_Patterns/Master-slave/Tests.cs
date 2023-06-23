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
            var taskQueue = new ConcurrentQueue<string>();
            var master = new Master(taskQueue);
            var slave = new Slave(taskQueue);

            // Act
            var masterTask = Task.Run(() => master.Start());
            var slaveTask = Task.Run(() => slave.Start());

            Task.WaitAll(masterTask, slaveTask);

            // Assert
            Assert.True(taskQueue.IsEmpty);
        }
    }
}
