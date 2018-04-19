using LinqCore;
using LinqCore.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleOutput
{
    public class JsonLinqDataModel : IDataModel
    {
        private readonly JObject DataProvider;

        public JsonLinqDataModel(string pathToDataFile)
        {
            DataProvider = JObject.Parse(System.IO.File.ReadAllText(pathToDataFile));
        }

        public IQueryable<Device> Devices
        {
            get
            {
                return DataProvider["Devices"].Select(d => new Device()
                {
                    Id = d["Id"].Value<int>(),
                    Name = d["Name"].Value<string>(),
                    Category = d["Category"].Value<string>(),
                    IsAvailable = d["IsAvailable"].Value<bool>(),
                    Price = d["Price"].Value<decimal>()
                }).AsQueryable();
            }
        }

        public IQueryable<Order> Orders
        {
            get
            {
                return DataProvider["Orders"].Select(o => new Order()
                {
                    Id = o["Id"].Value<int>(),
                    CustomerId = o["CustomerId"].Value<int>(),
                    Date = o["Date"].Value<DateTime>(),
                    Total = o["Total"].Value<decimal>()
                }).AsQueryable();
            }
        }

        public IQueryable<Customer> Customers
        {
            get
            {
                return DataProvider["Customers"].Select(c => new Customer()
                {
                    Id = c["Id"].Value<int>(),
                    Name = c["Name"].Value<string>(),
                    DateOfBirth = c["DateOfBirth"].Value<DateTime>(),
                    PhoneNumber = c["PhoneNumber"].Value<string>(),
                    Status = c["Status"].Value<string>()
                }).AsQueryable();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IDataModel dataModel = new JsonLinqDataModel(".\\data.json");

            dataModel.ShowOutput();
            Console.ReadLine();
        }
    }
}
