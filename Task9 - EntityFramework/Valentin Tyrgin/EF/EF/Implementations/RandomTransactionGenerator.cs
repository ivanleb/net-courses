using System;
using System.Collections.Generic;
using System.Linq;
using EF.Core;

namespace EF.Implementations
{
    internal class RandomTransactionGenerator : TransactionGenerator
    {
        private static readonly Random Random = new Random();

        public RandomTransactionGenerator(BusinessService bs) : base(bs)
        {
            Active = true;
        }

        public override T GetCollectionItem<T>(ICollection<T> entityCollection)
        {
            var ids = entityCollection.Select(x => x.Id).ToArray();
            var id = Random.Next(0, ids.Length);
            return entityCollection.Single(x => x.Id == ids[id]);
        }

        public int GetValue(Stock stock)
        {
            return Random.Next(1, stock.Quantity);
        }

        public override int GetValue(object ob)
        {
            return GetValue((Stock) ob);
        }
    }
}