namespace StockExchangeSimulator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStockTypeToClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "ClientStocksQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "Stock_Id", c => c.Int());
            CreateIndex("dbo.Clients", "Stock_Id");
            AddForeignKey("dbo.Clients", "Stock_Id", "dbo.Stocks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "Stock_Id", "dbo.Stocks");
            DropIndex("dbo.Clients", new[] { "Stock_Id" });
            DropColumn("dbo.Clients", "Stock_Id");
            DropColumn("dbo.Clients", "ClientStocksQuantity");
        }
    }
}
