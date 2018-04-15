using LinqCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LinqToJsonProvider
{
    class JsonLinqDataModel : IDataModel
    {
        private readonly JObject dataProvider;

        public JsonLinqDataModel(string pathToDataFile)
        {
            this.dataProvider = JObject.Parse(System.IO.File.ReadAllText(pathToDataFile));
        }

        public IQueryable<Player> Players
        {
            get
            {
                return dataProvider["Players"].Select(s => new Player()
                {
                    Id = s["Id"].Value<int>(),
                    Name = s["Name"].Value<string>(),
                    Citizenship = s["Citizenship"].Value<string>(),
                    Salary = s["Salary"].Value<decimal>(),
                    BirthdayDate = DateTime.Parse(s["BirthdayDate"].Value<string>()),
                    StrongestHand = s["Shoots"].Value<string>().CastToSide()
                }).AsQueryable();
            }
        }

        public IQueryable<Team> Teams
        {
            get
            {
                return dataProvider["Teams"].Select(s => new Team()
                {
                    Id = s["Id"].Value<int>(),
                    Name = s["Name"].Value<string>(),
                    Country = s["Country"].Value<string>(),
                    HeadCoach = s["Coach"].Value<string>(),
                    FoundationDate = s["Foundation"].Value<DateTime>()
                }).AsQueryable();
            }
        }

        public IQueryable<Stadium> Stadiums
        {
            get
            {
                return dataProvider["Stadiums"].Select(s => new Stadium()
                {
                    Id = s["Id"].Value<int>(),
                    Name = s["Name"].Value<string>(),
                    City = s["City"].Value<string>(),
                    Capacity = s["Capacity"].Value<int>()
                }).AsQueryable();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IDataModel dataModel = new JsonLinqDataModel(".\\data.json");
            dataModel.ShowOutput();

        }
    }
}
