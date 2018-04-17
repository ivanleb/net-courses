using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChesnovLinqCore;
using Newtonsoft.Json.Linq;
using System.IO;

namespace LinqToJsonProvider
{
    public class JsonLinqDataModel : IDataModel
    {
        private readonly JObject dataProvider;

        public JsonLinqDataModel(string pathToDataFile)
        {
            dataProvider = JObject.Parse(File.ReadAllText(pathToDataFile));
        }
        public IQueryable<Game> Games
        {
            get
            {
                return dataProvider["Games"].Select(g => new Game()
                {
                    Name = g["Name"].Value<string>(),
                    Genre = g["Genre"].Value<string>(),
                    Platform = g["Platform"].Value<string>(),
                    CollectorsEdition = g["CollectorsEdition"].Value<bool>(),
                    Price = g["Price"].Value<decimal>(),
                    Rating = g["Rating"].Value<decimal>(),
                    Quantity = g["Quantity"].Value<int>(),
                    MinimumAge = g["MinimumAge"].Value<int>(),
                    DevelopingCompanyId = g["DevelopingCompanyId"].Value<int>(),
                    NumberOfTimesBuyed = g["NumberOfTimesBuyed"].Value<int>(),
                    Id = g["Id"].Value<int>()
                }).AsQueryable();
            }
        }

        public IQueryable<Player> Players
        {
            get
            {
                return dataProvider["Players"].Select(p => new Player()
                {
                    RealName = p["RealName"].Value<string>(),
                    Nickname = p["Nickname"].Value<string>(),
                    Country = p["Country"].Value<string>(),
                    DateOfBirth = p["DateOfBirth"].Value<DateTime>(),
                    Balance = p["Balance"].Value<decimal>(),
                    IsActive = p["IsActive"].Value<bool>(),
                    Id = p["Id"].Value<int>()
                }).AsQueryable();
            }
        }

        public IQueryable<GameCompany> GameCompanies
        {
            get
            {
                return dataProvider["GameCompanies"].Select(c => new GameCompany()
                {
                    Name = c["Name"].Value<string>(),
                    YearOfEstablishment = c["YearOfEstablishment"].Value<int>(),
                    Id = c["Id"].Value<int>()
                }).AsQueryable();
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                IDataModel dataModel = new JsonLinqDataModel("..\\..\\..\\data.json");

                dataModel.ShowAllGames();

                dataModel.ShowOutput();
            }
        }
    }
}
