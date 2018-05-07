using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions_and_Logging.Core
{
    internal interface IClient
    {
        string Name { get; set; }
        void OnPointReceived(object sender, IPoint point);
    }
}
