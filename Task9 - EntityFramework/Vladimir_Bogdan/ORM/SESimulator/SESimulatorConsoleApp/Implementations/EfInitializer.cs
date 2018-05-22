using SESimulator.Core;
using SESimulator.Extentions;
using SESimulator.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESimulatorConsoleApp.Implementations
{
    public class EfInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
    {
        private readonly BussinesService bussinesService;

        public EfInitializer(BussinesService bussinesService)
        {
            this.bussinesService = bussinesService;
        }

        protected override void Seed(MyDbContext context)
        {
            bussinesService.RegisterNewClient("Ilya", "Muromec", "12345", 1000);
            bussinesService.RegisterNewClient("Elena", "Prekrasnaya", "111", 800);
            bussinesService.RegisterNewClient("Ivan", "Durak", "", 200);
            bussinesService.RegisterNewClient("Vasilisa", "Premudraya", "555", 0);
            bussinesService.RegisterNewClient("Koshey", "Bessmertniy", "666", 3000);

            bussinesService.RegisterNewStockType("Lukoil", 200);
            bussinesService.RegisterNewStockType("Gazprom", 400);
            bussinesService.RegisterNewStockType("Telegram", 400);

            bussinesService.RegisterNewStockToClient("Lukoil", bussinesService.GetAllClients().GetRandom());
            bussinesService.RegisterNewStockToClient("Lukoil", bussinesService.GetAllClients().GetRandom());
            bussinesService.RegisterNewStockToClient("Gazprom", bussinesService.GetAllClients().GetRandom());
            bussinesService.RegisterNewStockToClient("Gazprom", bussinesService.GetAllClients().GetRandom());
            bussinesService.RegisterNewStockToClient("Telegram", bussinesService.GetAllClients().GetRandom());
            bussinesService.RegisterNewStockToClient("Telegram", bussinesService.GetAllClients().GetRandom());

            context.SaveChanges();
        }
    }
}
