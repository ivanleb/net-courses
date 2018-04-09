using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdasAnonymousMethodsConsoleApp
{
    struct ExampleStruct
    {
        public int X;
        public int Y;
    }

    class ExampleClass
    {
        public int X;
        public int Y;
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<ExampleStruct> testList = new List<ExampleStruct>();
            
            testList.Add(new ExampleStruct() { X = 5, Y = 10 });
            testList.Add(new ExampleStruct() { X = 2, Y = 11 });
            testList.Add(new ExampleStruct() { X = 13, Y = 14 });

            testList[0] = new ExampleStruct() { };
            var test1 = testList.First();

            //var testEntity = testList.First();
            //testEntity.X = 0;
            //testEntity.Y = 1;
             
            ExampleStruct[] testList2 = new ExampleStruct[]
            {
                new ExampleStruct() { X = 5, Y = 10 },
                new ExampleStruct() { X = 5, Y = 10 },
                new ExampleStruct() { X = 5, Y = 10 }
            };

            //testList[0] = new ExampleStruct() { };

            testList2[0].X = 123;

            Console.WriteLine(testList2[0]);

            //var testEntity = testList.First();
            //testEntity.X = 0;
            //testEntity.Y = 1;

            //var generator = new NumberGeneratorWithOneAction();

            //var client1 = new Client("1234");                

            //generator.Subscribe((x) =>
            //{
            //    Console.WriteLine(x);
            //    client1.HandleNumber(x);
            //});

            //var client1 = new Client("Client1");

            //generator.Subscribe(client1.HandleNumber);

            //Console.ReadLine();

            //var generator2 = new NumberGeneratorWithOneActionAndFunc();
            //var client2 = new Client("Client2");

            //generator2.Subscribe((x) =>
            //{
            //    client2.HandleNumber(x);
            //}, (x) => { return x % 2 == 0; });

            //Console.ReadLine();

            //var client3 = new Client("Client3");

            //generator2.Subscribe((x) => client3.HandleNumber(x), (x) => { return x % 3 == 0; });

            //Console.ReadLine();
        }
    }

    class NumberGeneratorWithOneAction
    {
        public void Subscribe(Action<int> onNumberReceived)
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 1500; i++)
                {
                    onNumberReceived(i);
                }
            });
        }
    }

    class NumberGeneratorWithOneActionAndFunc
    {
        public void Subscribe(Action<int> onNumberReceived, Func<int, bool> filterRule)
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 1500; i++)
                {
                    if (filterRule(i))
                    {
                        onNumberReceived(i);
                    }
                }
            });
        }
    }

    class Client
    {
        private string ClientName { get; set; }

        public Client(string clientName)
        {
            this.ClientName = clientName;
        }

        public void HandleNumber(int x)
        {
            Console.WriteLine($"{ClientName} - {x}");
        }
    }
}
