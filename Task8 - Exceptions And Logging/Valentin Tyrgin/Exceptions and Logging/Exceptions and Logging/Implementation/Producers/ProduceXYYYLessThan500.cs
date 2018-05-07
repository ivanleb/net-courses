using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions_and_Logging.Core;

namespace Exceptions_and_Logging.Implementation
{
    internal class ProduceXYYYLessThan500 : PointProducer
    {
        public override IPoint CreatePoint()
        {
            var multiplier =(double)random.Next(1, 11) / 10;
            var x = random.Next(0, 50) * multiplier;
            var y = Math.Pow(x, 3);
            if(x<500&&y<500) return new Point((int)x, (int)y);
            throw new ArgumentOutOfRangeException($"({x},{y})");
        }
    }
}
