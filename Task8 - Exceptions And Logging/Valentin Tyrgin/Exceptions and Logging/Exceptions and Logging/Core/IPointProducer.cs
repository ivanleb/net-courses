using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions_and_Logging.Core
{
    interface IPointProducer
    {
        bool IsContinue { get; set; }
        event EventHandler<IPoint> OnPointProduced;
        void Run(Action<IPoint> onPointAction);
    }
}
