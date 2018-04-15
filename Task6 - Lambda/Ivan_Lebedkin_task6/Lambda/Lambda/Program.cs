using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda
{
    class NumberGenerator
    {
        public NumberGenerator()
        {
            rnd = new Random();
            lst = new List<int>();
            GenerateNumbers(1000);
        }

        public Random rnd;
        public List<Int32> lst;

        public void GenerateNumbers(Int32 amount)
        {
            for (int i = 0; i < amount; i++)
            {
                lst.Add(rnd.Next());
            }
        }

        public void Subscribe(Action<Int32> onNumberReceived, Func<int, bool> useFilter)
        {
            foreach (var number in lst)
            {
                if (useFilter(number))
                {
                    onNumberReceived(number);
                }
            }
        }

        public void Subscribe(Action<Int32> onNumberReceived, IEnumerable<Func<int, bool>> useFilter)
        {
            foreach (var number in lst)
            {
                foreach (var filter in useFilter)
                {
                    if (filter(number))
                    {
                        onNumberReceived(number);
                    }
                }

            }
        }
    }

    class Client
    {
        public Client()
        {
            lst = new List<int>();
        }
        public List<Int32> lst;
        public void HandleOutput(Int32 x)
        {
            lst.Add(x);
        }
        public bool Filter(Int32 x)
        {
            return x % 2 == 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client();
            var numberGenerator = new NumberGenerator();

            numberGenerator.Subscribe((x) =>
            {
                client.HandleOutput(x);
            }, (x) =>
            {
                return x % 2 == 0; 
            });
            //client.lst.ForEach((x) => Console.WriteLine(x));

            Console.WriteLine(client.lst.Count);
            List<Func<Int32, bool>> myFilters = new List<Func<Int32, bool>>();
            myFilters.Add(x => x % 2 == 0);
            myFilters.Add(x => x % 3 == 0);
            myFilters.Add(x => x % 255 == 0);
            var client2 = new Client();
            numberGenerator.Subscribe((x) =>
            {
                client2.HandleOutput(x);
            }, 
            myFilters);
            Console.WriteLine(client2.lst.Count);
        }
    }
}
