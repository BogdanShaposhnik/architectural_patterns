using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace MasterSlavePattern
{
    // Master class
    public class Master
    {
        public readonly ConcurrentQueue<string> tasksQueue = new ConcurrentQueue<string>();
        public int slaveCount;
        public volatile bool isStopped;

        public ConcurrentQueue<string> TasksQueue => tasksQueue;
        public ConcurrentBag<string> ProcessedTasks { get; } = new ConcurrentBag<string>();

        public void AddTask(string task)
        {
            Console.WriteLine($"Master adds task: {task}");
            Thread.Sleep(100);
            tasksQueue.Enqueue(task);
        }

        public void ProcessTasks(int slaveCount)
        {
            this.slaveCount = slaveCount;
            var slaves = new Task[slaveCount];

            // Start the slave tasks
            for (int i = 0; i < slaveCount; i++)
            {
                slaves[i] = Task.Run(() => SlaveTask());
            }

            // Wait for all tasks to be processed
            Task.WaitAll(slaves);
        }

        private void SlaveTask()
        {
            while (!isStopped && tasksQueue.TryDequeue(out string task))
            {
                // Process the task
                Console.WriteLine($"Slave {Task.CurrentId} processing task: {task}");
                Thread.Sleep(1000);

                // Store the processed task
                ProcessedTasks.Add(task);
            }
        }
    }

    // Slave class
    public class Slave
    {
        private readonly Master master;
        private readonly int id;

        public Slave(Master master, int id)
        {
            this.master = master;
            this.id = id;
        }

        public void Start()
        {
            while (!master.TasksQueue.IsEmpty)
            {
                if (master.TasksQueue.TryDequeue(out string task))
                {
                    // Process the task
                    Console.WriteLine($"Slave {id} processing task: {task}");
                    Thread.Sleep(1000);

                    // Store the processed task
                    master.ProcessedTasks.Add(task);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var master = new Master();

            // Add tasks to the master
            master.AddTask("Task 1");
            master.AddTask("Task 2");
            master.AddTask("Task 2");

            // Process tasks using 3 slave threads
            master.ProcessTasks(3);

            Console.WriteLine("All tasks processed. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
