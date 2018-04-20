using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointsGenerator.Core.Abstractions
{
    public interface IExceptionIndicator
    {       
        List<IPointProducer> Producers { get; set; }

        void WriteException(object sender, IPoint e);
    }
}
