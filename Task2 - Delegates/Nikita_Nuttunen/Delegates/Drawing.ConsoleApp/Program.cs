using Drawing.ConsoleApp.Implementations;
using Drawing.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drawing.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(81, 40);
            new GameLogic().Run(new ConsoleAppRegistry());
        }
    }
}
