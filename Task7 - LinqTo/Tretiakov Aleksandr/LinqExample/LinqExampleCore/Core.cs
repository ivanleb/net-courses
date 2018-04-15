using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqExampleCore
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public List<int> Orders { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }

    public interface IDataModel
    {
        IQueryable<Product> Products { get; }
        IQueryable<Order> Orders { get;}
        IQueryable<Customer> Customers { get; }
    }

    public static class DataRetriever
    {
        public static IQueryable<Product> GetProductsWithPriceLessThan10K(this IQueryable<Product> products)
        {
            return products.Where(p => p.UnitPrice <= 10000);
        }

        public static IQueryable<string> GetProductsName(this IQueryable<Product> products)
        {
            return products.Select(x => x.Name);
        }

        public static decimal GetAverageCost(this IQueryable<Product> products)
        {
            return products.Average(w => w.UnitPrice);
        }

        public static IQueryable<Order> GetLastFiveOrders(this IQueryable<Order> orders)
        {
            return orders.OrderByDescending(x => x.OrderDate).Take(5);
        }

        public static IQueryable<int> GetOrdersFromCountry(this IQueryable<Customer> customers, string country)
        {
            return customers.Where(c => c.Country == country).SelectMany(x => x.Orders);
        }

        public static IQueryable<Product> GetAllButMostExpensive2(this IQueryable<Product> products)
        {
            return products.OrderByDescending(p => p.UnitPrice).Skip(2);
        }
        

        public static IQueryable<string> GetAdresses(this IQueryable<Customer> customers)
        {
            return customers.Select(c => c.City).
                    Concat(customers.Select(c => c.Region)).
                    Concat(customers.Select(c => c.Country)).
                    Distinct();
        }

        public static IQueryable<IGrouping<string, Product>> GetGroupedProducts(this IQueryable<Product> products)
        {
            return products.GroupBy(p => p.Category);
        }

        public static decimal GetMinCost(this IQueryable<Product> books)
        {
            return books.Min(x => x.UnitPrice);
        }

        public static decimal GetMaxCost(this IQueryable<Product> books)
        {
            return books.Max(x => x.UnitPrice);
        }

        public static IQueryable<Product> GetProductsInStock(this IQueryable<Product> products)
        {
            return products.Where(x => x.UnitsInStock != 0);
        }


        public static decimal GetCostOfAllCustomerOrders(this IQueryable<Order> orders, int customerId)
        {
            return orders.Where(x => x.CustomerId == customerId).Select(x => x.Total).Sum();
        }

        public static bool IsCustomerPlacedOrders(this IQueryable<Order> orders, int customerId)
        {
            return orders.Any(x => x.CustomerId == customerId);
        }

        public static void ShowData(this IDataModel dataModel)
        {
            Console.WriteLine("Products:");
            dataModel.Products.ShowProducts();
            Console.WriteLine("Orders:");
            dataModel.Orders.ShowOrders();
            Console.WriteLine("Customers:");
            dataModel.Customers.ShowCustomers();
            
        }

        public static void ShowCustomers(this IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.Write("{0, -5}|", customer.Id);
                Console.Write("{0, -15}|", customer.Name);
                Console.Write("{0, -30}|", customer.Address);
                Console.Write("{0, -15}|", customer.Country);
                Console.Write("{0, -10}|", customer.Region);
                Console.WriteLine();
            }
        }

        public static void ShowOrders(this IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                Console.Write("{0, -5}|", order.Id);
                Console.Write("{0, -5}|", order.CustomerId);
                Console.Write("{0, -15}|", order.OrderDate);
                Console.Write("{0, -10}|", order.Total);
                Console.WriteLine();
            }
        }

        public static void ShowProducts(this IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                Console.Write("{0, -20}|", product.Id);
                Console.Write("{0, -20}|", product.Name);
                Console.Write("{0, -20}|", product.Category);
                Console.Write("{0, -20}|", product.UnitPrice);
                Console.Write("{0, -20}|", product.UnitsInStock);
                Console.WriteLine();
            }
        }


        public static void ShowOperations(this IDataModel dataModel)
        {
            var adresses = dataModel.Customers.GetAdresses().ToList();
            ShowTitle("Adresses:");
            foreach (var adress in adresses)
            {
                Console.WriteLine(adress);
            }
            var id = 1;
            var cost = dataModel.Orders.GetCostOfAllCustomerOrders(id);
            ShowTitle($"Total cost of customer {id} orders is {cost}");
            var latestFiveOrders = dataModel.Orders.GetLastFiveOrders().ToList();
            ShowTitle("Latest five orders:");
            latestFiveOrders?.ShowOrders();
            var isCustomerPlacedOrders = dataModel.Orders.IsCustomerPlacedOrders(id);
            ShowTitle($"Customer {id} plased orders is {isCustomerPlacedOrders}");
            var allButMostExpensive10 = dataModel.Products.GetAllButMostExpensive2().ToList();
            allButMostExpensive10?.ShowProducts();
            var averageCost = dataModel.Products.GetAverageCost();
            ShowTitle($"Average product cost is ${averageCost}");
            var groupedProducts = dataModel.Products.GetGroupedProducts().ToList();
            foreach (var groupedProduct in groupedProducts)
            {
                ShowTitle($"Group \"{groupedProduct.Key}\":");
                groupedProduct.AsQueryable().ShowProducts();
            }
            var minCost = dataModel.Products.GetMinCost();
            ShowTitle($"Min product cost is ${minCost}");
            var maxCost = dataModel.Products.GetMaxCost();
            ShowTitle($"Max product cost is ${maxCost}");
            var productsInStock = dataModel.Products.GetProductsInStock().ToList();
            ShowTitle("Products in stock:");
            productsInStock?.ShowProducts();
            var productsName = dataModel.Products.GetProductsName().ToList();
            ShowTitle("Products names:");
            foreach (var productName in productsName)
            {
                Console.WriteLine(productName);
            }
            var productsWithPriceLessThan10 = dataModel.Products.GetProductsWithPriceLessThan10K().ToList();
            ShowTitle("Products With Price Less Than 10K:");
            productsWithPriceLessThan10?.ShowProducts();
        }

        public static void ShowTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
