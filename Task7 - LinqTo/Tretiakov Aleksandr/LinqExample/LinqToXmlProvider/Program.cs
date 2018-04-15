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
