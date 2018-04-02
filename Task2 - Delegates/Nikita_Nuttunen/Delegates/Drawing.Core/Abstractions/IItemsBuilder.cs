using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drawing.Core.Abstractions
{
    public interface IItemsBuilder
    {
        void DrawPoint(IBoard board);
        void DrawVerticalLine(IBoard board);
        void DrawHorizontalLine(IBoard board);        
    }
}
