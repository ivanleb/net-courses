using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Core.Abstractions;

namespace Events.Implementations
{

        public class ConsoleAppRegistry : IRegistry
        {
            public IShowDrawingToUser ShowDrawingToUser { get; set; }
            public IProcessUserInput ProcessUserInput { get; set; }
            public IBoard GetEmptyBoard { get; }

            public ConsoleAppRegistry()
            {
                ShowDrawingToUser = new Drawing();
                ProcessUserInput = new ProcessUserInput();
                GetEmptyBoard = new Board();
            }
        }
    
}
