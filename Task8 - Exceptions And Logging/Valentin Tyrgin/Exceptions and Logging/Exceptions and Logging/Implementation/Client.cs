using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions_and_Logging.Core;

namespace Exceptions_and_Logging.Implementation
{
    class Client:IClient
    {
        public string Name { get; set; }
        public void OnPointReceived(object sender, IPoint point)
        {
            Logger.Log.Warn($"Client {Name} receved point {point}");
        }
    }
}
