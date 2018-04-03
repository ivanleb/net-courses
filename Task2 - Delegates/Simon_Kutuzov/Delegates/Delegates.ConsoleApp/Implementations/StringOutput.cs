using System;
using Delegates.Core.Abstractions;

namespace Delegates.ConsoleApp.Implementations
{
    public class StringOutput : IStringOutput
    {
        public void ShowMessage(string msg) { Console.Write(msg); }
        public void ShowMenu()
        {
            Console.Write("1) Dot\n2) Vertical line\n3) Horizontal line\n4) Vertical and horizontal lines\n5) Reset\n0) Quit\n");
        }
    }
}
