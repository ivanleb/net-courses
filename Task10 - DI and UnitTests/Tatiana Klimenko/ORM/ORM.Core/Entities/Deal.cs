namespace ORM.Core.Entities
{
    public class Deal
    {
        public int Id { get; set; }

        public Client Seller { get; set; }

        public Client Purchaser { get; set; }

        public Stock SelledStock { get; set; }

        public decimal Cost { get; set; }
    }
}
