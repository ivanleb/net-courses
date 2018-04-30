using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESimulator.Abstractions
{
    public interface IUserInput
    {
        void ListenToUser();
        event EventHandler<ConsoleKey> OnUserInputRecieved;
    }
}
