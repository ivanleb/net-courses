using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGeneratorLambda
{
    class Program
    {
        static void Main(string[] args)
        {
            Client joshua = new Client("Joshua Abrams");
            Client wu = new Client("John Wu");
            Client linkoln = new Client("Abraham Linkoln");

            NumberGenerator ng = new NumberGenerator(200);

            //Подписываем клиентов, используя методы класса Client
            ng.SubscribeWithOneFilter((x) =>
            {
                joshua.HandleNumbers(x);
            },
            (x) => { return (x*2) % 7 == 0; });
            Console.ReadLine();

            ng.SubscribeWithOneFilter((x) =>
            {
                wu.HandleNumbers(x);
            },
            (x) => { return x < 10; });
            Console.ReadLine();

            ng.SubscribeWithSeveralFilters((x) =>
            {
                linkoln.HandleNumbers(x);
            },
            new List<Func<int, bool>>
            {
                (x) => { return x%2 ==0; },
                (x) => {return x%3 == 0; },
                (x) => {return Math.Pow(x,3) < 10000; }
            });
            Console.ReadLine();

            //Подписываем клиентов, используя анонимный метод
            ng.SubscribeWithOneFilter((x) =>
            {
                Console.WriteLine($"Fist client got {x}");
            },
            (x) => { return x % 10 ==0; });
            Console.ReadLine();

            ng.SubscribeWithOneFilter((x) =>
            {
                Console.WriteLine($"Second client got {x}");
            },
            (x) => { return x % 15 == 0; });
            Console.ReadLine();

            ng.SubscribeWithSeveralFilters((x) =>
            {
                Console.WriteLine($"Third client got {x}");
            },
            new List<Func<int, bool>>
            {
                (x) => { return x%2 ==0; },
                (x) => {return x%3 == 0; },
                (x) => {return (x+8)%10 == 0; }
            });
            Console.ReadLine();
        }
    }
}
