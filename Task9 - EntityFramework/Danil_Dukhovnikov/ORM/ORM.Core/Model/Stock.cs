namespace ORM.Core.Model
{
    public sealed class Stock
    {
        public int Id { get; set; }

        public int TypeId { get; set; }
        public StockType Type { get; set; }

        public int ClientId { get; set; }
        public Client Owner { get; set; }

        public bool IsForSale { get; set; }
    }
}
