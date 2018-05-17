using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqCore.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }        

        public DateTime DateOfBirth { get; set; }

        public string Status { get; set; }

        public override string ToString()
        {
            return $"{Id, 2} | {Name, 25} | {PhoneNumber, 10} | " +
                $"{DateOfBirth.ToShortDateString(), 10} | Status: {Status}";
        }
    }
}
