using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsCore.Abstractions
{
    public class InputEventArgs : GameEventArgs
    {
        public ConsoleKeyInfo input;
    }
    public delegate void InputEventHandler(IUserInput sender, InputEventArgs args);
    public interface IUserInput
    {
        void ListenToUser();
        event InputEventHandler OnInput;
    }
}
