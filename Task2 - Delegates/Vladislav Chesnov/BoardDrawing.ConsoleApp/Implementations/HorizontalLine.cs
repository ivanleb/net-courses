using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDrawing.ConsoleApp.Implementations
{
    public class HorizontalLine
    {
        public List<Point> pList;

        public HorizontalLine(int startX, int endX, int y, char symbol)
        {
            pList = new List<Point>();
            for(int x = startX; x <= endX; x++)
            {
                Point p = new Point(x, y, symbol);
                pList.Add(p);
            }
        }

        public void Draw()
        {
            foreach(Point p in pList)
            {
                p.Draw();
            }
        }
    }
}
