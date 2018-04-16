using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lambdas
{
    class Program
    {
        static void Main(string[] args)
        {
            var numberGenerator = new NumberGenerator();

            Console.WriteLine("Even:");
            numberGenerator.Subscribe((x) =>
            {
                numberGenerator.ShowNumber(x);
            }, new List<Func<int, bool>>()
            {
                (x) => {return x % 2 == 0; }
            });

            Console.WriteLine("\nDivisible by 3:");
            numberGenerator.Subscribe((x) =>
            {
                numberGenerator.ShowNumber(x);
            }, new List<Func<int, bool>>()
            {
                (x) => {return x % 3 == 0; }
            });

            Console.WriteLine("\nDivisible by 4 and 6:");
            numberGenerator.Subscribe((x) =>
            {
                numberGenerator.ShowNumber(x);
            }, new List<Func<int, bool>>()
            {
                (x) => {return x % 4 == 0; },
                (x) => {return x % 6 == 0; }
            });

            Console.ReadKey();
        }
    }
}
