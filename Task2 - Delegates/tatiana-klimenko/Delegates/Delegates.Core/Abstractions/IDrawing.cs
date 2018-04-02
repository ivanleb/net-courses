using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Core.Abstractions
{
    public interface IDrawing
    {
        void DrawBoard(IBoard board);
        void DrawDot(IBoard board);
        void DrawHorizontalLine(IBoard board);
        void DrawVerticalLine(IBoard board);       
    }
}
