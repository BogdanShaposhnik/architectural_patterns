using System;
using System.Collections.Generic;

namespace BlackboardPattern
{
    // Knowledge Source: Represents a module that contributes knowledge to the Blackboard
    public class KnowledgeSource
    {
        public void ProcessData(Blackboard blackboard)
        {
            // Access the data on the blackboard and perform some processing
            string data = blackboard.GetData();
            string processedData = $"Processed: {data}";

            // Update the blackboard with the processed data
            blackboard.SetData(processedData);

            Console.WriteLine("Knowledge Source processed data.");
        }
    }

    // Blackboard: Central repository that holds the shared knowledge
    public class Blackboard
    {
        private string data;

        public void SetData(string newData)
        {
            data = newData;
        }

        public string GetData()
        {
            return data;
        }
    }

    // Controller: Orchestrates the interaction between knowledge sources and the blackboard
    public class Controller
    {
        private List<KnowledgeSource> knowledgeSources;
        private Blackboard blackboard;

        public Controller()
        {
            knowledgeSources = new List<KnowledgeSource>();
            blackboard = new Blackboard();
        }

        public void RegisterKnowledgeSource(KnowledgeSource knowledgeSource)
        {
            knowledgeSources.Add(knowledgeSource);
        }

        public void Run()
        {
            foreach (var knowledgeSource in knowledgeSources)
            {
                knowledgeSource.ProcessData(blackboard);
            }

            // Access the final processed data from the blackboard
            string finalData = blackboard.GetData();
            Console.WriteLine($"Final Data: {finalData}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create the controller and knowledge sources
            var controller = new Controller();
            var knowledgeSource1 = new KnowledgeSource();
            var knowledgeSource2 = new KnowledgeSource();

            // Register the knowledge sources with the controller
            controller.RegisterKnowledgeSource(knowledgeSource1);
            controller.RegisterKnowledgeSource(knowledgeSource2);

            // Run the controller to initiate knowledge processing
            controller.Run();

            Console.ReadKey();
        }
    }
}
