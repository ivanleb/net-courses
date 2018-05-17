using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ChesnovLinqCore;
using LinqToJsonProvider;

namespace LinqToDbWithEf
{
    public class LinqToDbWithEf: DbContext, IDataModel
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<GameCompany> GameCompanies { get; set; }

        IQueryable<Game> IDataModel.Games => Games;
        IQueryable<Player> IDataModel.Players => Players;
        IQueryable<GameCompany> IDataModel.GameCompanies => GameCompanies;

        public LinqToDbWithEf(string connectionString) : base(connectionString)
        {

        }

        public void OnModelCreation(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("Games");

            modelBuilder.Entity<Player>().ToTable("Players");

            modelBuilder.Entity<GameCompany>().ToTable("GameCompanies");
        }

        static void Main(string[] args)
        {
            IDataModel dataModel = new JsonLinqDataModel("..\\..\\..\\data.json");

            using (var dbContext = new LinqToDbWithEf("Data Source=chesnov.database.windows.net;Initial Catalog=Task7Chesnov;Integrated Security=False;User ID=Chesanita;Password=c6119900@;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                //БД проинициализирована, так что код ниже трогать не стоит
                //dbContext.Games.AddRange(dataModel.Games);
                //dbContext.Players.AddRange(dataModel.Players);
                //dbContext.GameCompanies.AddRange(dataModel.GameCompanies);
                //dbContext.SaveChanges();
                dbContext.ShowAllGames();
                dbContext.ShowOutput();
                Console.ReadLine();
            }

        }
    }
}
