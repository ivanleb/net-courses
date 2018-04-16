using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    interface IUserInput
    {
        void StartListen();
        event EventHandler<EventArgs> InputReceived;
    }
}
