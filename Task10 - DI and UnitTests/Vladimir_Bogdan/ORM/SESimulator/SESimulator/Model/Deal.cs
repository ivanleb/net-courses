namespace SESimulator.Model
{
    public class Deal
    {
        public int Id { get; set; }

        public decimal Cost { get; set; }

        public int IdBuyer { get; set; }
        public virtual Client Buyer { get; set; }

        public int IdSeller { get; set; }
        public virtual Client Seller { get; set; }

        public int StockId { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
