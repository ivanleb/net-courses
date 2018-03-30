using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core = Delegates.Core;
using Delegates.App.Implementations;

namespace Delegates.App
{
    class Program
    {
        static void Main(string[] args)
        {
            core.AppLogic.Run(new CustomRegistry());
        }
    }
}
