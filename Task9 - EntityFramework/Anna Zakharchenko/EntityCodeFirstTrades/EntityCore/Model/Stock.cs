using System.Collections.Generic;

namespace EntityCore.Model
{
    public class Stock
    {
        public int Id { get; set; }
        public string NameTypeOfStock { get; set; }
        public decimal Cost { get; set; }
        public KeyValuePair<string,decimal> TypeOfStock { get; set; }
    }
}