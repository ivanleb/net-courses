using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions_and_Logging.Implementation
{
    public class ArgumentSimpleNumberException:Exception
    {
        public ArgumentSimpleNumberException(string msg) : base(msg){}
        public ArgumentSimpleNumberException() { }
    }
}
