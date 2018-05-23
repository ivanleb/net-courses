using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ORM.Implementations;
using ORMCore;
using ORMCore.Model;
using log4net;

namespace ORM
{
    class EfInitializer: CreateDatabaseIfNotExists<TPTUserDbContext>
    {
        protected override void Seed(TPTUserDbContext context)
        {
            //var buisnessService = new BuisnessService(context);

            //buisnessService.AddNewClient("John", "Smith", "85123163241", 500);
            //buisnessService.AddNewClient("Lucifer", "Betrayer", "86669341246", 1500);
            //programmLogic.AddNewClient("Viktor", "Chestinov", "89712351783", 2000);
            //programmLogic.AddNewClient("Gennadiy", "Ikarov", "86123651231", 1700);
            //programmLogic.AddNewClient("Valentina", "Pristavko", "+71426123612", 1200);
            //programmLogic.AddNewClient("The God-Emperor", "Of Mankind", "+71426123612", 5000);
            //programmLogic.AddNewClient("Patrissia", "Bublegum", "+71426123612", 100);

            //logger.Info("Registering new stock types");
            //programmLogic.AddNewStockType("Rosal", 100);
            //programmLogic.AddNewStockType("Nestle", 200);
            //programmLogic.AddNewStockType("Tesla", 60);
            //programmLogic.AddNewStockType("Blackguard", 50);
            //programmLogic.AddNewStockType("Epic Games", 70);
            //programmLogic.AddNewStockType("Sven", 53);
            //programmLogic.AddNewStockType("Pioneer", 25);

            //logger.Info("adding stocks to clients");
            //for (int i = 0; i < 100; i++)
            //{
            //    programmLogic.AddStockToClient(programmLogic.GetStockTypes().GetRandom().Name, programmLogic.GetAllClients().GetRandom());
            //}
        }
    }
}
