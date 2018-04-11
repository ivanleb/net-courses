using System;
using System.Collections.Generic;

namespace Lambdas
{
    class Program
    {
        static void Main(string[] args)
        {
            NumberGenerator numberGenerator = new NumberGenerator();
            numberGenerator.Subscribe(x => Console.WriteLine($"Client 1 got {x}"), null);
            numberGenerator.Subscribe(x => Console.WriteLine($"Client 2 got {x} because it's a multiple of 6"),
                                      new List<Func<int, bool>> { x => x % 2 == 0, x => x % 3 == 0 });
            numberGenerator.Generate(15);
        }
    }
}
