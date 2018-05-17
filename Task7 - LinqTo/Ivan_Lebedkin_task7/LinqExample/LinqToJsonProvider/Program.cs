using LinqExampleCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToJsonProvider
{
    class JsonLinqDataModel : IDataModel
    {
        private readonly JObject dataProvider;

        public JsonLinqDataModel(string pathToDataFile)
        {
            this.dataProvider = JObject.Parse(System.IO.File.ReadAllText(pathToDataFile));
        }

        public IQueryable<Book> Books
        {
            get
            {
                return dataProvider["Books"].Select(s => new Book()
                {
                    Name = s["Name"].Value<string>(),
                    Price = s["Price"].Value<decimal>(),
                    Genre = s["Genre"].Value<string>(),
                    IsForSale = s["IsForSale"].Value<bool>(),
                    IsValuable = s["IsValuable"].Value<bool>()
                }).AsQueryable();
            }
        }

        public IQueryable<Dinosaur> Dinosaurs
        {
            get
            {
                return dataProvider["Dinosaurs"].Select(s => new Dinosaur()
                {
                    Name = s["Name"].Value<string>(),
                    Weight = s["Weight"].Value<decimal>(),
                    High = s["High"].Value<decimal>(),
                    IsDangerous = s["IsDangerous"].Value<bool>(),
                    IsFlying = s["IsFlying"].Value<bool>(),
                    IsFloating = s["IsFloating"].Value<bool>()
                }).AsQueryable();
            }
        }

        public IQueryable<HistoricalFigure> HistoricalFigures
        {
            get
            {
                return dataProvider["HistoricalFigures"].Select(s => new HistoricalFigure()
                {
                    Name = s["Name"].Value<string>(),
                    IsHaveBookAbout = s["IsHaveBookAbout"].Value<bool>(),
                    BookAbout = s["BookAbout"].Value<string>(),
                    IsDangerous = s["IsDangerous"].Value<bool>(),
                    ArmCount = s["ArmCount"].Value<int>(),
                    IsReptilian = s["IsReptilian"].Value<bool>()
                }).AsQueryable();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IDataModel dataModel = new JsonLinqDataModel(".\\data.json");
            dataModel.ShowOneArmedHistoricalFigures();
            dataModel.ShowTheBiggestDinosaurs();
        }
    }
}
