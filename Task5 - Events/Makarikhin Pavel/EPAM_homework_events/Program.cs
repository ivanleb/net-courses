using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_events
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Registry registry = new Registry();
            new ProgramLogic().Run(registry);

        }
    }
}
