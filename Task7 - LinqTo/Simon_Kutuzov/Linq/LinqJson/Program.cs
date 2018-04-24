using LinqCore;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace LinqJson
{
    class JsonLinqDataModel : IDataModel
    {
        private readonly JObject dataProvider;

        public JsonLinqDataModel(string path)
        {
            this.dataProvider = JObject.Parse(System.IO.File.ReadAllText(path));
        }

        public IQueryable<Guitar> Guitars
        {
            get
            {
                return dataProvider["Guitars"].Select(g => new Guitar()
                {
                    Id = g["id"].Value<int>(),
                    Brand = g["Brand"].Value<string>(),
                    Name = g["Name"].Value<string>(),
                    PickupConfig = g["PickupConfig"].Value<string>(),
                    Color = g["Color"].Value<string>(),
                    Frets = g["Frets"].Value<int>(),
                    Strings = g["Strings"].Value<int>(),
                    Price = g["Price"].Value<int>(),
                    IncludedAccessories = g["IncludedAccessories"].Value<string>()
                }).AsQueryable();
            }
        }

        public IQueryable<Amplifier> Amplifiers
        {
            get
            {
                return dataProvider["Amplifiers"].Select(a => new Amplifier()
                {
                    Id = a["id"].Value<int>(),
                    Brand = a["Brand"].Value<string>(),
                    Name = a["Name"].Value<string>(),
                    Nobs = a["Nobs"].Value<string>(),
                    Effects = a["Effects"].Value<string>(),
                    MaxPower = a["MaxPower"].Value<int>(),
                    Price = a["Price"].Value<int>(),
                }).AsQueryable();
            }
        }

        public IQueryable<Keyboard> Keyboards
        {
            get
            {
                return dataProvider["Keyboards"].Select(k => new Keyboard()
                {
                    Id = k["id"].Value<int>(),
                    Brand = k["Brand"].Value<string>(),
                    Name = k["Name"].Value<string>(),
                    Keys = k["Keys"].Value<int>(),
                    Price = k["Price"].Value<int>(),
                }).AsQueryable();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IDataModel dataModel = new JsonLinqDataModel(@".\..\..\data.json");
            dataModel.ShowOutput();
        }
    }
}
