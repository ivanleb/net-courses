using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMCore.Abstractions
{
    public interface IStock
    {
        int Id { get; set; }

        string Type { get; set; }

        decimal Cost { get; }

        decimal GetStockCost(string type);
    }
}
