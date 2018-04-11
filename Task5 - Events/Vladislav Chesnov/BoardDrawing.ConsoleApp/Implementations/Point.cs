using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDrawing.ConsoleApp.Implementations
{
    public class Point
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public char Sym { get; set; }
        public Point()
        {

        }
        public Point(int xCoord, int yCoord, char pointSymbol)
        {
            PosX = xCoord;
            PosY = yCoord;
            Sym = pointSymbol;
        }

        public void DrawPoint()
        {
            Console.SetCursorPosition(PosX, PosY);
            Console.Write(Sym);
        }

        public bool IsHit(Point p)
        {
            return p.PosX == PosX && p.PosY == PosY;
        }
    }
}
