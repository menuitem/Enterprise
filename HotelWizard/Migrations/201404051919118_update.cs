namespace HotelWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomBookings", "costs_totalCost", c => c.Double(nullable: false));
            AddColumn("dbo.RoomBookings", "costs_depositDue", c => c.Double(nullable: false));
            AddColumn("dbo.RoomBookings", "costs_depositPaid", c => c.Double(nullable: false));
            AddColumn("dbo.RoomBookings", "costs_balanceDue", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomBookings", "costs_balanceDue");
            DropColumn("dbo.RoomBookings", "costs_depositPaid");
            DropColumn("dbo.RoomBookings", "costs_depositDue");
            DropColumn("dbo.RoomBookings", "costs_totalCost");
        }
    }
}
