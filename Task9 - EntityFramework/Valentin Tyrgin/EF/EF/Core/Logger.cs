using System.Reflection;
using EF.Implementations;
using log4net;
using log4net.Config;
using log4net.Core;
using static log4net.LogManager;

namespace EF
{
    public static class Logger
    {
        public static ILog Log { get; } = GetLogger("LOGGER");

        public static void InitLogger()
        {
            //LoggerExtension.Addas();
            XmlConfigurator.Configure();
            Log.Operation(Operation.Header);
        }
    }

    public static class LoggerExtension
    {
        private static readonly Level operationLevel = new Level(20000, "Operation");
        //private static readonly Level traidersLevel = new Level(20001, "Traider");
        //private static readonly Level stockLevel = new Level(20002, "Stock");

        //public static void Addas()
        //{
        //    log4net.LogManager.GetRepository().LevelMap.Add(LoggerExtension.operationLevel);
        //    log4net.LogManager.GetRepository().LevelMap.Add(LoggerExtension.traidersLevel);
        //    log4net.LogManager.GetRepository().LevelMap.Add(LoggerExtension.stockLevel);
        //}

        public static void Operation(this ILog log, string message)
        {
            log.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, operationLevel, message, null);
        }

        public static void OperationFormat(this ILog log, string message, params object[] args)
        {
            var formattedMessage = string.Format(message, args);
            log.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, operationLevel, formattedMessage, null);
        }

        //    log.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, traidersLevel, message, null);
        //{

        //public static void Traider(this ILog log, string message)
        //}

        //public static void TraiderFormat(this ILog log, string message, params object[] args)
        //{
        //    string formattedMessage = String.Format(message, args);
        //    log.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, traidersLevel, formattedMessage, null);
        //}

        //public static void Stock(this ILog log, string message)
        //{
        //    log.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, stockLevel, message, null);
        //}

        //public static void StockFormat(this ILog log, string message, params object[] args)
        //{
        //    string formattedMessage = String.Format(message, args);
        //    log.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, stockLevel, formattedMessage, null);
        //}
    }
}