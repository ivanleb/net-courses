using System;
using System.Configuration;
using LinqTo.Core;
using MongoDB.Driver;

namespace LinqToMongodbProvider
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var con = new MongodbLinqDataModel();
            con.ShowOutput();
        }
    }
}