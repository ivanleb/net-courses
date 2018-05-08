namespace StockExchangeSimulator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingClientClass2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clients", "Stock_Id", "dbo.Stocks");
            DropIndex("dbo.Clients", new[] { "Stock_Id" });
            AlterColumn("dbo.Clients", "Stock_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Clients", "Stock_Id");
            AddForeignKey("dbo.Clients", "Stock_Id", "dbo.Stocks", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "Stock_Id", "dbo.Stocks");
            DropIndex("dbo.Clients", new[] { "Stock_Id" });
            AlterColumn("dbo.Clients", "Stock_Id", c => c.Int());
            CreateIndex("dbo.Clients", "Stock_Id");
            AddForeignKey("dbo.Clients", "Stock_Id", "dbo.Stocks", "Id");
        }
    }
}
