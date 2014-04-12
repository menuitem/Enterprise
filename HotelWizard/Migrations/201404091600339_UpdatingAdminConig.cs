namespace HotelWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingAdminConig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdminConfigs", "HotelName", c => c.String());
            AddColumn("dbo.AdminConfigs", "HotelAddress", c => c.String());
            AddColumn("dbo.AdminConfigs", "NumOfRooms", c => c.Int(nullable: false));
            AddColumn("dbo.AdminConfigs", "NumOfTables", c => c.Int(nullable: false));
            DropColumn("dbo.AdminConfigs", "numRooms");
            DropColumn("dbo.AdminConfigs", "numTables");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdminConfigs", "numTables", c => c.Int(nullable: false));
            AddColumn("dbo.AdminConfigs", "numRooms", c => c.Int(nullable: false));
            DropColumn("dbo.AdminConfigs", "NumOfTables");
            DropColumn("dbo.AdminConfigs", "NumOfRooms");
            DropColumn("dbo.AdminConfigs", "HotelAddress");
            DropColumn("dbo.AdminConfigs", "HotelName");
        }
    }
}
