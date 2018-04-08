using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambdas
{
    class Program
    {
        static void Main(string[] args)
        {
            NumberGenerator ng = new NumberGenerator();
            ng.Subscribe(x => Console.WriteLine("Client1 got " + x), null);
            var filter = new List<Func<int, bool>>() { (x) => { return x % 3 == 0; }, (x) => { return x % 5 == 0; } };
            ng.Subscribe(x=>Console.WriteLine("Client2: This number is a multiple of 15. That's why i got "+x), filter);
            ng.Generate(20);
            Console.ReadKey(true);
        }
    }
}
