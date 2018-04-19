using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            public string Style { get; set; }
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
        IQueryable<Restaurant> Restaurants { get; }
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
            return hotels.SkipWhile(y => y.YearBuilt < year);
        }

        public static IQueryable<Restaurant> GetConcatRestaurants(this IQueryable<Restaurant> restaurants, int numberConcat)
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

        public static IQueryable<string> GetStylesRestaurantsWithoutDuplicate(this IQueryable<Restaurant> restaurants)
        {
            return restaurants.Select(r => r.Style).Distinct();
        }

        public static IQueryable<string> GetAllNamesHotels(this IQueryable<Hotel> hotels, int number)
        {
            IQueryable<string> first = hotels.Select(h => h.Name).Take(number);
            IQueryable<string> second = hotels.Select(h => h.Name).Skip(number - 1);

            return first.Union(second);
        }

        public static IQueryable<string> GetIntersectOfShopsStreets(this IQueryable<Shop> shops, int number)
        {
            IQueryable<string> first = shops.Select(s => s.Street).Take(number);
            IQueryable<string> second = shops.Select(s => s.Street).Skip(number - 1);

            return first.Intersect(second);
        }

        public static bool SequenceEqualHotels(this IQueryable<Hotel> hotels)
        {
            if (hotels.SequenceEqual(hotels.Take(hotels.Count())))
                return true;
            else
                return false;
        }

        public static Shop GetFirstWithBigDailyProceeds(this IQueryable<Shop> shops, double bigDailyProceeds)
        {
            return shops.First(s => s.DailyProceeds > bigDailyProceeds);
        }

        public static Restaurant GetFirstOrDefaultRestaurantWithSmallAerageBill(this IQueryable<Restaurant> restaurants, int smallAerageBill)
        {
            return restaurants.FirstOrDefault(r => r.AverageBill < smallAerageBill);
        }

        public static Hotel GetLastHorel(this IQueryable<Hotel> hotels)
        {
            return hotels.Last();
        } 

        public static Restaurant GetLastRestaurant(this IQueryable<Restaurant> restaurants)
        {
            return restaurants.LastOrDefault();
        }

        public static Shop GetSinglShopWithBrand(this IQueryable<Shop> shops, string brand)
        {
            return shops.Single(s => s.Brand == brand);
        }

        public static Hotel GetSingHotelWhithNumberRooms(this IQueryable<Hotel> hotels, int numberRooms)
        {
            return hotels.SingleOrDefault(h => h.NumberRooms == numberRooms);
        }

        public static Restaurant GetElementAtIndex(IQueryable<Restaurant> restaurants, int index)
        {
            return restaurants.ElementAt(index);
        }

        public static Shop GetShopAtIndex(IQueryable<Shop> shops, int index)
        {
            return shops.ElementAtOrDefault(index);
        }

        public static bool HasSequenceAnyElements(IQueryable<Hotel> hotels)
        {
            return hotels.Any();
        }

        public static bool IsRoomyRestaurants(IQueryable<Restaurant> restaurants, int capacityVisitors)
        {
            return restaurants.All(r => r.CapacityVisitors > capacityVisitors);
        }
        
        public static bool HasSequenceStreet(IQueryable<Shop> shops, string street)
        {
            return shops.Select(s=>s.Street).Contains(street);
        }

        public static int GetSumShopsSquareInMetrs(IQueryable<Shop> shops)
        {
            return shops.Select(s => s.SquareInMetrs).Sum();
        }

        public static int GetYougestHotel(IQueryable<Hotel> hotels)
        {
            return hotels.Select(h => h.YearBuilt).Min();
        }

        public static double GetAverageDailyProceeds(IQueryable<Shop> shops)
        {
            return shops.Select(s => s.DailyProceeds).Average();
        }

        public static void ShowOutput(this IDataModel dataModel)
        {
            var adultHotels = dataModel.Hotels.GetHotelsForAdult().ToArray();
            foreach(var hotel in adultHotels)
            {
                Console.WriteLine($"Hotels for adult: {hotel.Name}\t{hotel.NumberRooms}\t" +
                    $"{hotel.YearBuilt}\t{hotel.IsAdultOnle}");
            }

            var allBrans = dataModel.Shops.GetAllBrands().ToArray();
            Console.WriteLine("All brans: ");
            foreach (var brand in allBrans)
            {
                Console.Write(brand + ", ");
            }

            var oneBigName = dataModel.Restaurants.JoinNames().ToArray();
            Console.WriteLine("All names in one line: ");
            foreach(var ch in oneBigName)
            {
                Console.Write(ch);
            }

            var twoShops = dataModel.Shops.GetManyShops(2).ToArray();
            foreach(var shop in twoShops)
            {
                Console.WriteLine($"Two shops: {shop.Name}\t{shop.Brand}" +
                    $"\t{shop.DailyProceeds}\t{shop.SquareInMetrs}\t{shop.Street}");
            }

            var restShops = dataModel.Shops.GetRestShops(2).ToArray();
            foreach (var shop in restShops)
            {
                Console.WriteLine($"Rest shops: {shop.Name}\t{shop.Brand}\t" +
                    $"{shop.DailyProceeds}\t{shop.SquareInMetrs}\t{shop.Street}");
            }

            var whileBiggerBill = dataModel.Restaurants
                                    .GetrRestaurantsWhileBiggerBill(500).ToArray();
            foreach(var restaurant in whileBiggerBill)
            {
                Console.WriteLine($"Restaurans with averange bill bigger 500: {restaurant.Name}\t" +
                    $"{restaurant.Style}\t{restaurant.Kitchen}\t{restaurant.CapacityVisitors}\t" +
                    $"{restaurant.AverageBill}");
            }

            var newHotels = dataModel.Hotels.GetNewHotels(2000).ToArray();
            foreach (var hotel in newHotels)
            {
                Console.WriteLine($"Hotels built after 2000: {hotel.Name}\t{hotel.NumberRooms}\t" +
                    $"{hotel.YearBuilt}\t{hotel.IsAdultOnle}");
            }

            var concatRestaurants = dataModel.Restaurants.GetConcatRestaurants(3).ToArray();
            foreach (var restaurant in concatRestaurants)
            {
                Console.WriteLine($"Concat restaurants, all and skip(3): {restaurant.Name}\t" +
                    $"{restaurant.Style}\t{restaurant.Kitchen}\t{restaurant.CapacityVisitors}\t" +
                    $"{restaurant.AverageBill}");
            }

            var orderShops = dataModel.Shops.OrderShopsBySquare().ToArray();
            foreach (var shop in orderShops)
            {
                Console.WriteLine($"Shops order by square: {shop.Name}\t{shop.Brand}\t" +
                    $"{shop.DailyProceeds}\t{shop.SquareInMetrs}\t{shop.Street}");
            }

            //var groupIdByKitchen = dataModel.Restaurants.GetRestrauntIdsByKitchen().ToArray();
            //foreach(var grouped in groupIdByKitchen)
            //{
            //    Console.WriteLine($"Group restaurants by kitchen: {grouped.Key}\t{grouped.AsQueryable().}");
            //}

            var withoutDublicate = dataModel.Restaurants
                                    .GetStylesRestaurantsWithoutDuplicate().ToList();
            Console.WriteLine("The are kind of kitchenes by restaurants: ");
            foreach(var kitchen in withoutDublicate)
            {
                Console.Write(kitchen + ", ");
            }

            var allNamesHoels = dataModel.Hotels.GetAllNamesHotels(3).ToList();
            Console.WriteLine("All names of hotels: ");
            foreach (var name in allNamesHoels)
            {
                Console.Write(name + ", ");
            }

            var nameShopsInterset = dataModel.Shops.GetIntersectOfShopsStreets(1).ToArray();
            Console.WriteLine("Interset shop street: ");
            foreach(var shop in nameShopsInterset)
            {
                Console.WriteLine(shop + "\t");
            }

            var equalHotels = dataModel.Hotels.SequenceEqualHotels();
            Console.WriteLine("All hotels to SequenceEqual " + equalHotels);

            var profitableShop = dataModel.Shops.GetFirstWithBigDailyProceeds(5000);
            Console.WriteLine($"First shop with daily proceeds more than 5000: {profitableShop.Name}\t" +
                $"{profitableShop.Brand}\t{profitableShop.DailyProceeds}\t" +
                 $"{profitableShop.SquareInMetrs}\t{profitableShop.Street}");

            var smallBill = dataModel.Restaurants
                .GetFirstOrDefaultRestaurantWithSmallAerageBill(400);
            if(smallBill!=null)
            {
                Console.WriteLine($"First restaurant with average bill less than 400: {smallBill.Name}\t" +
                    $"{smallBill.Style}\t{smallBill.Kitchen}\t{smallBill.CapacityVisitors}\t" +
                    $"{smallBill.AverageBill}");
            }

            var lastHotel = dataModel.Hotels.GetLastHorel();
            Console.WriteLine($"Last hotel: {lastHotel.Name}\t{lastHotel.NumberRooms}\t" +
                    $"{lastHotel.YearBuilt}\t{lastHotel.IsAdultOnle}");

            var lastRestaurant = dataModel.Restaurants.GetLastRestaurant();
            if (lastRestaurant!=null)
            {
                Console.WriteLine($"Last restaurant: {lastRestaurant.Name}\t" +
                    $"{lastRestaurant.Style}\t{lastRestaurant.Kitchen}\t{lastRestaurant.CapacityVisitors}\t" +
                    $"{lastRestaurant.AverageBill}");
            }

            var singlShop = dataModel.Shops.GetSinglShopWithBrand("ErichKrause");
            Console.WriteLine($"Shop with ErichKrause brand: {profitableShop.Name}\t" +
                $"{profitableShop.Brand}\t{profitableShop.DailyProceeds}\t" +
                 $"{profitableShop.SquareInMetrs}\t{profitableShop.Street}");

            var singleHotel = dataModel.Hotels.GetSingHotelWhithNumberRooms(120);
            if (singleHotel != null)
            {
                Console.WriteLine($"Hotel with 120 rooms: {singleHotel.Name}\t{singleHotel.NumberRooms}\t" +
                                    $"{singleHotel.YearBuilt}\t{singleHotel.IsAdultOnle}");
            }

         
            //public static Restaurant GetElementAtIndex(IQueryable<Restaurant> restaurants, int index)
            //{
            //    return restaurants.ElementAt(index);
            //}

            //public static Shop GetShopAtIndex(IQueryable<Shop> shops, int index)
            //{
            //    return shops.ElementAtOrDefault(index);
            //}

            //public static bool HasSequenceAnyElements(IQueryable<Hotel> hotels)
            //{
            //    return hotels.Any();
            //}

            //public static bool IsRoomyRestaurants(IQueryable<Restaurant> restaurants, int capacityVisitors)
            //{
            //    return restaurants.All(r => r.CapacityVisitors > capacityVisitors);
            //}

            //public static bool HasSequenceStreet(IQueryable<Shop> shops, string street)
            //{
            //    return shops.Select(s => s.Street).Contains(street);
            //}

            //public static int GetSumShopsSquareInMetrs(IQueryable<Shop> shops)
            //{
            //    return shops.Select(s => s.SquareInMetrs).Sum();
            //}

            //public static int GetYougestHotel(IQueryable<Hotel> hotels)
            //{
            //    return hotels.Select(h => h.YearBuilt).Min();
            //}

            //public static double GetAverageDailyProceeds(IQueryable<Shop> shops)
            //{
            //    return shops.Select(s => s.DailyProceeds).Average();
            //}

            Console.ReadKey();
        }
    }
}
