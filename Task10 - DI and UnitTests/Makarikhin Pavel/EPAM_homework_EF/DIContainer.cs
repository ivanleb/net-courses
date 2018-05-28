using EPAM_homework_EF_Core;
using log4net;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_EF
{
    class DIContainer : Registry
    {
        public DIContainer()
        {
            For<IProcess>().Use<ProcessSimulation>();
            For<BussinesService>().Use<BussinesService>();
            For<ILoggerService>().Use<LoggerService>().Ctor<ILog>().Is(LogManager.GetLogger("TextLogger"));
            For<IDataContext>().Use<TablePerConcreteClass>().Ctor<string>().Is(@"Data Source=.;Initial Catalog=SharesCompany;Integrated Security=True");
        }
    }
}
