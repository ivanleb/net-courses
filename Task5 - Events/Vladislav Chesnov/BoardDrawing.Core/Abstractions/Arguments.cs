using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDrawing.Core.Abstractions
{
    public class GameEventArgs
    {
    }

    public class CommandEventArgs : GameEventArgs
    {
        public ConsoleKeyInfo PressedKey { get; set; }
    }

    public class MineArgs : GameEventArgs
    {
        public int WhereToWrite { get; set; }
    }
}
