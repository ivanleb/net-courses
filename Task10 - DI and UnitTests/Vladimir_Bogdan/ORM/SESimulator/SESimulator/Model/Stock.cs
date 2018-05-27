namespace SESimulator.Model
{
    public partial class Stock
    {
        public int Id { get; set; }

        public int TypeId { get; set; }
        public virtual StockType Type { get; set; }

        public int ClientId { get; set; }
        public virtual Client Owner { get; set; }

        public bool IsForSale { get; set; }
    }
}
