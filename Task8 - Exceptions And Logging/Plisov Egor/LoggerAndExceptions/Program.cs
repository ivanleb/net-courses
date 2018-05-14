using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace LoggerAndExceptions
{
    class Program
    {

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            ILog logMaster = LogManager.GetLogger("MyLog");
            LoggerService loggerService = new LoggerService(logMaster);

            MinusProducer minusProducer = new MinusProducer(loggerService);
            PlusProducer plusProducer = new PlusProducer(loggerService);
            BadProducer badProducer = new BadProducer(loggerService);

            Client myClient = new Client("My Client");
            myClient.StartListening(badProducer);

            Task.Run(() => { minusProducer.Start((point) => loggerService.Info($"minus function {point}"), 0, 1);  });
            Task.Run(() => { plusProducer.Start((point) => loggerService.Info($"plus function {point}"), 0, 1);    });
            Task.Run(() => { badProducer.Start((point) => loggerService.Info($"bad function {point}"), 0, 1);      });
            
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            while (true)
            {
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1) minusProducer.IsContinue = false;
                if (key.Key == ConsoleKey.D2) plusProducer.IsContinue = false;
                if (key.Key == ConsoleKey.D3) badProducer.IsContinue = false;
            }
        }
    }
}
