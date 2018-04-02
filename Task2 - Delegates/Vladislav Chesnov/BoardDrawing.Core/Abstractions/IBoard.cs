using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDrawing.Core.Abstractions
{
    public interface IBoard
    {
        int BoardSizeX { get; set; }
        int BoardSizeY { get; set; }
        void DrawBoard();
        bool Draw(string userChoice, char[] menuItems);
        void BoardClear();        
    }
}
