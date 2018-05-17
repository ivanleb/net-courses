using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Core.Abstractions
{
    public interface IGeometryObject
    {
        Int32 X { get; set; }
        Int32 Y { get; set; }
    }
}
