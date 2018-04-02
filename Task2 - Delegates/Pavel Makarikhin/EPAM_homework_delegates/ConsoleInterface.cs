using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

using Board;

namespace EPAM_homework_delegates
{
    class ConsoleInterface : IUserInterface
    {
        public string ProcessUserInput()
        {
            return ReadLine();
        }

        public void ShowMessage(string message)
        {
            WriteLine(message);
        }


    }
}
