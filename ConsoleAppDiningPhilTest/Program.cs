using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace ConsoleAppDiningPhilTest
{
    class Program
    {
        private const int philosopherCount = 5;
        static void Main(string[] args)
        {
            var philosophers = InitializePhilosophers();

            Console.WriteLine("Dinner is starting!");

            var philosopherThreads = new List<Thread>();
            foreach (var philosopher in philosophers)
            {
                var philosopherThread = new Thread(new ThreadStart(philosopher.EatAll));
                philosopherThreads.Add(philosopherThread);
                philosopherThread.Start();
            }

            foreach (var thread in philosopherThreads)
            {
                thread.Join();
            }

            Console.WriteLine("Dinner is over!");
            Console.ReadKey();
        }

        private static List<Philosopher> InitializePhilosophers()
        {
            var philosophers = new List<Philosopher>(philosopherCount);
            for (int i = 0; i < philosopherCount; i++)
            {
                philosophers.Add(new Philosopher(philosophers, i));
            }

            foreach (var philosopher in philosophers)
            {
                philosopher.LeftFrok = philosopher.LeftPhilosopher.RightFrok ?? new Frok();

                philosopher.RightFrok = philosopher.RightPhilosopher.LeftFrok ?? new Frok();
            }

            return philosophers;
        }
    }
}
