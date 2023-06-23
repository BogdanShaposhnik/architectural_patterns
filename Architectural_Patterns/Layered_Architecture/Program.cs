using System;
using System.Collections.Generic;

namespace LayeredArchitectureExample
{
    // Data Model
    public class DataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // Data Access Layer
    public class DataAccess
    {
        private List<DataModel> _data;

        public DataAccess()
        {
            _data = new List<DataModel>
            {
                new DataModel { Id = 1, Name = "Data 1" },
                new DataModel { Id = 2, Name = "Data 2" },
                new DataModel { Id = 3, Name = "Data 3" }
            };
        }

        public List<DataModel> GetData()
        {
            // Simulated data retrieval from a data source
            return _data;
        }

        public void SaveData(DataModel newData)
        {
            // Simulated data saving to a data source
            _data.Add(newData);
        }
    }

    // Business Logic Layer
    public class BusinessLogic
    {
        private readonly DataAccess _dataAccess;

        public BusinessLogic(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<DataModel> ProcessData()
        {
            // Perform business logic operations
            List<DataModel> data = _dataAccess.GetData();
            // Additional processing...

            return data;
        }

        public void AddData(string name)
        {
            // Create a new data model
            DataModel newData = new DataModel { Id = DateTime.Now.Millisecond, Name = name };

            // Save the new data
            _dataAccess.SaveData(newData);
        }
    }

    // Presentation Layer
    class Program
    {
        static void Main(string[] args)
        {
            DataAccess dataAccess = new DataAccess();
            BusinessLogic businessLogic = new BusinessLogic(dataAccess);

            // Call the business logic layer
            List<DataModel> data = businessLogic.ProcessData();

            // Display the data
            Console.WriteLine("Existing Data:");
            foreach (DataModel item in data)
            {
                Console.WriteLine($"{item.Id} - {item.Name}");
            }

            Console.WriteLine("---------------------------------------");

            // Prompt the user to add new data
            Console.WriteLine("Enter new data:");
            string newData = Console.ReadLine();
            businessLogic.AddData(newData);

            Console.WriteLine("New Data Added!");

            Console.WriteLine("---------------------------------------");

            // Display the updated data
            data = businessLogic.ProcessData();
            Console.WriteLine("Updated Data:");
            foreach (DataModel item in data)
            {
                Console.WriteLine($"{item.Id} - {item.Name}");
            }

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
