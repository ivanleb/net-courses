using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Drawer
{
    interface IBoard
    {
        void DrawBoarder();
        void DrawPoint();
        void DrawHorizontalLine();
        void DrawVerticalLine();

    }
}
