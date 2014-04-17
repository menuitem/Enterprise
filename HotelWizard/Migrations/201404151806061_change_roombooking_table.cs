namespace HotelWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_roombooking_table : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RoomBookings", "roomNum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoomBookings", "roomNum", c => c.Int(nullable: false));
        }
    }
}
