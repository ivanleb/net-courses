using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Core.Abstractions
{
    public interface IDrawingObject
    {
        Int32 PositionX { get; set; }
        Int32 PositionY { get; set; }
        Int32 Length { get; set; }
    }
}
