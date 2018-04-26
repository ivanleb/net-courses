using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMCore.Abstractions
{
    public interface IDeal
    {
        int Id { get; set; }

        IClient Seller { get; set; }

        IClient Purchaser { get; set; }

        IStock SelledStock { get; set; }

        decimal Cost { get; }
    }
}
