using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Core.Abstractions;

namespace Events.Implementations
{
    public class Point : IGeometryObject
    {
        public Point(Int32 x, Int32 y) { this.X = x; this.Y = y; }
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        public char Symbol { get; set; }
    }
}
