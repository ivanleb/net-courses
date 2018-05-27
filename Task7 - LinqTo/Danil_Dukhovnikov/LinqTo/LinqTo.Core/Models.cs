using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LinqTo.Core
{
    public sealed class Project
    {
        [JsonProperty("categories")]
        public ICollection<Category> Categories { get; set; }
        //public IQueryable<Category> Categories { get; set; }
       
        [JsonProperty("projectBalance")]
        public decimal ProjectBalance { get; set; }
        
        [JsonProperty("projectBudget")]
        public decimal ProjectBudget { get; set; }
        
        [JsonProperty("projectId")]
        public string ProjectId { get; set; }
        
        [JsonProperty("projectName")]
        public string ProjectName { get; set; }
    }
       
    public sealed class Category
    {
        [JsonProperty("categoryId")]
        public string CategoryId { get; set; }
        
        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }
        
        [JsonProperty("transactions")]
        public ICollection<Transaction> Transactions { get; set; }
        //public IQueryable<Transaction> Transactions { get; set; }
    }
    
    public class Transaction
    {
        [JsonProperty("comment")]
        public string Comment { get; set; }
        
        [JsonProperty("instant")]
        public DateTime Instant { get; set; }
        
        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }
        
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }

}