using StockExchangeSimulator.Data.Repositories;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using StockExchangeSimulator.Data.Models;

namespace StockExchangeSimulator.App.Implementations
{


    public class StockExchangeDataContext :  RepositoryCommon
    {
        public StockExchangeDataContext()
            : base("name=StockExchangeDataContext")
        {
            Database.SetInitializer<StockExchangeDataContext>(new DataInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Stock>().ToTable("Stocks");
            modelBuilder.Entity<Transaction>().ToTable("Transactions");
        }



        class DataInitializer : DropCreateDatabaseIfModelChanges<StockExchangeDataContext>
        {
            private readonly JsonInitializer _jsonInitializer = new JsonInitializer("d:\\data.json");

//            internal DataInitializer()
//            {
//                _jsonInitializer =
//            }

            protected override void Seed(StockExchangeDataContext context)
            {
                //            var lastClient = context.Clients.AsEnumerable().LastOrDefault();
                //
                //            int lastClientId = 0;
                //
                //            if (lastClient != null)
                //            {
                //                lastClientId = lastClient.Id;
                //            }

                foreach (var client in _jsonInitializer.Clients)
                {
                    context.Clients.Add(client);
                }

                foreach (var stock in _jsonInitializer.Stocks)
                {
                    context.Stocks.Add(stock);
                }

                context.SaveChanges();
            }
        }


    }


}