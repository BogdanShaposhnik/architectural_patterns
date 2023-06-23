using System;
using System.Collections.Generic;
using System.Linq;

namespace PipeFilterPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pipe-Filter Pattern Example");

            // Create a list of numbers
            List<int> numbers = Enumerable.Range(1, 10).ToList();

            // Create filters
            var filter1 = new MultiplyFilter(2);
            var filter2 = new SubtractFilter(5);

            // Create the pipeline
            var pipeline = new Pipeline<int>();

            // Add filters to the pipeline
            pipeline.Register(filter1);
            pipeline.Register(filter2);

            // Process the numbers through the pipeline
            List<int> results = pipeline.Process(numbers);

            // Print the results
            Console.WriteLine("Results:");
            foreach (int result in results)
            {
                Console.WriteLine(result);
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }

    // Filter interface
    interface IFilter<T>
    {
        T Process(T input);
    }

    // Multiply filter
    class MultiplyFilter : IFilter<int>
    {
        private int factor;

        public MultiplyFilter(int factor)
        {
            this.factor = factor;
        }

        public int Process(int input)
        {
            int result = input * factor;
            Console.WriteLine($"MultiplyFilter: {input} * {factor} = {result}");
            return result;
        }
    }

    // Subtract filter
    class SubtractFilter : IFilter<int>
    {
        private int value;

        public SubtractFilter(int value)
        {
            this.value = value;
        }

        public int Process(int input)
        {
            int result = input - value;
            Console.WriteLine($"SubtractFilter: {input} - {value} = {result}");
            return result;
        }
    }

    // Pipeline class
    class Pipeline<T>
    {
        private List<IFilter<T>> filters;

        public Pipeline()
        {
            filters = new List<IFilter<T>>();
        }

        public void Register(IFilter<T> filter)
        {
            filters.Add(filter);
        }

        public List<T> Process(List<T> input)
        {
            List<T> output = new List<T>(input);

            foreach (IFilter<T> filter in filters)
            {
                output = output.Select(filter.Process).ToList();
            }

            return output;
        }
    }
}
