using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Abstractions
{
    public interface IDrawing
    {
        void DrawAtBoard(IBoard board, string symbol, int positionX, int positionY);
        void DrawVerticalLine(IBoard board, int positionX);
        void DrawHorizontalLine(IBoard board, int positionY);
        void DrawBoard(IBoard board);
    }
}
