using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class MoverTurnEventArgs : EventArgs
    {
        public int NewX { get; set; }
        public int NewY { get; set; }
    }
}
