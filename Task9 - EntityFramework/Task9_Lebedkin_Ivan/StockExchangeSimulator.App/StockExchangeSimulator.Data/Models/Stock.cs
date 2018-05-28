using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StockExchangeSimulator.Data.Models
{
    public enum StockTypeEnum : byte { LowPrice = 0, MiddlePrice = 1, HighPrice = 2 }

    public class Stock : IAggregateRoot
    {
        //[Key, Required]
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public Int32 Price { get; set; }
        public StockTypeEnum Type { get; set; }
    }
}
