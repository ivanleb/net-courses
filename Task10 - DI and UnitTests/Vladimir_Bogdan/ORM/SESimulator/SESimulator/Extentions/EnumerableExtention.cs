using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESimulator.Extentions
{
    public static class EnumerableExtention
    {
        private static Random rnd = new Random();
        public static T GetRandom<T>(this IEnumerable<T> collection)
        {
            return collection.ElementAt(rnd.Next(collection.Count()));
        }
    }
}
