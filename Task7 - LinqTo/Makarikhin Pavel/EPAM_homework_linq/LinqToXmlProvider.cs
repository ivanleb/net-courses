using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EPAM_homework_linq
{
    class LinqToXmlProvider : IDataModel
    {
        private readonly XDocument dataProvider;

        public LinqToXmlProvider(string pathToDataFile)
        {
            this.dataProvider = XDocument.Load(System.IO.File.OpenRead(pathToDataFile));
        }

        public IQueryable<Song> Songs
        {
            get
            {
                return dataProvider.Element("root").Element("Songs")
                    .Elements().Select(s => new Song()
                    {
                        Name = s.Element("Name").Value,
                        Author = s.Element("Author").Value,
                        Album = s.Element("Album").Value,
                        ChartPosition = int.Parse(s.Element("ChartPosition").Value),
                        Duration = double.Parse(s.Element("Duration").Value)
                    }).AsQueryable();
            }
        }

        public IQueryable<Picture> Pictures
        {
            get
            {
                return dataProvider.Element("root").Element("Pictures")
                    .Elements().Select(s => new Picture()
                    {
                        Name = s.Element("Name").Value,
                        Author = s.Element("Author").Value,
                        Style = s.Element("Style").Value,
                        Age = int.Parse(s.Element("Age").Value),
                        Cost = double.Parse(s.Element("Cost").Value)
                    }).AsQueryable();
            }
        }

        public IQueryable<Movie> Movies
        {
            get
            {
                return dataProvider.Element("root").Element("Movies")
                    .Elements().Select(s => new Movie()
                    {
                        Name = s.Element("Name").Value,
                        Genre = s.Element("Genre").Value,
                        Rating = double.Parse(s.Element("Rating").Value),
                        Duration = double.Parse(s.Element("Duration").Value)
                    }).AsQueryable();
            }
        }
    }
}
