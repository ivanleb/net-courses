using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions_and_Logging.Core;

namespace Exceptions_and_Logging.Implementation
{
    class Point:IPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public Point(){}

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
