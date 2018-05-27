using System.Data.Entity;
using ORM.Core;
using ORM.Core.Extentions;

namespace ORM.ConsoleApp.Implementations
{
    public class EfInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
    {
        private readonly BussinesService _bussinesService;

        public EfInitializer(BussinesService bussinesService)
        {
            this._bussinesService = bussinesService;
        }

        protected override void Seed(MyDbContext context)
        {
            _bussinesService.RegisterNewClient("TestClientName1", "TestClientSurname1", "TestClientPhone1", 500);
            _bussinesService.RegisterNewClient("TestClientName2", "TestClientSurname2", "TestClientPhone2", 700);
            _bussinesService.RegisterNewClient("TestClientName3", "TestClientSurname3", "TestClientPhone3", 900);
            _bussinesService.RegisterNewClient("TestClientName4", "TestClientSurname4", "TestClientPhone4", 1100);
            _bussinesService.RegisterNewClient("TestClientName5", "TestClientSurname5", "TestClientPhone5", 1300);
            _bussinesService.RegisterNewClient("TestClientName6", "TestClientSurname6", "TestClientPhone6", 1500);
            _bussinesService.RegisterNewClient("TestClientName7", "TestClientSurname7", "TestClientPhone7", 0);
            _bussinesService.RegisterNewClient("TestClientName8", "TestClientSurname8", "TestClientPhone8", 1700);
            _bussinesService.RegisterNewClient("TestClientName9", "TestClientSurname9", "TestClientPhone9", 1900);

            _bussinesService.RegisterNewStockType("TestStockType1", 100);
            _bussinesService.RegisterNewStockType("TestStockType2", 200);
            _bussinesService.RegisterNewStockType("TestStockType3", 300);
            _bussinesService.RegisterNewStockType("TestStockType4", 400);

            _bussinesService.RegisterNewStockToClient("TestStockType4", _bussinesService.GetAllClients().GetRandom());
            _bussinesService.RegisterNewStockToClient("TestStockType3", _bussinesService.GetAllClients().GetRandom());
            _bussinesService.RegisterNewStockToClient("TestStockType2", _bussinesService.GetAllClients().GetRandom());
            _bussinesService.RegisterNewStockToClient("TestStockType1", _bussinesService.GetAllClients().GetRandom());

            context.SaveChanges();
        }
    }
}
