using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RuslanTask5.Implementations;

namespace RuslanTask5.Abstractions
{
    interface IInputProcess
    {
        event EventHandler InputReceived;
        void Start(IHero hero,IBoard board);
    }
}
