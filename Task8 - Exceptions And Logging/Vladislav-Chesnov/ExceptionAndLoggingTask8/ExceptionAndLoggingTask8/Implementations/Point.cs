using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionAndLoggingTask8.Abstractions;

namespace ExceptionAndLoggingTask8
{
    class Point : IPoint
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }
    }
}
