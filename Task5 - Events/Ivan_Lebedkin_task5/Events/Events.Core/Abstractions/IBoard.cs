using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Core.Abstractions
{
    public interface IBoard : IHaveMines
    {
        Int32 boardSizeX { get; set; }
        Int32 boardSizeY { get; set; } 
        IGeometryObject Point { get; set; }
        event Action Hit;
    }
}
