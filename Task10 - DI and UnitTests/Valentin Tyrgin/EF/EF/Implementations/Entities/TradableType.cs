using System;
using EF.Core.Abstractions;

namespace EF.Implementations.Entities
{
    public class TradableType : IEntity
    {
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }

        public string GetInfo()
        {
            throw new NotImplementedException();
        }
    }
}