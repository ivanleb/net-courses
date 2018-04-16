using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Implementations
{
    public class ConsoleKeyEventsArgs : EventArgs
    {
        public ConsoleKeyEventsArgs(ConsoleKey currentKey)
        {
            CurrentConsoleKey = currentKey;
        }
        public ConsoleKey CurrentConsoleKey { get; set; }
    }
}
