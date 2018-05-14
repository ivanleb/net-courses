using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions_and_Logging.Core;

namespace Exceptions_and_Logging.Implementation
{
    class ProduceNotSimpleCoords:PointProducer
    {
        public override IPoint CreatePoint()
        {
            var x = random.Next(0, 50);
            var y = random.Next(0, 50);
            if(IsSimple(x)||IsSimple(y)) throw new ArgumentSimpleNumberException($"Координаты не должны быть простыми числами: ({x},{y})");
            return new Point(x, y);
        }

        private static bool IsSimple(int x)
        {
            for (int i = 2; i < x / 2; i++)
            {
                if (x % i == 0) return false;
            }
            return true;
        }
    }
}
