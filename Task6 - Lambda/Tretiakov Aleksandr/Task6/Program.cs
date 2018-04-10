using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    class Program
    {
        static void Main(string[] args)
        {
            var client1 = new Client("First");
            var client2 = new Client("Second");
            var client3 = new Client("Third");
            var generator = new NumberGenerator();
            generator.Subscribe(x => client1.HandleEvent(x), x => x % 2 == 0);
            generator.Subscribe(x => client2.HandleEvent(x), x => x % 2 == 1);
            generator.Subscribe(x => client3.HandleEvent(x), new Func<int, bool>[] {
                x => x % 2 == 0,
                x => x % 3 == 0
            });
            var list = generator.Generate(15, 0, 8);
            Console.WriteLine("Generated list: ");
            foreach (var item in list)
            {
                Console.Write($"{item} ");
            }
            Console.ReadKey();
        }
    }
}
