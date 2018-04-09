using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardDrawing.Core.Abstractions;

namespace BoardDrawing.ConsoleApp.Implementations
{
    class ConsoleAppShowMessageToUser : IShowMessageToUser
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void Pause()
        {
            Console.ReadLine();
        }
    }
}
