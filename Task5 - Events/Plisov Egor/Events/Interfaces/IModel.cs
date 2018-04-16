using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    interface IModel
    {
        IMover Mover { get; set; }
        IList<IProblem> problem { get; set; }
    }
}
