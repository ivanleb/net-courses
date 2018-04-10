using System;
using System.Collections.Generic;
using RuslanTask5.Abstractions;

namespace RuslanTask5.Implementations
{
    class Board : IBoard
    {
        public int StartBoardPositionX { get { return 0; } }
        public int StartBoardPositionY { get { return 0; } }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        
    }
}
