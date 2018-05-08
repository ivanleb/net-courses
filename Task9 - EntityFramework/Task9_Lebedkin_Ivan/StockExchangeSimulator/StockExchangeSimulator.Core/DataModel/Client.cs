using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StockExchangeSimulator.Core.DataModel
{
    public enum ZoneType : byte
    {
        Grey = 0,
        Orange = 1,
        Black = 2
    }

    public class Client
    {
        [Key, Required]
        public Int32 Id { get; set; }               
        public string FirstName { get; set; }               
        public string SurName { get; set; }               
        public string TelephonNumber { get; set; }               
        public Int32 Balance { get; set; }               
        //public ZoneType Zone { get; set; }
        [Required]
        public Stock Stock { get; set; }               
        public Int32 ClientStocksQuantity { get; set; }
    }
}
