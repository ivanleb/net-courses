using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using LinqTo.Core;

namespace LinqToXmlProvider
{
    public class XmlLinqDataModel : IDataModel
    {
        private readonly XDocument _dataProvider;
        
        public XmlLinqDataModel(string pathToDataFile)
        {
            this._dataProvider = XDocument.Load(System.IO.File.OpenRead(pathToDataFile));
        }

        public IQueryable<Project> Projects
        {
            get
            {
                return _dataProvider.Element("root")
                    ?.Element("Projects")
                    ?.Elements().Select(s => new Project()
                {
                    ProjectBalance = decimal.Parse(s.Element("projectBalance")?.Value),
                    ProjectBudget = decimal.Parse(s.Element("projectBudget")?.Value),
                    ProjectId = s.Element("projectId")?.Value,
                    ProjectName = s.Element("projectName")?.Value,

                    Categories = (ICollection<Category>) s.Element("categories")
                        ?.Elements().Select(c => new Category()
                    {
                        CategoryId = c.Element("categoryId")?.Value,
                        CategoryName = c.Element("categoryName")?.Value,

                        Transactions = (ICollection<Transaction>) c.Element("transactions")
                            ?.Elements().Select(t =>
                            new Transaction()
                            {
                                Amount = decimal.Parse(t.Element("amount")?.Value),
                                Comment = t.Element("comment")?.Value,
                                Instant = DateTime.Parse(t.Element("instant")?.Value),
                                TransactionId = t.Element("transactionId")?.Value
                            })
                    })

                }) as IQueryable<Project>;
            }
        }
    }
}