
namespace EntityCore.Model
{
    public class Trade
    {
        public int Id { get; set; }
        public Client Seller { get; set; }
        public Client Buyer { get; set; }
        public Stock StockFromSeller { get; set; }

        public override string ToString()
        {
            return $"Seller is {Seller.FirstName} {Seller.LastName}," +
                $" Buyer is {Buyer.FirstName} {Buyer.LastName}, " +
                $"Sold stock - {StockFromSeller.NameTypeOfStock}";
        }
    }
}
