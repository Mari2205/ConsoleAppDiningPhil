using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDiningPhilTest
{
    public class Frok
    {
        private static int count = 1;
        public string Name { get; set; }

        public Frok()
        {
            this.Name = "Frok " + count++;
        }
    }
}
