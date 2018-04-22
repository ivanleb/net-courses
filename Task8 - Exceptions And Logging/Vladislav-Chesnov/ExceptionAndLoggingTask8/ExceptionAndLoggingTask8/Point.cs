using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionAndLoggingTask8
{
    public interface IPoint
    {
        decimal X { get; set; }
        decimal Y { get; set; }
    }

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
