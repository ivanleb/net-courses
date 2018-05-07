using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions_and_Logging.Core;

namespace Exceptions_and_Logging.Implementation
{
    internal class Produce2XY:PointProducer
    {
        public override IPoint CreatePoint()
        {
            int x = random.Next(0, 50);
            int y = x / 2;
            if (x % 2 == 0) return new Point(x, y);
            throw new ArgumentNotEvenException($"Координата X должна быть четной, текущее значение: {x}");
        }
    }
}
