using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StockExchangeSimulator.Data.Models
{
    /// <summary>
    /// Client POCO
    /// </summary>
    public class Client : IAggregateRoot
    {
        //[Key, Required]
        public Int32 Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string TelephonNumber { get; set; }
        public Int32 Balance { get; set; }
        //public ZoneType Zone { get; set; }
        //[Required]
        public Stock Stock { get; set; }
        public Int32 ClientStocksQuantity { get; set; }
    }
}
