using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardDrawing.ConsoleApp.Implementations;
using BoardDrawing.Core;

namespace BoardDrawing.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new ProgramLogic().Run(new Registry());
        }
    }
}
