using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MonteCarloPiApproximation
{
    class Program
    {
        // Enviroment will grab the number of cores, will help to put things in parallel.
        static int numberOfCores = Environment.ProcessorCount;
        static int iterations = 60000000 * numberOfCores;

        static void Main(string[] args)
        {
            Console.WriteLine("Monte Carlo Method for approximating Pi");
            Console.WriteLine("Number of Iterations (Based off of cores): " + iterations);
            Console.WriteLine("Number of processor cores on system: " + Environment.ProcessorCount);

            // Stopwatch is used to track the amount of time that is passed during a certain operation. 
            var sw = new Stopwatch();
            sw.Start();

            Console.WriteLine("\nRunning the Method for approximating pi:");

            MonteCarloPiApproximation();

            Console.WriteLine("Running program without parallelism: (time in ms)" + sw.ElapsedMilliseconds);
            long timeSeconds = sw.ElapsedMilliseconds / 1000;
            Console.WriteLine("\nTime in seconds:" + timeSeconds);

            sw.Stop();

            Console.WriteLine("Press Enter Key");
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
            int total = 0;
            int inCircle = 0;
            double x = 0;
            double y = 0;
            Random rand = new Random();

            // Need to figure out how to seperate tasks for the different cores. 

            int[] coreCounts = new int[numberOfCores];
            Task[] tasks = new Task[numberOfCores];

            // Need to find out if creating seperate tasks equal to the number of cores will force the cores to take the seperate tasks. 


        }



    }
}
