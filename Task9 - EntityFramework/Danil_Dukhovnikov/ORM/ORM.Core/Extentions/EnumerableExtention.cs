using System;
using System.Collections.Generic;
using System.Linq;

namespace ORM.Core.Extentions
{
    public static class EnumerableExtention
    {
        private static readonly Random Rnd = new Random();
        public static T GetRandom<T>(this IEnumerable<T> collection)
        {
            var enumerable = collection as T[] ?? collection.ToArray();
            
            return enumerable.ElementAt(Rnd.Next(enumerable.Length));
        }
    }
}
