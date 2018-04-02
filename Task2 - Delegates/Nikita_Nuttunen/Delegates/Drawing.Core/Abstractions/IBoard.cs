using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drawing.Core.Abstractions
{
    delegate void Draw(IBoard board);
    public interface IBoard
    {
        int BoardSizeX { get; set; }
        int BoardSizeY { get; set; } 
    }
}
