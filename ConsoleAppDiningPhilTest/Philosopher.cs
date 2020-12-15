using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace ConsoleAppDiningPhilTest
{
    public class Philosopher
    {
        public string ThreadName { get; set; }
        public Frok LeftFrok { get; set; }
        public Frok RightFrok { get; set; }

        Random random = new Random();
        private  List<Philosopher> allPhilosophersList;
        private  int index;


        public Philosopher(List<Philosopher> allPhilosophers, int index)
        {
            allPhilosophersList = allPhilosophers;
            this.index = index;
            this.ThreadName = string.Format($"Philosopher {this.index}");
        }

        public Philosopher LeftPhilosopher
        {
            get
            {
                if (index == 0)
                    return allPhilosophersList[allPhilosophersList.Count - 1];
                else
                    return allPhilosophersList[index - 1];
            }
        }

        public Philosopher RightPhilosopher
        {
            get
            {
                if (index == allPhilosophersList.Count - 1)
                    return allPhilosophersList[0];
                else
                    return allPhilosophersList[index + 1];
            }
        }

        public void EatAll()
        {
            while (true)
            {
                this.Think();
                if (this.PickUp())
                {
                    this.Eat();

                    this.PutDownLeft();
                    this.PutDownRight();
                }
            }
        }

        private bool PickUp()
        {
            if (Monitor.TryEnter(this.LeftFrok))
            {
                Console.WriteLine(this.ThreadName + " picks up left Fork.");

                if (Monitor.TryEnter(this.RightFrok))
                {
                    Console.WriteLine(this.ThreadName + " picks up right Fork.");
                    return true;
                }
                else
                {
                    this.PutDownLeft();
                }
            }
            return false;
        }

        private void Eat()
        {
            Thread.Sleep(random.Next(3000));
            Console.WriteLine(this.ThreadName + " eats.");
        }

        private void PutDownLeft()
        {
            Monitor.Exit(this.LeftFrok);
            Console.WriteLine(this.ThreadName + " puts down left Fork.");
        }

        private void PutDownRight()
        {
            Monitor.Exit(this.RightFrok);
            Console.WriteLine(this.ThreadName + " puts down right Fork.");
        }


        private void Think()
        {
            Thread.Sleep(random.Next(2000));
        }
    }
}

