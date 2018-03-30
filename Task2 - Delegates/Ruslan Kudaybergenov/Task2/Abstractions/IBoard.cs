using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Abstractions
{
    public interface IBoard
    {
        int BoardSizeX { get; set; }
        int BoardSizeY { get; set; }
        int BoardPositionX { get; set; }
        int BoardPositionY { get; set; }
        string BoardLineMarkerX { get; set; }
        string BoardLineMarkerY { get; set; }
    }
}
