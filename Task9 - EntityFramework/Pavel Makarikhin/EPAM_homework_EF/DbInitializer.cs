using EPAM_homework_EF_Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_EF
{
    class DbInitializer : DropCreateDatabaseIfModelChanges<TablePerConcreteClass>
    {
        private readonly BussinesService bussinesService;

        public DbInitializer(BussinesService bussinesService)
        {
            this.bussinesService = bussinesService;
        }

        protected override void Seed(TablePerConcreteClass context)
        {
            bussinesService.RegisterShare("BlizzardEntertainment");
            bussinesService.RegisterShare("Ubisoft");
            bussinesService.RegisterShare("ElectronicArt");
            bussinesService.RegisterShare("CDPR");

            bussinesService.RegisterNewClient("Andrey", "Alexandrov", "124191249", 250);
            bussinesService.RegisterNewClient("Vitaliy", "Fedotov", "9124719125", 250);
            bussinesService.RegisterNewClient("Oleg", "Olegov", "182741259", 500);
            bussinesService.RegisterNewClient("Nikolay", "Egorov", "23258301124", 300);
            bussinesService.RegisterNewClient("Tatyana", "Petrova", "7126481249", 350);

            foreach (Client client in context.Clients.ToList())
                bussinesService.RegisterClientShares(client);

            context.SaveChanges();
        }
    }
}
