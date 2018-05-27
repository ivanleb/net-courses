using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMCore.Model
{
    public class Deal
    {
        public int Id { get; set; }

        public Client Buyer { get; set; }

        public Client Seller { get; set; }

        public decimal Sum { get; set; }

        public Stock Stock { get; set; }
    }
}
