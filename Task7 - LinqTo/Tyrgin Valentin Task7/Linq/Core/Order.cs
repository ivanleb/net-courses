using System;

namespace Core
{
    public class Order
    {
        public int Id { get; set; }
        public int DealerId { get; set; }
        public int CarId { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
    }
}
