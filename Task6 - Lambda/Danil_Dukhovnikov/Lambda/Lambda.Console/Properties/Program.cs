using System;
using System.Collections.Generic;

namespace Lambda.Console.Properties
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var numberGenerator = new NumberGenerator();
            numberGenerator.Subscribe(x => System.Console.WriteLine($"Client 1 got {x}"), null);
            
            var filters = new List<Func<int, bool>>()
            {
                (x) => x % 3 == 0,
                (x) => x % 4 == 0
            };

            numberGenerator.Subscribe(
                x => System.Console.WriteLine($"Client 2 got {x}. It's a multiple of 12"),
                filters
                );
            
            numberGenerator.Generate(35);
            System.Console.ReadKey();
        }
    }
}