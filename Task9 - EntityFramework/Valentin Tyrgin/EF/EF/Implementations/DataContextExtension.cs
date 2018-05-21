using System;
using System.Linq;
using EF.Core.Services;

namespace EF.Core
{
    public static class DataContextExtension
    {
        public static void ShowAll(this IQueryable<IGetInfo> entities, string header)
        {
            if (!entities.Any()) return;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Logger.Log.Info("\n"+header);
            Console.ForegroundColor = ConsoleColor.Gray;

            foreach (var x in entities)
                Logger.Log.Info(x.GetInfo());
        }
    }
}