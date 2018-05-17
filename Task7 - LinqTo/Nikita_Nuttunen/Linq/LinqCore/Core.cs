using LinqCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqCore
{
    public interface IDataModel
    {
        IQueryable<Device> Devices { get; }
        IQueryable<Order> Orders { get; }
        IQueryable<Customer> Customers { get; }
    }

    public static class DataRetriever
    {
        #region MethodsForDevices

        // First
        public static Device GetDeviceById(this IQueryable<Device> devices, int deviceId)
        {
            return devices.First(p => p.Id == deviceId);
        }

        // FirstOrDefault
        public static Device GetDeviceByName(this IQueryable<Device> devices, string deviceName)
        {
            return devices.FirstOrDefault(d => d.Name == deviceName);
        }

        // Where, AsEnumerable
        public static IQueryable<Device> GetAvailableDevicesInCategory(this IQueryable<Device> devices, string deviceCategory)
        {
            return devices.AsEnumerable().Where(d => d.Category == deviceCategory && d.IsAvailable).AsQueryable();
        }

        // For AsEnumerable example
        private static IEnumerable<Device> Where(IQueryable<Device> devices, Func<Device, bool> predicate)
        {
            return devices.Where(predicate);
        }

        // Select, Count
        public static int GetNumberOfCategories(this IQueryable<Device> devices, string productCategory)
        {
            return devices.Select(d => d.Category).Count();
        }
        
        // OrderBy, ThenBy, ToList
        public static List<Device> GetDevicesSortedByCategoryAndPrice(this IQueryable<Device> devices)
        {
            return devices
                .OrderBy(d => d.Category)
                .ThenBy(d => d.Price).ToList();
        }

        // Take
        public static IQueryable<Device> GetMostExpensiveDevices(this IQueryable<Device> devices, int number)
        {
            return devices
                .OrderByDescending(d => d.Price)
                .Take(number);
        }

        // TakeWhile, ToArray
        public static Device[] GetCheapDevicesArray(this IQueryable<Device> devices, decimal maxPrice)
        {
            return devices
                .OrderBy(d => d.Price)
                .TakeWhile(d => d.Price <= maxPrice)
                .ToArray();
        }

        // Cast
        public static IQueryable<Device> ArrayToQueryable(this Device[] devices)
        {
            return devices.Cast<Device>().AsQueryable();
        }

        // Min
        public static decimal GetLowestDevicePrice(this IQueryable<Device> devices)
        {
            return devices.Min(d => d.Price);
        }

        // Max
        public static decimal GetHighestDevicePrice(this IQueryable<Device> devices)
        {
            return devices.Max(d => d.Price);
        }

        // Skip
        public static IQueryable<Device> GetLastAddedDevices(this IQueryable<Device> devices, int number)
        {
            return devices.Skip(devices.Count() - number);
        }


        #endregion

        #region MethodsForCustomers

        // Last (LastOrDefault)
        public static Customer GetLastAddedSilverCustomer(this IQueryable<Customer> customers)
        {
            return customers.LastOrDefault(c => c.Status == "Silver");
        }
        
        // SkipWhile
        public static IQueryable<Customer> GetOldestCustomers(this IQueryable<Customer> customers, int minAge)
        {
            return customers
                .OrderByDescending(c => c.DateOfBirth)
                .SkipWhile(c => GetAge(c.DateOfBirth) < minAge);
        }

        private static int GetAge(DateTime dateOfBirth)
        {
            DateTime now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day) )
            {
                age--;
            }
            return age;
        }


        // Concat
        public static IQueryable<string> GetCustomersNamesAndPhoneNumbers(this IQueryable<Customer> customers)
        {
            return customers.Select(c => c.Name)
                .Concat(customers.Select(c => c.PhoneNumber));
        }

        // Reverse
        public static void ReverseCustomers(this IQueryable<Customer> customers)
        {
            customers.Reverse();
        }

        // Distinct
        public static IQueryable<string> GetStatuses(this IQueryable<Customer> customers)
        {
            return customers.Select(c => c.Status).Distinct();
        }

        // OfType
        public static IQueryable<Order> GetAllOrders(this IQueryable<Customer> customers)
        {
            return customers.OfType<Order>();
        }
        
        // Any
        public static bool ContainsPhoneNumber(this IQueryable<Customer> customers, string phoneNumber)
        {
            return customers.Any(c => c.PhoneNumber == phoneNumber);
        }

        // All
        public static bool AllCustomersAreAdults(this IQueryable<Customer> customers)
        {
            return customers.All(c => GetAge(c.DateOfBirth) >= 18);
        }

        #endregion

        #region MethodsForOrders

        // Single (SingleOrDefault)
        public static Order GetOrderById(this IQueryable<Order> orders, int id)
        {
            return orders.SingleOrDefault(o => o.Id == id);
        }

        // ElementAt (ElementAtOrDefault)
        public static Order GetFirstOrderByUser(this IQueryable<Order> orders, int id)
        {
            return orders
                .Where(o => o.CustomerId == id)
                .ElementAt(0);
        }


        // Average
        public static decimal GetAverageOrderPrice(this IQueryable<Order> orders)
        {
            return orders.Average(o => o.Total);
        }

        // Sum
        public static decimal GetTotalEarnings(this IQueryable<Order> orders)
        {
            return orders.Sum(o => o.Total);
        }

        // GroupBy
        public static IQueryable<IGrouping<DateTime, int>> GetOrdersIdsByDate(this IQueryable<Order> orders)
        {
            return orders.GroupBy(o => o.Date, o => o.CustomerId);
        }

        // Contains
        public static bool CustomerMadeAtLeastOneOrder(this IQueryable<Order> orders, Customer customer)
        {
            return orders.Select(o => o.CustomerId).Contains(customer.Id);
        }

        #endregion


        public static void ShowOutput(this IDataModel model)
        {
            IQueryable<Device> devices = model.Devices;
            IQueryable<Customer> customers = model.Customers;
            IQueryable<Order> orders = model.Orders;
            
            IQueryable<Device> availableDevicesInCategory = devices.GetAvailableDevicesInCategory("Smartphones");
            Console.WriteLine("Available smartphones:");
            foreach (var device in availableDevicesInCategory)
            {
                Console.WriteLine(device.ToString());
            }

            Console.WriteLine("\nDevices sorted by Category then by Price:");
            List<Device> sortedDevices = devices.GetDevicesSortedByCategoryAndPrice();
            foreach (var device in sortedDevices)
            {
                Console.WriteLine(device.ToString());
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();

            Console.WriteLine("Top 5 most expensive devices:");
            IQueryable<Device> mostExpensiveDevices = devices.GetMostExpensiveDevices(5);
            foreach (var device in mostExpensiveDevices)
            {
                Console.WriteLine(device.ToString());
            }

            // SkipWhile doesn't work with LINQ to Entities
            /*Console.WriteLine("\nCustomers Older than 30:");
            IQueryable<Customer> oldCustomers = customers.GetOldestCustomers(30);
            foreach (var customer in oldCustomers)
            {
                Console.WriteLine(customer.ToString());
            }*/

            Console.WriteLine("\nAvailable statuses for customers:");
            IQueryable<string> statuses = customers.GetStatuses();
            foreach (var status in statuses)
            {
                Console.Write("{0} ", status);
            }

            // Join
            Console.WriteLine("\nOrder date by customer:");            
            var orderDates = customers.Join(orders, c => c.Id, o => o.Id, (c, o) => new { c.Name, o.Date });
            foreach (var orderDate in orderDates)
            {
                Console.WriteLine($"{orderDate.Name, 20} | {orderDate.Date.ToShortDateString()}");
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();

            decimal averageOrderPrice = orders.GetAverageOrderPrice();
            Console.WriteLine("\nAverage order price: {0,2}", averageOrderPrice);

            decimal totalEarnings = orders.GetTotalEarnings();
            Console.WriteLine("\nTotal earnings: {0}", totalEarnings);
            
        }           
    }
}
