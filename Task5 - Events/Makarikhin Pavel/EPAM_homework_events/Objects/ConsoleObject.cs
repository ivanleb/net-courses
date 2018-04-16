using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_events.Objects
{
    interface IConsoleObject : IObject
    {
        ConsoleColor Color { get; set; }

        char Model { get; set; }
    }
}
