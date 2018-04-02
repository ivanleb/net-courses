using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegates.Core.Abstractions;

namespace Delegates.Implementations
{
    public class ConsoleProcessUserInput : IProcessUserInput
    {
        public IDrawingObject InputObject()
        {
            var userChoice = Console.ReadLine();
            if (int.Parse(userChoice) == 0) return new Point();
            if (int.Parse(userChoice) == 1) return new HorizontalLine();
            if (int.Parse(userChoice) == 2) return new VerticalLine();
            return null;
        }
    }
}
