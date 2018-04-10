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

            numberGenerator.Subscribe((x) => { client1.HandleOutput(x); }, (x) => { return x % 5 == 0; });
            numberGenerator.Subscribe((x) => { client2.HandleOutput(x); }, (x) => { return x % 10 == 1; });
            numberGenerator.Subscribe((x) => { client3.HandleOutput(x); }, (x) => { return x != 25; });

            Console.ReadKey();
        }
    }

    class NumberGenerator
    {
        public void Subscribe(Action<int> onNumberReceived, Func<int,bool> useFilter)
        {
            for(int i = 5; i<36; i += 3)
            {
                if (useFilter(i))
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
