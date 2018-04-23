using System;
using System.Collections.Generic;
using Exceptions_Logging.Abstractions;

namespace Exceptions_Logging.Implementations
{
    public class Point : IPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }
}
