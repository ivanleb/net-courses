using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDrawing.ConsoleApp.Implementations
{
    public class Shape
    {
        public List<Point> pList;

        public void DrawShape()
        {
            foreach(Point p in pList)
            {
                p.DrawPoint();
            }
        }

        public void DrawShapeRed()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            DrawShape();
            Console.ResetColor();
        }

        public bool IsHit(Point point)
        {
            foreach(var p in pList)
            {
                if (p.IsHit(point))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
