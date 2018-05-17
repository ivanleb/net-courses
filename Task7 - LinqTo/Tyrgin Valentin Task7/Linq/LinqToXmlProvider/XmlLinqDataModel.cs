using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Core;

namespace LinqToXmlProvider
{
    class XmlLinqDataModel : IDataModel
    {
        private readonly XDocument provider;

        public XmlLinqDataModel(string pathToFile)
        {
            this.provider = XDocument.Load(System.IO.File.OpenRead(pathToFile));
        }

        public IQueryable<Car> Cars => provider.Element("Root").Element("Cars")
            .Elements().Select(x => new Car
            {
                Id = int.Parse(x.Element("Id").Value),
                Name = x.Element("Name").Value,
                Price = decimal.Parse(x.Element("Price").Value),
                FuelConsumption = double.Parse(x.Element("FuelConsumption").Value),
                HorsePower = int.Parse(x.Element("HorsePower").Value),
                EngineCapacity = double.Parse(x.Element("EngineCapacity").Value),
                Brend = x.Element("Brend").Value,
            }).AsQueryable();

        public IQueryable<Dealer> Dealers => provider.Element("Root").Element("Dealers")
            .Elements().Select(x => new Dealer
            {
                Id = int.Parse(x.Element("Id").Value),
                Title = x.Element("Title").Value,
                CarsNumber = int.Parse(x.Element("CarsNumber").Value),
                Employee = int.Parse(x.Element("Employee").Value),
                Location = x.Element("Location").Value
            }).AsQueryable();

        public IQueryable<Order> Orders => provider.Element("Root").Element("Orders")
            .Elements().Select(x => new Order
            {
                Id = int.Parse(x.Element("Id").Value),
                Date = DateTime.Parse(x.Element("Date").Value),
                DealerId = int.Parse(x.Element("DealerId").Value),
                CarId = int.Parse(x.Element("CarId").Value),
                CustomerName = x.Element("CustomerName").Value
            }).AsQueryable();
    }
}
