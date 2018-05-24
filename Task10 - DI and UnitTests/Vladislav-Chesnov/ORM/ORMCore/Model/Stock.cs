using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMCore.Model
{
    public class Stock
    {
        public int Id { get; set; }

        public StockType Type { get; set; }
    }
}
