using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda
{
    class Program
    {
        class NumberGenerator
        {
           public void Subscribe (Action<int> onNumberReceived, Func<int, bool> useFilter)
            {
                for ( int i = 0; i < 150;i++)
                {
                    if (useFilter(i)) onNumberReceived(i);
                }
            }
            public void GetNumber(int i)
            {
                Console.WriteLine("Good number: " + i);
            }
        }

        static void Main(string[] args)
        {
            NumberGenerator generator = new NumberGenerator();
            Console.WriteLine("Every 10th number:");
            generator.Subscribe((x) =>
            {
                // should be even only
                generator.GetNumber(x);

            }, (x) =>
            {
                return x % 10 == 0;// declare rule for even.
            });
            Console.WriteLine("Every > 99 and %5 number:");
            generator.Subscribe((x) =>
            {
                // should be even only
                generator.GetNumber(x);

            }, (x) =>
            {
                return ((x % 5 == 0)&(x>99));// declare rule for even.
            });
            Console.ReadKey();
        }
    }
}
