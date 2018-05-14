using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Core;
using static System.Xml.Linq.XDocument;
using LinqToDbEf;

namespace LinqToJsonProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataModel dataModel = new JsonLinqModel("data.json");
            dataModel.ShowData();
            #region Creating data.xml from data.json

            var doc = new XmlDocument();
            var root = doc.CreateElement("Root");
            doc.AppendChild(root);
            foreach (var collection in dataModel.GetType().GetProperties())
            {
                var firstChild = doc.CreateElement(collection.Name);
                root.AppendChild(firstChild);
                foreach (var collectionElement in (IQueryable)collection.GetGetMethod().Invoke(dataModel,null))
                {
                    var element = doc.CreateElement("element");
                    firstChild.AppendChild(element);
                    foreach (var property in collectionElement.GetType().GetProperties())
                    {
                        var propElement = doc.CreateElement(property.Name);
                        propElement.InnerText = property
                            .GetGetMethod()
                            .Invoke(collectionElement, null)
                            .ToString();
                        element.AppendChild(propElement);
                    }
                }
            }
            var solutionDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)
                .Parent
                .Parent
                .Parent;
            var xmlDebugDirectory = Directory.EnumerateDirectories(solutionDirectory.FullName)
                .SelectMany(x => Directory.EnumerateDirectories(new DirectoryInfo(x).FullName))
                .SelectMany(x => Directory.EnumerateDirectories(new DirectoryInfo(x).FullName))
                .First(x=>x.Contains("Xml")&& x.Contains("bin")&& x.Contains("Debug"));
            doc.Save(xmlDebugDirectory+"\\data.xml");
            #endregion

            #region Creating sql db from data.json
            using (var db = new LinqToDbModel())
            {
                db.Cars.AddRange(dataModel.Cars);
                db.Orders.AddRange(dataModel.Orders);
                db.Dealers.AddRange(dataModel.Dealers);
                db.SaveChanges();
            }
            #endregion
        }
    }
}
