using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions_and_Logging.Core
{
    public interface IPoint
    {
        int X { get; set; }
        int Y { get; set; }
    }
}
