using System.Collections.Generic;
using System.Linq;
using System.Net;
using LinqTo.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LinqToJsonProvider
{
    internal static class Program
    {
        private static string Url { get; set; } = "https://simple-money-tracker.herokuapp.com/projects"; 
        
        public static void Main(string[] args)
        {
            var myDataModel = new JsonLinqDataModel();
            
            myDataModel.ShowOutput();
        }
        
        public static string GetAllProjets()
        {    
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(Url);
                var responseJson = JsonConvert.DeserializeObject<List<Project>>(response);
                var responseJsonString = JsonConvert.SerializeObject(responseJson, Formatting.Indented);

                return responseJsonString;
            }
        }
    }
}