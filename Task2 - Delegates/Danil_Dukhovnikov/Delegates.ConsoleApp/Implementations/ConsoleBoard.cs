using System;
using System.Collections.Generic;
using Delegates.Core.Abstractions;

namespace Delegates.ConsoleApp.Implementations
{
    public class ConsoleBoard : IBoard
    {
        public ConsoleBoard(int boardSizeX = 40, int boardSizeY = 15)
        {
            BoardSizeX = boardSizeX;
            BoardSizeY = boardSizeY;
        }

        public int BoardSizeX { get; }
        public int BoardSizeY { get; }
    }
}