using System;

namespace Events.Console.Implementations
{
    internal class CommandEventArgs : EventArgs
    {
        public ConsoleKey ReceivedCommand { get; set; }
    }

    internal class HeroMovedEventArgs : EventArgs
    {
        public int NewX { get; set; }
        public int NewY { get; set; }
        
        
    }        
}