using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions_and_Logging.Implementation
{
    class ArgumentNotEvenException : Exception
    {
        public ArgumentNotEvenException(string msg) : base(msg) { }
        public ArgumentNotEvenException() { }
    }
}
