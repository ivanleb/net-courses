using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tatiana_klimenko_task6
{
    class Program
    {
        static void Main(string[] args)
        {
            NumberGenerator numGen = new NumberGenerator(100);

            Client client1 = new Client("client 1");
            Client client2 = new Client("client 2");

            //subscribe with one filter
            numGen.Subscribe((x) =>
            {
                client1.HandleOutput(x);
            }, 
            (x) => 
            {
                return x % 8 == 0;
            });

            //subscribe with several filters
            numGen.Subscribe((x) =>
            {
                client2.HandleOutput(x);
            },
            new List<Func<int,bool>>
            {
                (x) => { return x * x < 1000; },
                (x) => { return x % 10 == 0; }
            });


        }
    }
}
