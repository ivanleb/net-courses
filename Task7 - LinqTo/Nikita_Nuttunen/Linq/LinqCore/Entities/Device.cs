using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqCore.Entities
{
    public class Device
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public bool IsAvailable { get; set; }

        public override string ToString()
        {
            string available = IsAvailable ? "Available" : "Not available";
            
            return $"{Id, 2} | {Name, 25} | {Price, 5} | {Category, 11} | {available}";
        }
    }
}
