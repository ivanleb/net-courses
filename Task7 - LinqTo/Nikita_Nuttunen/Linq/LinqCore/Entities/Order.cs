using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqCore.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime Date { get; set; }

        public decimal Total { get; set; }

        public override string ToString()
        {
            return $"{Id, 2} | CustomerId: {CustomerId, 2} | Date: {Date.ToShortDateString(), 10} | {Total}";
        }
    }
}
