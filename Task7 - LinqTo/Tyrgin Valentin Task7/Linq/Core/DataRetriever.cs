using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Core
{
    public static class DataRetriever
    {
        public static void PrintHeader(Type type)
        {
            if(type == typeof(Car)) PrintHeader($"{"Name",-12}{"Brand",-15}{"Engine Capacity",-18}{"HP",-5}" +
                                                      $"{"Consumption",-14}Price");
            if (type == typeof(Dealer)) PrintHeader($"Title\t\tLocation\t\tCars in stock\t\tEmployee");
            if (type == typeof(Order)) PrintHeader($"{"Number",-8}{"Sum",-12}{"Dealer",-15}{"Car",-12}{"Customer",-10}Date");
        }

        public static void Show(this IQueryable<Car> cars)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            PrintHeader(typeof(Car));
            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Name,-12}{car.Brend,-15}{car.EngineCapacity,-18}" +
                                  $"{car.HorsePower,-5}{car.FuelConsumption,-14}{car.Price}");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }

        public static void Show(this IQueryable<Dealer> dealers)
        {
            PrintHeader(typeof(Dealer));

            foreach (var dealer in dealers)
            {
                Console.WriteLine($"{dealer.Title}\t\t{dealer.Location}\t\t{dealer.CarsNumber}\t\t{dealer.Employee}");
            }
            Console.WriteLine();
        }

        public static void Show(this IQueryable<Order> orders, IQueryable<Car> cars, IQueryable<Dealer> dealers)
        {
            PrintHeader(typeof(Order));
            var orederss = (from order in orders
                            join car in cars on order.CarId equals car.Id
                            join dealer in dealers on order.DealerId equals dealer.Id
                            select new { ordId = order.Id,
                                price = car.Price,
                                delerTitle = dealer.Title,
                                carName = car.Name,
                                custName = order.CustomerName,
                                date = order.Date });

            foreach (var order in orederss)
            {
                Console.WriteLine($"{order.ordId,-8}{order.price,-12:N0}" +
                                  $"{order.delerTitle,-15}" +
                                  $"{order.carName,-12}" +
                                  $"{order.custName,-10}{order.date.ToShortDateString()}");
            }
            Console.WriteLine();
        }

        public static IQueryable<Car> GetThreeMostPowerfulCars(this IQueryable<Car> cars)
        {
            return cars.OrderByDescending(x => x.HorsePower).Take(3);
        }

        public static void ShowThreeMostPopularCars(this IQueryable<Car> cars, IQueryable<Order> orders)
        {
            var mostPopCars = orders.GroupBy(x => x.CarId).OrderByDescending(x => x.Count())
                .Take(3)
                .Join(cars, x => x.Key, y => y.Id, (x, y) => new {carName = y.Name, cnt = x.Count()});

            PrintHeader($"{"Car",-14}Sold");
            foreach (var car in mostPopCars)
            {
                Console.WriteLine($"{car.carName,-14}{car.cnt}");
            }
            Console.WriteLine();
        }

        public static void ShowThreeBestSellsDealers(this IQueryable<Dealer> dealers,
            IQueryable<Order> orders, IQueryable<Car> cars)
        {
            var e = (from order in orders
                join car in cars on order.CarId equals car.Id
                join dealer in dealers on order.DealerId equals dealer.Id
                select new {dealer = dealer.Title, sold = car.Price, carName = car.Name})
                .GroupBy(x=>x.dealer).ToDictionary(x=>x.Key,x=>x.Sum(y=>y.sold));

            PrintHeader($"{"Dealer",-15}Sold");

            foreach (var dealer in e)
            {
                Console.WriteLine($"{dealer.Key,-15}{dealer.Value:N0}");
            }
            Console.WriteLine();
        }

        public static void ShowData(this IDataModel dataModel)
        {
            var e = dataModel.Cars.Count();
            dataModel.Cars.Show();
            Console.WriteLine("Three Most Powerful Cars");
            dataModel.Cars.GetThreeMostPowerfulCars().Show();
            Console.WriteLine("Three Most Popular Cars");
            dataModel.Cars.ShowThreeMostPopularCars(dataModel.Orders);
            dataModel.Orders.Show(dataModel.Cars,dataModel.Dealers);
            dataModel.Dealers.ShowThreeBestSellsDealers(dataModel.Orders, dataModel.Cars);
        }

        static void PrintHeader(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
