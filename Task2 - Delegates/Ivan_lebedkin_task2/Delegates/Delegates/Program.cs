using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegates.Core;
using Delegates.Core.Abstractions;
using Delegates.Implementations;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            new DrawLogic().Run(new Registry());
        }
    }
}
