using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGeneratorLambda
{
    class Client
    {
        public string Name { get; set; }
        public Client(string name)
        {
            Name = name;
        }

        public void HandleNumbers(int x)
        {
            Console.WriteLine($"Client {Name} get {x}");
        }
    }
}
