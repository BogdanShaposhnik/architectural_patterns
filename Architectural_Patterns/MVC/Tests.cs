using MVCPattern;
using Xunit;

namespace MVCPattern.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public void UpdateView_DisplaysUserDetails()
        {
            // Arrange
            var userModel = new UserModel();
            var userView = new UserView();
            var userController = new UserController(userModel, userView);
            userModel.Name = "John Doe";
            userModel.Age = 30;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            // Act
            userController.UpdateView();

            // Assert
            // Verify that the expected user details are displayed
            // We can capture the output using a StringWriter and assert against it,
            // but for simplicity, we'll just check if the output contains the expected values
            var output = stringWriter.ToString();
            Assert.Contains("User Details:", output);
            Assert.Contains("Name: John Doe", output);
            Assert.Contains("Age: 30", output);
        }

        [Fact]
        public void SetUserDetails_UpdatesModel()
        {
            // Arrange
            var userModel = new UserModel();
            var userView = new UserView();
            var userController = new UserController(userModel, userView);

            // Act
            userController.SetUserDetails("Jane Smith", 25);

            // Assert
            // Verify that the model has been updated with the new user details
            Assert.Equal("Jane Smith", userModel.Name);
            Assert.Equal(25, userModel.Age);
        }
    }
}
