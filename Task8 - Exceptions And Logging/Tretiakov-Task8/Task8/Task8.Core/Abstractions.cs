using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task8.Core
{
    public interface IPoint
    {
        decimal X { get; set; }
        decimal Y { get; set; }
    }

    public class Point : IPoint
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }
    }

    public interface IPointProducer
    {
        bool IsContinue { get; set; }
        void Run(Func<IPoint, bool> isPointValid);
    }

}
