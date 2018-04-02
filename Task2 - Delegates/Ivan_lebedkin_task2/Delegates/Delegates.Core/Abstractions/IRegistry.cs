using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Core.Abstractions
{
    public interface IRegistry
    {
        //IDoorsNumbersBuilder DoorsNumbersBuilder { get; set; }
        IShowDrawingToUser ShowDrawingToUser { get; set; }
        IProcessUserInput ProcessUserInput { get; set; }
        //ILevelWatcher LevelWatcher { get; set; }
    }
}
