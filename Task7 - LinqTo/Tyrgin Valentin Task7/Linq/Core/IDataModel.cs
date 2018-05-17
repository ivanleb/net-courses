using System.Linq;

namespace Core
{
    public interface IDataModel
    {
        IQueryable<Car> Cars { get; }
        IQueryable<Dealer> Dealers { get; }
        IQueryable<Order> Orders { get; }
    }
}
