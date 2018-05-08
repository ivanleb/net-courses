namespace StockExchangeSimulator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        SurName = c.String(),
                        TelephonNumber = c.String(),
                        Balance = c.Int(nullable: false),
                        Zone = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Price = c.Int(nullable: false),
                        Type = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Buyer_Id = c.Int(),
                        Seller_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Buyer_Id)
                .ForeignKey("dbo.Clients", t => t.Seller_Id)
                .Index(t => t.Buyer_Id)
                .Index(t => t.Seller_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Seller_Id", "dbo.Clients");
            DropForeignKey("dbo.Transactions", "Buyer_Id", "dbo.Clients");
            DropIndex("dbo.Transactions", new[] { "Seller_Id" });
            DropIndex("dbo.Transactions", new[] { "Buyer_Id" });
            DropTable("dbo.Transactions");
            DropTable("dbo.Stocks");
            DropTable("dbo.Clients");
        }
    }
}
