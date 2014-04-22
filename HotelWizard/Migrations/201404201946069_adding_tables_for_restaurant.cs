namespace HotelWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_tables_for_restaurant : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RestaurantBookings",
                c => new
                    {
                        RestaurantBookingId = c.Int(nullable: false, identity: true),
                        BookingDate = c.DateTime(nullable: false),
                        BookingTime = c.Time(nullable: false, precision: 7),
                        NumOfPeople = c.Int(nullable: false),
                        customerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RestaurantBookingId)
                .ForeignKey("dbo.RestaurantCustomers", t => t.customerID, cascadeDelete: true)
                .Index(t => t.customerID);
            
            CreateTable(
                "dbo.RestaurantCustomers",
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
            DropForeignKey("dbo.RestaurantBookings", "customerID", "dbo.RestaurantCustomers");
            DropIndex("dbo.RestaurantBookings", new[] { "customerID" });
            DropTable("dbo.RestaurantCustomers");
            DropTable("dbo.RestaurantBookings");
        }
    }
}
