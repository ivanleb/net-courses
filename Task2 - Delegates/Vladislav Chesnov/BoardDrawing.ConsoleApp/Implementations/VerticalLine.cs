using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDrawing.ConsoleApp.Implementations
{
    public class VerticalLine
    {
        public List<Point> pList;

        public VerticalLine(int startY, int endY, int x, char symbol)
        {
            pList = new List<Point>();
            for (int y = startY; y <= endY; y++)
            {
                Point p = new Point(x, y, symbol);
                pList.Add(p);
            }
        }

        public void Draw()
        {
            foreach (Point p in pList)
            {
                p.Draw();
            }
        }
    }
}
