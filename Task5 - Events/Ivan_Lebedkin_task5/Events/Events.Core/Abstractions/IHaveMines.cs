using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Core.Abstractions
{
    public interface IHaveMines 
    {
        IEnumerable<IGeometryObject> Mines { get; set; }
    }
}
