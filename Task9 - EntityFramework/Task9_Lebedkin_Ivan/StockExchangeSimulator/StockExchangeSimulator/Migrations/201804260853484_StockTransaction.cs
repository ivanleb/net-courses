namespace StockExchangeSimulator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "StocksQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "Stock_Id", c => c.Int());
            CreateIndex("dbo.Transactions", "Stock_Id");
            AddForeignKey("dbo.Transactions", "Stock_Id", "dbo.Stocks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Stock_Id", "dbo.Stocks");
            DropIndex("dbo.Transactions", new[] { "Stock_Id" });
            DropColumn("dbo.Transactions", "Stock_Id");
            DropColumn("dbo.Transactions", "StocksQuantity");
        }
    }
}
