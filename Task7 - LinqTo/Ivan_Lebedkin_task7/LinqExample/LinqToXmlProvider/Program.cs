using LinqExampleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqToXmlProvider
{
    class XmlLinqDataModel : IDataModel
    {
        private readonly XDocument dataProvider;

        public XmlLinqDataModel(string pathToDataFile)
        {
            this.dataProvider = XDocument.Load(System.IO.File.OpenRead(pathToDataFile));
        }

        public IQueryable<Book> Books
        {
            get
            {
                return dataProvider.Element("root").Element("Books")
                    .Elements().Select(s => new Book()
                    {
                        Name = s.Element("Name").Value,
                        Price = decimal.Parse(s.Element("Price").Value),
                        Genre = s.Element("Genre").Value,
                        IsForSale = bool.Parse(s.Element("IsForSale").Value),
                        IsValuable = bool.Parse(s.Element("IsValuable").Value)
                    }).AsQueryable();
            }
        }

        public IQueryable<Dinosaur> Dinosaurs
        {
            get
            {
                return dataProvider.Element("root").Element("Dinosaurs")
                    .Elements().Select(s => new Dinosaur()
                    {
                        Name = s.Element("Name").Value,
                        Weight = decimal.Parse(s.Element("Weight").Value),
                        High = decimal.Parse(s.Element("High").Value),
                        IsDangerous = bool.Parse(s.Element("IsDangerous").Value),
                        IsFlying = bool.Parse(s.Element("IsFlying").Value),
                        IsFloating = bool.Parse(s.Element("IsFloating").Value)
                    }).AsQueryable();
            }
        }

        public IQueryable<HistoricalFigure> HistoricalFigures
        {
            get
            {
                return dataProvider.Element("root").Element("HistoricalFigures")
                    .Elements().Select(s => new HistoricalFigure()
                    {
                        Name = s.Element("Name").Value,
                        IsHaveBookAbout = bool.Parse(s.Element("IsHaveBookAbout").Value),
                        BookAbout = s.Element("BookAbout").Value,
                        IsDangerous = bool.Parse(s.Element("IsDangerous").Value),
                        ArmCount = int.Parse(s.Element("ArmCount").Value),
                        IsReptilian = bool.Parse(s.Element("IsReptilian").Value)
                    }).AsQueryable();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IDataModel dataModel = new XmlLinqDataModel(".\\data.xml");

            //dataModel.ShowOutput();
            dataModel.ShowOneArmedHistoricalFigures();
            dataModel.ShowTheBiggestDinosaurs();
        }
    }
}
