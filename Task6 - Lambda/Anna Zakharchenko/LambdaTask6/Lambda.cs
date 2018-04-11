using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaTask6
{
    class Lambda
    {
        static void Main(string[] args)
        {
            NumberGenerator numberGenerator = new NumberGenerator();
            Client client1 = new Client("Client Fresh");
            Client client2 = new Client("Client Disappointed");
            Client client3 = new Client("Client Spitz");

            List<Func<int,bool>> func1 = new List<Func<int, bool>>() { (x) => { return x % 5 == 0; },
                                                                        (x) => { return x != 20; } };
            numberGenerator.Subscribe((x) => { client1.HandleOutput(x); }, func1);

            List<Func<int, bool>> func2 = new List<Func<int, bool>>() { (x) => { return x % 10 == 1; },
                                                                        (x) => { return x < 30; } };
            numberGenerator.Subscribe((x) => { client2.HandleOutput(x); }, func2);

            List<Func<int, bool>> func3 = new List<Func<int, bool>>() { (x) => { return x != 20; },
                                                                        (x) => { return x < 33; },
                                                                        (x) => { return x % 2 == 0; } };
            numberGenerator.Subscribe((x) => { client3.HandleOutput(x); }, func3);

            Console.ReadKey();
        }
    }

    class NumberGenerator
    {
        public void Subscribe(Action<int> onNumberReceived, IEnumerable<Func<int,bool>> useFilter)
        {            
            for(int i = 5; i<36; i += 3)
            {
                bool allTrue = true;
                foreach (var func in useFilter)
                {
                    if (!func(i))
                    {
                        allTrue = false;
                        break;
                    }
                }
                if (allTrue)
                {
                    onNumberReceived(i);
                }
            }
        }
    }

    class Client
    {
        private string Name;
        public Client(string name)
        {
            Name = name;
        }

        public void HandleOutput(int date)
        {
            Console.WriteLine(Name + "\t-\t" + date);
        }
    }
}
