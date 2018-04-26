using ORMCore.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMConsoleApp.Implementations
{
    public class Stock : IStock
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Cost { get { return GetStockCost(Type); } }

        public decimal GetStockCost(string type)
        {
            switch (type)
            {
                case "Gazprom":
                    return 1000;
                case "Tesla":
                    return 1500;
                case "Apple":
                    return 1200;
                case "Facebook":
                    return 2000;
                case "Google":
                    return 3000;
                case "Coca-Cola":
                    return 3000;
                case "Lenovo":
                    return 1600;
                case "Samsung":
                    return 2500;
                default:
                    throw new ArgumentException("Stock doesn't exist");
            }
        }
    }
}
