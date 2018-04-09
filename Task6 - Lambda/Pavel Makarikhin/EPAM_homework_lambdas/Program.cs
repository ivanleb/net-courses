using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_lambdas
{
    class Program
    {
        static void Main(string[] args)
        {
            RedClient redClient = new RedClient();
            BlueClient blueClient = new BlueClient();
            GreenClient greenClient = new GreenClient();

            NumberGenerator generator = new NumberGenerator();

            // x % 2 == 0 && x % 3 == 0
            generator.Subscribe(redClient.onNumberReceived, new List<Func<int, bool>>() { (x) => { return x % 2 == 0; },
                (x) => {return x % 3 == 0; } });

            // x % 7 == 0 && x % 2 != 0
            generator.Subscribe(blueClient.onNumberReceived, new List<Func<int, bool>>() { (x) => { return x % 7 == 0; },
                (x) => {return x % 2 != 0; } });

            // x % 5 == 0 && x % 2 != 0 && x % 3 != 0 && x % 13 != 0
            generator.Subscribe(greenClient.onNumberReceived, new List<Func<int, bool>>() { (x) => { return x % 5 == 0; },
                (x) => {return x % 2 != 0; }, (x) => {return x % 3 != 0; }, (x) => {return x % 13 != 0; } });

            generator.GenerateNumbers(1000);

            Console.ReadKey();
        }
    }
}
