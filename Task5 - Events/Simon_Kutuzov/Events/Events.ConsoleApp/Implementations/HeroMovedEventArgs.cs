using System;

namespace Events.ConsoleApp.Implementations
{
    class HeroMovedEventArgs : EventArgs
    {
        public int NewX { get; set; }
        public int NewY { get; set; }
    }
}
