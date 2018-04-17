using System.Linq;

namespace LinqCore
{
    public class Shop
        {
            public string Name { get; set; }
            public string Street { get; set; }
            public string Brand { get; set; }
            public int SquareInMetrs { get; set; }
            public double DailyProceeds { get; set; }
            public int ID { get; set; }
        }

        public class Restaurant
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Kitchen { get; set; }
            public int CapacityVisitors { get; set; }
            public double AverageBill { get; set; }

        }

        public class Hotel
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int YearBuilt { get; set; }
            public int NumberRooms { get; set; }
            public bool IsAdultOnle { get; set; }
        }

    public interface IDataModel
    {
        IQueryable<Shop> Shops { get; }
        IQueryable<Restaurant> Restraurants { get; }
        IQueryable<Hotel> Hotels { get; }
    }
    
    public static class DataRetriviever
    {
        public static IQueryable<Hotel> GetHotelsForAdult(this IQueryable<Hotel> hotels)
        {
            return hotels.Where(h => h.IsAdultOnle);
        }

        public static IQueryable<string> GetAllBrands(this IQueryable<Shop> shops)
        {
            return shops.Select(s => s.Brand);
        }

        public static IQueryable<char> JoinNames(this IQueryable<Restaurant> restaurants)
        {
            return restaurants.SelectMany(r => r.Name);
        }

        public static IQueryable<Shop> GetManyShops(this IQueryable<Shop> shops, int count)
        {
            return shops.Take(count);
        }

        public static IQueryable<Shop> GetRestShops(this IQueryable<Shop> shops, int count)
        {
            return shops.Skip(count);
        }

        public static IQueryable<Restaurant> GetrRestaurantsWhileBiggerBill(this IQueryable<Restaurant> restaurants, double bill)
        {
            return restaurants.TakeWhile(b => b.AverageBill > bill);
        }

        public static IQueryable<Hotel> GetNewHotels(this IQueryable<Hotel> hotels, int year)
        {
            return hotels.SkipWhile(y => y.YearBuilt < 2000);
        }

        public static IQueryable<Restaurant> GegConcatRestaurants(this IQueryable<Restaurant> restaurants, int numberConcat)
        {
            return restaurants.Concat(restaurants.Skip(numberConcat));
        }

        public static IQueryable<Shop> OrderShopsBySquare(this IQueryable<Shop> shops)
        {
            return shops.OrderBy(s => s.SquareInMetrs);
        }

        public static IQueryable<IGrouping<string, int>> GetRestrauntIdsByKitchen(this IQueryable<Restaurant> restaurants)
        {
            return restaurants.GroupBy(o => o.Kitchen, o => o.ID);
        }


}
}
