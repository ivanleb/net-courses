using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegates.Core.Abstractions;

namespace Delegates.Core
{
    public class DrawLogic
    {
        public void Run(IRegistry registry)
        {
            var showMessageToUser = registry.ShowDrawingToUser;
            var processUserInput = registry.ProcessUserInput;

            while (true)
            {
                var currentObject = processUserInput.InputObject();

            }
        }

    }
}
