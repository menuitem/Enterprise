namespace HotelWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_checkout_column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomBookings", "isCheckedOut", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomBookings", "isCheckedOut");
        }
    }
}
