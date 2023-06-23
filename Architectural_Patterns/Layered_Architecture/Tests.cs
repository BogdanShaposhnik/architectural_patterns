using System.Collections.Generic;
using Xunit;

namespace LayeredArchitectureExample.Tests
{
    public class BusinessLogicTests
    {
        [Fact]
        public void ProcessData_ReturnsData()
        {
            // Arrange
            var dataAccess = new DataAccess();
            var businessLogic = new BusinessLogic(dataAccess);

            // Act
            List<DataModel> result = businessLogic.ProcessData();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void AddData_SavesNewData()
        {
            // Arrange
            var dataAccess = new DataAccess();
            var businessLogic = new BusinessLogic(dataAccess);
            var newData = "New Data";

            // Act
            businessLogic.AddData(newData);
            List<DataModel> result = businessLogic.ProcessData();

            // Assert
            Assert.Equal(4, result.Count);
            Assert.Contains(result, d => d.Name == newData);
        }
    }
}
