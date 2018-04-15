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
