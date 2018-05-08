namespace StockExchangeSimulator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingClientClass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stocks", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stocks", "Name", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
