namespace DIAndUnitTests.Core.Models
{
    public class Deal
    {
        public int Id { get; set; }
        public Trader Buyer { get; set; }
        public Trader Seller { get; set; }
        public Share Share { get; set; }
        public decimal Amount { get; set; }
    }
}