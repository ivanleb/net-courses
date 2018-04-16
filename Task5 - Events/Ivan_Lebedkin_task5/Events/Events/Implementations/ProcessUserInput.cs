using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Core.Abstractions;
using System.Windows.Forms;

namespace Events.Implementations
{
    public class ProcessUserInput : IProcessUserInput
    {
        public ProcessUserInput()
        {
            Console.CursorVisible = false;
        }

        public event Action<object, EventArgs> Shift
        {
            add { shift += value; }
            remove { shift -= value; }
        }
        public Action<object, EventArgs> shift;

        public void Input(IBoard board)
        {
            ConsoleKeyEventsArgs e = new ConsoleKeyEventsArgs(Console.ReadKey().Key);
            shift(board, e);   
        }
    }
}
