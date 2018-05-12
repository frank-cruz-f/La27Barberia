namespace La27Barberia.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Email", c => c.String());
            AddColumn("dbo.Clients", "LastVisit", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "LastVisit");
            DropColumn("dbo.Clients", "Email");
        }
    }
}
