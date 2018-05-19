using System;
using System.Linq;
using System.Collections.Generic;

namespace ORM.ConsoleApp
{
    public static class ListExtensions
    {
        private static readonly Random rng = new Random();

        public static T RandomElement<T>(this ICollection<T> collection)
        {
            return collection.ElementAtOrDefault(rng.Next(collection.Count));
        }
    }
}
