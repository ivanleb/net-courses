using System.Collections.Generic;

namespace ORMCore.Model
{
    public interface IClient
    {
        decimal Balance { get; set; }
        ICollection<Stock> ClientStocks { get; set; }
        ICollection<Stock> ClientStocksForSale { get; }
        Zone ClientZone { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        string PhoneNumber { get; set; }
        string Surname { get; set; }

        string ToString();
    }
}