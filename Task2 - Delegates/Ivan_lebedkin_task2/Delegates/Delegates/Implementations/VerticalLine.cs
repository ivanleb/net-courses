using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegates.Core.Abstractions;

namespace Delegates.Implementations
{
    class VerticalLine : IDrawingObject
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Length { get; set; }
    }
}
