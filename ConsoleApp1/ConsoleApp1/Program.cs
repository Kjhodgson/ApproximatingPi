using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MonteCarloPiApproximation
{
    class Program
    {
        // Enviroment will grab the number of cores, will help to put things in parallel.
        static int numberOfCores = Environment.ProcessorCount;
        static int iterations = 10000000 * numberOfCores;

        static void Main(string[] args)
        {
            Console.WriteLine("Monte Carlo Method for approximating Pi");
            Console.WriteLine("Number of Iterations (Based off of cores): " + iterations);
            Console.WriteLine("Number of processor cores on system: " + Environment.ProcessorCount);

            // Stopwatch is used to track the amount of time that is passed during a certain operation. 
            var sw = new Stopwatch();
            sw.Start();

            Console.WriteLine("\nRunning the Method for approximating pi without parallelism:");

            MonteCarloPiApproximation();

            Console.WriteLine("Elasped time: (time in ms)" + sw.ElapsedMilliseconds);
            long timeSeconds = sw.ElapsedMilliseconds / 1000;
            Console.WriteLine("\nTime in seconds:" + timeSeconds);

            sw.Stop();
            sw.Start();

            Console.WriteLine("\nNow running the program with parallelism");

            MonetCarloPiParallel();

            Console.WriteLine("Elasped time: (time in ms)" + sw.ElapsedMilliseconds);
            timeSeconds = sw.ElapsedMilliseconds / 1000;
            Console.WriteLine("\nTime in seconds:" + timeSeconds);

            sw.Stop(); 

            Console.WriteLine("The program is done, please press a key to exit!");
            Console.ReadKey();
        }

        private static void MonteCarloPiApproximation()
        {
            double piApprox = 0;
            int total = 0;
            int inCircle = 0;
            double x = 0;
            double y = 0;
            Random rand = new Random();

            while (total < iterations)
            {
                x = rand.NextDouble();
                y = rand.NextDouble();

                if ((Math.Sqrt((x * x) + (y * y)) <= 1.0)) // <= 1 implies it is in the circle, anything outside is discarded but counted
                    inCircle++;

                total++;
                piApprox = 4 * ((double)inCircle / (double)total);
            }
          
            Console.WriteLine("\nApproximated Pi = {0}", piApprox);
        }

        private static void MonetCarloPiParallel()
        {
            double piApprox = 0;
            int TotalinCircle = 0;
            
            double x = 0;
            double y = 0;

            int[] coreCounts = new int[numberOfCores];
            Task[] tasks = new Task[numberOfCores];

            // This will seperate the number of approximations needed among the different number of cores that a computer has
            for (int i = 0; i < numberOfCores; i++)
            {
                // Corenumber is used here because of how the task functions in accordance to the loop. It stores the loop iterations so that the count can be properly placed.
                int coreNumber = i;
                tasks[coreNumber] = Task.Factory.StartNew(() =>
                {
                    int taskInCircle = 0;

                    // For loop used to divided the work among the cores. 
                    for (int j = 0; j < iterations / numberOfCores; j++)
                    {
                        
                        x = new Random().NextDouble();
                        y = new Random().NextDouble();
                        if ((Math.Sqrt((x * x) + (y * y)) <= 1.0)) // <= 1 implies it is in the circle, anything outside is discarded but counted
                            taskInCircle++;
                    }
                    coreCounts[coreNumber] = taskInCircle;
                });
               
            }
            // Needed for ending the tasks or they will continue to run in background.....
            Task.WaitAll(tasks);
            TotalinCircle = coreCounts.Sum();

            piApprox = 4 * ((double)TotalinCircle / (double)iterations);
            Console.WriteLine("\nApproximated Pi = {0}", piApprox);
        }



    }
}
