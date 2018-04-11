using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator generator = new Generator();

            FirstClient firstClient = new FirstClient();
            SecondClient secondClient = new SecondClient();

            generator.Subscribe(firstClient.onNumReceived, new List<Func<int, bool>>() { (x) => { return x % 2 == 0; } });

            generator.Subscribe(secondClient.onNumReceived, new List<Func<int, bool>>() { (x) => {return x % 2 != 0; } });

            generator.NumGenerate(50);

            Console.ReadKey();
        }
    }
}
