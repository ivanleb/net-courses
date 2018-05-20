using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ORMCore;

namespace EntityFramework.Implementations
{
    public class TPTContext : BaseDbContext
    {
        public TPTContext(string connectionstring) : base(connectionstring)
        {
            Database.SetInitializer<TPTContext>(new EfInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Shareholder>().ToTable("Shareholders");
            modelBuilder.Entity<Balance>().ToTable("Balances");
            modelBuilder.Entity<Trade>().ToTable("Trades");
        }

        class EfInitializer : DropCreateDatabaseIfModelChanges<TPTContext>
        {
            protected override void Seed(TPTContext context)
            {
                var lastShareholder = context.Shareholders.AsEnumerable().LastOrDefault();

                int lastShareholderId = 0;

                if (lastShareholder != null)
                    lastShareholderId = lastShareholder.Id;

                var shareholders = new List<Shareholder>
                {
                    new Shareholder {Id=lastShareholderId+1, FirstName = "FirstName test1",LastName ="LastName test1", PhoneNumber= "Phone test1" },
                    new Shareholder {Id=lastShareholderId+2, FirstName = "FirstName test2",LastName ="LastName test2", PhoneNumber= "Phone test2" },
                    new Shareholder {Id=lastShareholderId+3, FirstName = "FirstName test3",LastName ="LastName test3", PhoneNumber= "Phone test3" },
                    new Shareholder {Id=lastShareholderId+4, FirstName = "FirstName test4",LastName ="LastName test4", PhoneNumber= "Phone test4" },
                };

                shareholders.ForEach(p => context.Shareholders.Add(p));

                var balances = new List<Balance>
                {
                    new Balance {Id= lastShareholderId+1,FirstType= 1000, SecondType= 1000, ThirdType= 1000, BalanceValue= 7000, BalanceZone= "middle" },
                    new Balance {Id= lastShareholderId+2,FirstType= 1000, SecondType= 1000, ThirdType= 1000, BalanceValue= 7000, BalanceZone= "middle" },
                    new Balance {Id= lastShareholderId+3,FirstType= 1000, SecondType= 1000, ThirdType= 1000, BalanceValue= 7000, BalanceZone= "middle" },
                    new Balance {Id= lastShareholderId+4,FirstType= 1000, SecondType= 1000, ThirdType= 1000, BalanceValue= 7000, BalanceZone= "middle" },
                };
                balances.ForEach(p => context.Balances.Add(p));

                context.SaveChanges();
            }
        }

    }
}
