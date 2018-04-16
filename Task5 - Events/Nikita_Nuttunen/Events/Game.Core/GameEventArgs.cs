using Game.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Core
{
    public class GameEventArgs
    {

    }

    public class CommandEventArgs : GameEventArgs
    {
        public ConsoleKeyInfo ReceivedCommand { get; set; }
    }
}
