using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    interface IProblem
    {
        int Xpos { get; set; }
        int Ypos { get; set; }

        void WatchMoverTurn(IMover mover);
    }
}
