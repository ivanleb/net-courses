using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class StaticRegistry
    {
        public static IBoard board;
        public static IModel model;
        public static IUserInput input;
    }
}
