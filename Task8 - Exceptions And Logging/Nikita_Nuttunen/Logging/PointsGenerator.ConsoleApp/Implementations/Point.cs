using PointsGenerator.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointsGenerator.ConsoleApp.Implementations
{
    public struct Point : IPoint
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }

        public override string ToString()
        {
            return $"({X}, {Math.Round(Y, 2)})";
        }
    }
}
