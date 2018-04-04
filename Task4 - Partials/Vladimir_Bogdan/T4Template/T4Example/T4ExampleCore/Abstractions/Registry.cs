using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4ExampleCore.Abstractions
{
    public abstract class Registry
    {
        public ICatalogManager catalogManager { get; protected set; }
        public ICatalogPrinter catalogPrinter { get; protected set; }
    }
}
