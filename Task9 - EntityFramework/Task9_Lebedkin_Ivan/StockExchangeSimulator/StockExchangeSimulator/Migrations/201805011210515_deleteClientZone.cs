namespace StockExchangeSimulator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteClientZone : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Clients", "Zone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "Zone", c => c.Byte(nullable: false));
        }
    }
}
