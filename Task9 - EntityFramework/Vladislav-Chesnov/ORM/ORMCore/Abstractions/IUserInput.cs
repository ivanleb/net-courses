using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMCore.Abstractions
{
    public interface IUserInput
    {       
        event EventHandler<CommandEventArgs> OnKeyPressed;

        void ListenToUser();
    }

    public class CommandEventArgs
    {
        public ConsoleKeyInfo PressedKey { get; set; }
    }
}
