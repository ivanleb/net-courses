using EF.Core.Repositories;

namespace EF.Implementations
{
    public class StockType : IId
    {
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }
    }
}