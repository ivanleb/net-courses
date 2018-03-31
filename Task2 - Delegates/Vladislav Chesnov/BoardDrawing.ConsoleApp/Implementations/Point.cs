using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDrawing.ConsoleApp.Implementations
{
    public class Point
    {
        int x;
        int y;
        char symbol;
        public Point(int xCoord, int yCoord, char pointSymbol)
        {
            x = xCoord;
            y = yCoord;
            symbol = pointSymbol;
        }

        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }
    }
}
