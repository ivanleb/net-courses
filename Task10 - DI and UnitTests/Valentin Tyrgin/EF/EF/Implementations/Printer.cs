using System;
using System.Linq;
using EF.Core.Abstractions;
using EF.Core.Services;

namespace EF.Implementations
{
    internal class Printer : IPrinter
    {
        private readonly ILogService logService;

        public Printer(ILogService logService)
        {
            this.logService = logService;
        }

        public void ShowAll(IQueryable<IEntityService> entities, string header)
        {
            if (!entities.Any()) return;
            Console.ForegroundColor = ConsoleColor.Yellow;
            logService.Info("\n" + header);
            Console.ForegroundColor = ConsoleColor.Gray;

            foreach (var x in entities)
                logService.Info(x.GetInfo());
        }

        public void ShowMessage(string str)
        {
            Console.WriteLine(str);
        }

        public static string PrintInCentre(string str, int width = 12)
        {
            str = str ?? "";
            return str.PadRight(width - (width - str.Length) / 2).PadLeft(width);
        }
    }
}