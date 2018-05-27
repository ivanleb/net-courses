using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using LinqTo.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LinqToJsonProvider
{
    public class JsonLinqDataModel : IDataModel
    {
        private static string Url { get; } = "https://simple-money-tracker.herokuapp.com/projects";
        private readonly JObject _dataProvider;

        public JsonLinqDataModel()
        {
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(Url);

                _dataProvider = JObject.Parse(response);
            }
        }

        public JsonLinqDataModel(string path)
        {
            this._dataProvider = JObject.Parse(System.IO.File.ReadAllText(path));
        }

        public IQueryable<Project> Projects
        {
            get
            {
                return _dataProvider["Projects"].Select(s => new Project()
                {
                    ProjectBalance = s["projectBalance"].Value<decimal>(),
                    ProjectBudget = s["projectBudget"].Value<decimal>(),
                    ProjectId = s["projectId"].Value<string>(),
                    ProjectName = s["projectName"].Value<string>(),
                    Categories = (ICollection<Category>) s["categories"].Select(p => new Category()
                    {
                        CategoryId = p["categoryId"].Value<string>(),
                        CategoryName = p["categoryName"].Value<string>(),
                        Transactions = (ICollection<Transaction>) p["transactions"].Select(t => new Transaction()
                        {
                            Amount = t["amount"].Value<decimal>(),
                            Comment = t["comment"].Value<string>(),
                            Instant = t["instant"].Value<DateTime>(),
                            TransactionId = t["transactionId"].Value<string>()
                        })
                    })
                }) as IQueryable<Project>;
            }
        }
    }
}