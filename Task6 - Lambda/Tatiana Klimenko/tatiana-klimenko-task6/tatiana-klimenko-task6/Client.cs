using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tatiana_klimenko_task6
{
    class Client
    {
        public string Name { get; set; }

        public Client(string name)
        {
            this.Name = name;
        }

        public void HandleOutput(int x)
        {
            Console.WriteLine($"{Name} - {x}");
        }
    }
}
