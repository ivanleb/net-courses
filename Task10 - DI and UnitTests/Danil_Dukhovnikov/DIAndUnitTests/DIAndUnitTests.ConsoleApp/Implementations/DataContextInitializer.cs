using System.Collections.Generic;
using System.Data.Entity;
using DIAndUnitTests.Core.Extentions;
using DIAndUnitTests.Core.Models;

namespace DIAndUnitTests.ConsoleApp.Implementations
{
    public class DataContextInitializer : DropCreateDatabaseAlways<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var defaultTraders = new List<Trader>
            {
                new Trader()
                {
                    Name = "Andrey",
                    Surname = "Vasilev",
                    PhoneNumber = "(900) 714-1733"
                },
                new Trader()
                {
                    Name = "Aleksey",
                    Surname = "Ivanov",
                    PhoneNumber = "(810) 812-8855"
                },
                new Trader()
                {
                    Name = "Kirill",
                    Surname = "Naumov",
                    PhoneNumber = "(999) 885-4571"
                },
                new Trader()
                {
                    Name = "Egor",
                    Surname = "Maslov",
                    PhoneNumber = "(112) 963-2541"
                },
                new Trader()
                {
                    Name = "Dmitriy",
                    Surname = "Nevzorov",
                    PhoneNumber = "(918) 984-1237"
                },
                new Trader()
                {
                    Name = "Vladislav",
                    Surname = "Kirpichniy",
                    PhoneNumber = "(900) 315-1000"
                },
                new Trader()
                {
                    Name = "Danil",
                    Surname = "Nesterov",
                    PhoneNumber = "(812) 100-1010"
                },
                new Trader()
                {
                    Name = "Igor",
                    Surname = "Noskov",
                    PhoneNumber = "(312) 777-0105"
                },
                new Trader()
                {
                    Name = "Alexander",
                    Surname = "Shpak",
                    PhoneNumber = "(112) 745-5588"
                }
            };

            var defaultNamePrice = new[]
            {
                new {Name = "Apple", Pice = 2500M},
                new {Name = "Facebook", Pice = 1500M},
                new {Name = "Google", Pice = 2800M},
                new {Name = "Lenovo", Pice = 800M},
                new {Name = "Samsung", Pice = 1200M},
                new {Name = "Asus", Pice = 1400M},
                new {Name = "Telegram", Pice = 2200M},
                new {Name = "Microsoft", Pice = 1900M},
                new {Name = "Toshiba", Pice = 1300M},
                new {Name = "Sony", Pice = 2100M}
            };

            var defaultShares = new List<Share>();
            for (var i = 0; i < 20; i++)
            {
                var tempNamePrice = defaultNamePrice.GetRandomElement();
                var tempOwner = defaultTraders.GetRandomElement();

                var tempShare = new Share()
                {
                    Owner = tempOwner,
                    Name = tempNamePrice.Name,
                    Price = tempNamePrice.Pice
                };

                tempOwner.SharesCollection.Add(tempShare);
                defaultShares.Add(tempShare);
            }

            context.Shares.AddRange(defaultShares);
            context.Traders.AddRange(defaultTraders);
            base.Seed(context);
        }
    }
}