namespace HotelWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_AdminConfig_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminConfigs",
                c => new
                    {
                        AdminConfigId = c.Int(nullable: false, identity: true),
                        numRooms = c.Int(nullable: false),
                        numTables = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdminConfigId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AdminConfigs");
        }
    }
}
