using System;
using Delegates.Core.Abstractions;

namespace Delegates.ConsoleApp.Implementations
{
    public class Registry : IRegistry
    {
        public IBoard Board { get; set; }
        public IProcessUserInput ProcessUserInput { get; set; }
        public IShowMessageToUser ShowMessageToUser { get; set; }
        public IDrawing Drawing { get; set; }

        public Registry()
        {
            this.Board = new Board();
            this.ProcessUserInput = new ProcessUserInput();
            this.ShowMessageToUser = new ShowMessageToUser();
            this.Drawing = new Drawing();
        }
    }
}
