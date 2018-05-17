using LinqExampleCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Xml.Serialization;

namespace LinqToDbEf
{
    public class DbEfDataContext : DbContext, IDataModel
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        IQueryable<Product> IDataModel.Products => Products;
        IQueryable<Order> IDataModel.Orders => Orders;
        IQueryable<Customer> IDataModel.Customers => Customers;

        public DbEfDataContext(string connectionString) : base(connectionString)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Order>().ToTable("Orders");

        }
    }

    class JsonLinqDataModel : IDataModel
    {
        private readonly JObject _dataProvider;

        public JsonLinqDataModel(string pathToDataFile)
        {
            _dataProvider = JObject.Parse(System.IO.File.ReadAllText(pathToDataFile));
        }

        public IQueryable<Product> Products
        {
            get
            {
                var rofl = _dataProvider["Products"];
                return _dataProvider["Products"].Select(s => new Product
                {
                    Name = s["Name"].Value<string>(),
                    UnitPrice = s["UnitPrice"].Value<decimal>(),
                    Category = s["Category"].Value<string>(),
                    UnitsInStock = s["UnitsInStock"].Value<int>(),
                    Id = s["Id"].Value<int>()
                }).AsQueryable();
            }
        }

        public IQueryable<Order> Orders
        {
            get
            {
                var rofl = _dataProvider["Orders"];
                return _dataProvider["Orders"].Select(s => new Order
                {
                    Id = s["OrderId"].Value<int>(),
                    OrderDate = s["OrderDate"].Value<DateTime>(),
                    CustomerId = s["CustomerId"].Value<int>(),
                    Total = s["Total"].Value<decimal>()
                }).AsQueryable();
            }
        }
        public IQueryable<Customer> Customers
        {
            get
            {
                var rofl = _dataProvider["Customers"];
                return _dataProvider["Customers"].Select(s => new Customer
                {
                    Name = s["Name"].Value<string>(),
                    Address = s["Address"].Value<string>(),
                    City = s["City"].Value<string>(),
                    Region = s["Region"].Value<string>(),
                    Country = s["Country"].Value<string>(),
                    Id = s["Id"].Value<int>(),
                    Orders = s["Orders"].ToObject<List<int>>(),
                }).AsQueryable();
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {

            IDataModel dataModel = new JsonLinqDataModel("..\\..\\..\\LinqToJsonProvider\\data.json");

            using (var dbContext = new DbEfDataContext("DBConnection"))
            {
                /* Delete initialization of DB after uploading data */
                dbContext.Customers.AddRange(dataModel.Customers);
                dbContext.Orders.AddRange(dataModel.Orders);
                dbContext.Products.AddRange(dataModel.Products);
                dbContext.SaveChanges();
                dbContext.ShowData();
                dbContext.ShowOperations();
                Console.ReadLine();
            }
        }
    }
}
