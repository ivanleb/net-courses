using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionsAndLogging.Core.Abstractions;

namespace ExceptionsAndLogging
{
    public class Client
    {
        public void onPointReceive(object o, IPoint point)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(point.ToString() + " by " + o.GetType());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
