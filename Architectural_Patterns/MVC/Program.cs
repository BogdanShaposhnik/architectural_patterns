using System;
using System.Collections.Generic;

namespace MVCPattern
{
    // Model: Represents the data and business logic of the application
    public class UserModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    // View: Responsible for displaying the user interface and interacting with the user
    public class UserView
    {
        public void DisplayUserDetails(string name, int age)
        {
            Console.WriteLine("User Details:");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Age: {age}");
        }
    }

    // Controller: Acts as an intermediary between the Model and the View, handles user input and updates the Model and View accordingly
    public class UserController
    {
        private UserModel model;
        private UserView view;

        public UserController(UserModel model, UserView view)
        {
            this.model = model;
            this.view = view;
        }

        public void SetUserDetails(string name, int age)
        {
            model.Name = name;
            model.Age = age;
        }

        public void UpdateView()
        {
            view.DisplayUserDetails(model.Name, model.Age);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create the Model, View, and Controller
            var userModel = new UserModel();
            var userView = new UserView();
            var userController = new UserController(userModel, userView);

            // Set the initial user details
            userController.SetUserDetails("John Doe", 30);

            // Update and display user details
            userController.UpdateView();

            // Modify the user details
            userController.SetUserDetails("Jane Smith", 25);

            // Update and display user details again
            userController.UpdateView();

            Console.ReadKey();
        }
    }
}
