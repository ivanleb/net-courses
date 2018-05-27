using System;
using System.Collections.Generic;
using System.Linq;

namespace DIAndUnitTests.Core.Extentions
{
    public static class CollectionExtensions
    {
        private static readonly Random Random = new Random();

        public static T GetRandomElement<T>(this ICollection<T> collection)
        {
            return collection.ElementAt(Random.Next(collection.Count));
        }
    }
}