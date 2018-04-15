using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Core.Abstractions
{
    public interface IProcessUserInput
    {
        void StartListening();
        event EventHandler<UserChoise> UserInput;
    }

    public enum UserChoise
    {
        Up,
        Down,
        Left,
        Right,
        Restart
    }
}
