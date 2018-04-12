using System;

namespace Events.ConsoleApp.Implementations
{
    public class CommandEventArgs : EventArgs
    {
        public ConsoleKey ReceivedCommand { get; set; }
    }
}
