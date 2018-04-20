using PointsGenerator.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointsGenerator.ConsoleApp.Implementations
{
    public class ExceptionIndicator : IExceptionIndicator
    {
        private readonly ILoggerService loggerService;

        public List<IPointProducer> Producers { get; set; }

        public ExceptionIndicator(ILoggerService loggerService)
        {
            Producers = new List<IPointProducer>();
            this.loggerService = loggerService;
        }

        public void WriteException(object sender, IPoint e)
        {
            loggerService.Info($"Exception throwed by {sender.GetType().Name}");
        }
    }
}
