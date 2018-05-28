using System;

namespace EF.Core.Implementations
{
    public class KeyEventArgs : EventArgs
    {
        public ConsoleKey KeyPressed { get; set; }
    }
}