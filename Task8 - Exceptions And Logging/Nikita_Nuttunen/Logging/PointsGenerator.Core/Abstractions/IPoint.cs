using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointsGenerator.Core.Abstractions
{
    public interface IPoint
    {
        decimal X { get; set; }
        decimal Y { get; set; }
    }
}
