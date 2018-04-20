using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointsGenerator.Core.Abstractions
{
    public interface IPointProducer
    {
        bool IsContinue { get; set; }
        IPoint BuildPoint(decimal x);
        void Run(Action<IPoint> onPointReceiver);
    }
}
