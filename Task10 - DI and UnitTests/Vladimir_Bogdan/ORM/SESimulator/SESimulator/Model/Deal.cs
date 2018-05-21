namespace SESimulator.Model
{
    public class Deal
    {
        public int Id { get; set; }

        public Client Buyer { get; set; }

        public Client Seller { get; set; }

        public decimal Cost { get; set; }

        public Stock Stock { get; set; }
    }
}
