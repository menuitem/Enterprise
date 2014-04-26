namespace HotelWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_event_tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventBookings",
                c => new
                    {
                        EventBookingId = c.Int(nullable: false, identity: true),
                        BookingDate = c.DateTime(nullable: false),
                        BookingTime = c.Time(nullable: false, precision: 7),
                        EventType = c.String(),
                        NumOfPeople = c.Int(nullable: false),
                        customerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventBookingId)
                .ForeignKey("dbo.EventCustomers", t => t.customerID, cascadeDelete: true)
                .Index(t => t.customerID);
            
            CreateTable(
                "dbo.EventCustomers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        SecondName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventBookings", "customerID", "dbo.EventCustomers");
            DropIndex("dbo.EventBookings", new[] { "customerID" });
            DropTable("dbo.EventCustomers");
            DropTable("dbo.EventBookings");
        }
    }
}
