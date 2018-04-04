using Delegates.Core.Abstractions;

namespace Delegates.ConsoleApp.Implementations
{
    public class Registry : IRegistry
    {
        public IBoard Board { get; set; }
        public IDrawing Drawing { get; set; }
        public IStringOutput StringOutput { get; set; }
        public IUserActions UserActions { get; set; }

        public Registry()
        {
            this.Board = new Board();
            this.Drawing = new Drawing();
            this.StringOutput = new StringOutput();
            this.UserActions = new UserActions();
        }
    }
}
