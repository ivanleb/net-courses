using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    interface IMover
    {
        int Xpos { get; set; }
        int Ypos { get; set; }

        void startListenInput(IUserInput input);
        event EventHandler<EventArgs> Move;
    }
}
