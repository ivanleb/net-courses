using EF.Core.Services;
using log4net;

namespace EF.Core.Implementations
{
    public class Logger : ILogService
    {
        private readonly ILog logger;

        public Logger(ILog logger)
        {
            this.logger = logger;
        }

        public void Info(string msg)
        {
            logger.Info(msg);
        }

        public void Error(string msg)
        {
            logger.Error(msg);
        }

        public void Warn(string msg)
        {
            logger.Warn(msg);
        }
    }
}