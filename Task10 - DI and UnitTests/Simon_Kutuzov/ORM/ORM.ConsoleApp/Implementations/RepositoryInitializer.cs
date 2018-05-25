using System.Collections.Generic;
using System.Data.Entity;
using ORM.Core.Model;

namespace ORM.ConsoleApp.Implementations
{
    class RepositoryInitializer : DropCreateDatabaseAlways<Repository>
    {
        protected override void Seed(Repository context)
        {
            var defaultTraiders = new List<Trader>
            {
                new Trader
                {
                    FirstName = "Jalen",
                    SecondName = "Good",
                    PhoneNumber = "(222) 730-9441",
                    Balance = 10000,
                },
                new Trader
                {
                    FirstName = "Titus",
                    SecondName = "Patel",
                    PhoneNumber = "(940) 342-2889",
                    Balance = 10000,
                },
                new Trader
                {
                    FirstName = "Mercedes",
                    SecondName = "Wheeler",
                    PhoneNumber = "(521) 146-6336",
                    Balance = 10000,
                },
                new Trader
                {
                    FirstName = "Allan",
                    SecondName = "Robbins",
                    PhoneNumber = "(865) 647-8174",
                    Balance = 10000,
                },
                new Trader
                {
                    FirstName = "Izabella",
                    SecondName = "Jefferson",
                    PhoneNumber = "(744) 905-6715",
                    Balance = 10000,
                },
            };

            var defaultListings = new List<Listing>
            {
                new Listing
                {
                    Name = "Yandex NV",
                    Price = 2119.50M,
                },
                new Listing
                {
                    Name = "EPAM Systems Inc.",
                    Price = 7877.8712M,
                },
                new Listing
                {
                    Name = "TCS Group Holding",
                    Price = 1265.096M,
                },
                new Listing
                {
                    Name = "ASUSTEK Computer Inc.",
                    Price = 578.24M,
                },
                new Listing
                {
                    Name = "Lenovo Group Limited",
                    Price = 29.9338M,
                }
            };

            var defaultShares = new List<Share>();
            for (int i = 0; i < 50; i++)
            {
                var share = new Share
                {
                    Listing = defaultListings.RandomElement(),
                    Owner = defaultTraiders.RandomElement(),
                };
                defaultShares.Add(share);
            }

            context.Traders.AddRange(defaultTraiders);
            context.Listings.AddRange(defaultListings);
            context.Shares.AddRange(defaultShares);
            base.Seed(context);
        }
    }
}
