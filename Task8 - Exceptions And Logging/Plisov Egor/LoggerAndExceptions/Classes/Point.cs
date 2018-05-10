using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerAndExceptions
{
    class Point : IPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"x = {X}, y = {Y}";
        }
    }
}
