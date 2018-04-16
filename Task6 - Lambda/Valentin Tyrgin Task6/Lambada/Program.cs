using System;
using System.Collections.Generic;
using System.Linq;

namespace Lambada
{
    internal class NumberGenerator
    {
        public NumberGenerator(int number)
        {
            Number = number;
        }

        public int Number { get; set; }

        internal void Subscriber(Action<int> Handler, IEnumerable<Predicate<int>> Filters)
        {
            for (var i = 0; i <= Number; i++)
                if (Filters.All(filter => filter(i))) Handler(i);
        }
    }
    internal class Program
    {
        private static void Main(string[] args)
        {
            var numGen = new NumberGenerator(500);

            numGen.Subscriber(
                x=> Console.WriteLine($"User_A received number {x}"),
                new List<Predicate<int>>
                {
                   x=>x%3==0&&x%4==0,
                   x=>x.ToString().StartsWith("1"),
                   //сумма цифр больше 10
                   x =>
                    {
                        var sum = 0;
                        while (x > 0)
                        {
                            sum += x % 10;
                            x /= 10;
                        }
                        return sum > 10;
                    }
                });

            numGen.Subscriber(x=>Console.WriteLine($"User_B received number {x}"),
                new List<Predicate<int>>
                { x=>x>10&&x.ToString().Distinct().Count()==1});
            Console.ReadLine();
        }
    }
}