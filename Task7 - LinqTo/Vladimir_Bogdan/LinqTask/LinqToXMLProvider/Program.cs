using LinqCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqToXMLProvider
{
    class XmlLinqDataModel : IDataModel
    {
        private readonly XDocument dataProvider;
        public XmlLinqDataModel(string pathToDataFile)
        {
            this.dataProvider = XDocument.Load(System.IO.File.OpenRead(pathToDataFile));
        }

        public IQueryable<Player> Players => dataProvider.Element("root").Element("Players")
            .Elements().Select(s=>new Player() {
                Id = int.Parse(s.Element("Id").Value),
                Name = s.Element("Name").Value,
                BirthdayDate = DateTime.Parse(s.Element("BirthdayDate").Value),
                StrongestHand = s.Element("Shoots").Value.CastToSide(),
                Salary = decimal.Parse(s.Element("Salary").Value),
                Citizenship = s.Element("Citizenship").Value
            }).AsQueryable();

        public IQueryable<Team> Teams => dataProvider.Element("root").Element("Teams")
            .Elements().Select(s => new Team()
            {
                Id = int.Parse(s.Element("Id").Value),
                Name = s.Element("Name").Value,
                Country = s.Element("Country").Value,
                FoundationDate = DateTime.Parse(s.Element("WasFoundedIn").Value),
                HeadCoach = s.Element("HeadCoach").Value
            }).AsQueryable();

        public IQueryable<Stadium> Stadiums => dataProvider.Element("root").Element("Stadiums")
            .Elements().Select(s => new Stadium()
            {
                Id = int.Parse(s.Element("Id").Value),
                Name = s.Element("Name").Value,
                Capacity = int.Parse(s.Element("Capacity").Value),
                City = s.Element("City").Value
            }).AsQueryable();
    }
    class Program
    {
        static void Main(string[] args)
        {
            IDataModel dataModel = new XmlLinqDataModel(".\\data.xml");
            dataModel.ShowOutput();

        }
    }
}
