using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingAndExceptions.Abstracrions
{
    public interface IClient
    {
        void GetGoodPoint(object sender, IPoint point);
    }
}
