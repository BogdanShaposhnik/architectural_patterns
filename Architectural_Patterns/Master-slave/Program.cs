using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace MasterSlavePattern
{
    class Master
    {
        private ConcurrentQueue<string> taskQueue;

        public Master(ConcurrentQueue<string> taskQueue)
        {
            this.taskQueue = taskQueue;
        }

        public void Start()
        {
            Console.WriteLine("Master thread started.");

            // Enqueue tasks
            for (int i = 1; i <= 10; i++)
            {
                string task = $"Task {i}";
                taskQueue.Enqueue(task);
                Console.WriteLine($"Master: Enqueued task '{task}'");
                Thread.Sleep(1000); // Simulate delay between tasks
            }

            Console.WriteLine("Master thread finished.");
        }
    }

    class Slave
    {
        private ConcurrentQueue<string> taskQueue;

        public Slave(ConcurrentQueue<string> taskQueue)
        {
            this.taskQueue = taskQueue;
        }

        public void Start()
        {
            Console.WriteLine("Slave thread started.");

            while (!taskQueue.IsEmpty)
            {
                if (taskQueue.TryDequeue(out string task))
                {
                    Console.WriteLine($"Slave: Processing task '{task}'");
                    Thread.Sleep(2000); // Simulate task processing time
                }
            }

            Console.WriteLine("Slave thread finished.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Master-Slave Pattern Example");

            // Create a shared task queue
            var taskQueue = new ConcurrentQueue<string>();

            // Create the master and slave instances
            var master = new Master(taskQueue);
            var slave = new Slave(taskQueue);

            // Create the master and slave tasks
            var masterTask = Task.Run(() => master.Start());
            var slaveTask = Task.Run(() => slave.Start());

            // Wait for the tasks to complete
            Task.WaitAll(masterTask, slaveTask);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
