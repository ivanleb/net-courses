using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegates.Core.Abstractions;

namespace Delegates.Implementations
{
    class Registry : IRegistry
    {
        public IShowDrawingToUser ShowDrawingToUser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IProcessUserInput ProcessUserInput { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
