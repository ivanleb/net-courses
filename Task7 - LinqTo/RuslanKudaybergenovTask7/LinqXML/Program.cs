using System;
using Task7_linq;
using System.Linq;
using System.Xml.Linq;

namespace LinqXML
{
    class XmlLinqDataModel : IDataModel
    {
        private readonly XDocument UsersDataProvider;
        private readonly XDocument LocationsDataProvider;

        public XmlLinqDataModel(string pathToUsersData, string pathToLocationsData)
        {
            this.UsersDataProvider = XDocument.Load(System.IO.File.OpenRead(pathToUsersData));
            this.LocationsDataProvider = XDocument.Load(System.IO.File.OpenRead(pathToLocationsData));
        }
        

        public IQueryable<User> Users
        {
            get
            {
                return UsersDataProvider.Element("root").Element("Users")
                    .Elements().Select(s => new User()
                    {
                        ID = int.Parse(s.Element("ID").Value),
                        Name = s.Element("Name").Value,
                        Password = s.Element("Password").Value,
                        Type = s.Element("Type").Value,
                        IDLocation = int.Parse(s.Element("IDLocation").Value)
                    }).AsQueryable();
            }
        }

        public IQueryable<Location> Locations
        {
            get
            {
                return LocationsDataProvider.Element("root").Element("Locations")
                    .Elements().Select(s => new Location()
                    {
                        ID = int.Parse(s.Element("ID").Value),
                        Country = s.Element("Country").Value,
                        City = s.Element("City").Value
                    }).AsQueryable();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string s = System.Environment.CurrentDirectory;

            IDataModel usersDataModel = new XmlLinqDataModel("UersData.xml","LocationsData.xml");
           
            usersDataModel.ShowOutput();
        }
    }
}
