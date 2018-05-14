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

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Guest> Guests { get; set; }

        IQueryable<Room> IDataModel.Rooms => Rooms;
        IQueryable<Visit> IDataModel.Visits => Visits;
        IQueryable<Guest> IDataModel.Guests => Guests;

        public DbEfDataContext(string connectionString) : base(connectionString)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().ToTable("Rooms");
            modelBuilder.Entity<Guest>().ToTable("Guests");
            modelBuilder.Entity<Visit>().ToTable("Visits");

        }
    }

    class JsonLinqDataModel : IDataModel
    {
        private readonly JObject _dataProvider;

        public JsonLinqDataModel(string pathToDataFile)
        {
            _dataProvider = JObject.Parse(System.IO.File.ReadAllText(pathToDataFile));
        }

        public IQueryable<Room> Rooms
        {
            get
            {
                var rofl = _dataProvider["Rooms"];
                return _dataProvider["Rooms"].Select(s => new Room
                {
                    PricePerDay = s["PricePerDay"].Value<decimal>(),
                    Category = s["Category"].Value<string>(),
                    Id = s["Id"].Value<int>()
                }).AsQueryable();
            }
        }

        public IQueryable<Visit> Visits
        {
            get
            {
                var rofl = _dataProvider["Visits"];
                return _dataProvider["Visits"].Select(s => new Visit
                {
                    Id = s["VisitId"].Value<int>(),
                    VisitDate = s["VisitDate"].Value<DateTime>(),
                    GuestId = s["GuestId"].Value<int>(),
                    DaysNumber = s["DaysNumber"].Value<int>(),
                    Total = s["Total"].Value<decimal>()
                }).AsQueryable();
            }
        }
        public IQueryable<Guest> Guests
        {
            get
            {
                var rofl = _dataProvider["Guests"];
                return _dataProvider["Guests"].Select(s => new Guest
                {
                    Name = s["Name"].Value<string>(),
                    Address = s["Address"].Value<string>(),
                    City = s["City"].Value<string>(),
                    Country = s["Country"].Value<string>(),
                    Id = s["Id"].Value<int>(),
                    Visits = s["Visits"].ToObject<List<int>>(),
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
                dbContext.Guests.AddRange(dataModel.Guests);
                dbContext.Visits.AddRange(dataModel.Visits);
                dbContext.Rooms.AddRange(dataModel.Rooms);
                dbContext.SaveChanges();
                dbContext.ShowData();
                dbContext.ShowOperations();
                Console.ReadLine();
            }
        }
    }
}
