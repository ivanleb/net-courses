using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Core.Abstractions;

namespace Events.Implementations
{
    public class Mines : IGeometryObject
    {
        public Mines(Int32 x, Int32 y) { this.X = x; this.Y = y; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
