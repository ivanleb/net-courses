using Drawing.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drawing.ConsoleApp.Implementations
{
    public class Board : IBoard
    {
        public int BoardSizeX { get; set; }
        public int BoardSizeY { get; set; }
    }
}
