namespace La27Barberia.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Barbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        PhotoRoute = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        BarberType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        ClientId = c.Int(nullable: false),
                        BarberId = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        EstimatedMinutes = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsInQueue = c.Boolean(nullable: false),
                        HasStarted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Barbers", t => t.BarberId, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.BarberId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        Identification = c.String(maxLength: 20),
                        Birthday = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Identification, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Tickets", "BarberId", "dbo.Barbers");
            DropIndex("dbo.Clients", new[] { "Identification" });
            DropIndex("dbo.Tickets", new[] { "BarberId" });
            DropIndex("dbo.Tickets", new[] { "ClientId" });
            DropTable("dbo.Clients");
            DropTable("dbo.Tickets");
            DropTable("dbo.Barbers");
        }
    }
}
