namespace ORM.Core.Model
{
    public sealed class Deal
    {
        public int Id { get; set; }

        public decimal Cost { get; set; }

        public int BuyerIdd { get; set; }
        public Client Buyer { get; set; }

        public int Selleri { get; set; }
        public Client Seller { get; set; }

        public int StockId { get; set; }
        public Stock Stock { get; set; }
    }
}
