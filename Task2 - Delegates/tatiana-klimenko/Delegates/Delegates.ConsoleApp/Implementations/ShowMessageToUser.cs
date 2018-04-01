using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegates.Core.Abstractions;

namespace Delegates.ConsoleApp.Implementations
{
    public class ShowMessageToUser : IShowMessageToUser
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowMenu()
        {
            Console.WriteLine(@"
Menu:
0 - quit the game
1 - draw a dot
2 - draw a vertical line
3 - draw a horizantal line");
        }
    }
}
