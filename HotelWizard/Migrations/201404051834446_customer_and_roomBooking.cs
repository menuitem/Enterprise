namespace HotelWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customer_and_roomBooking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomBookings",
                c => new
                    {
                        RoomBookingId = c.Int(nullable: false, identity: true),
                        checkin = c.DateTime(nullable: false),
                        checkout = c.DateTime(nullable: false),
                        specialRate = c.Int(nullable: false),
                        roomRate = c.Int(nullable: false),
                        roomType = c.String(),
                        roomNum = c.Int(nullable: false),
                        numPeople = c.Int(nullable: false),
                        isDepositPaid = c.Boolean(nullable: false),
                        isCheckedIn = c.Boolean(nullable: false),
                        customerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomBookingId)
                .ForeignKey("dbo.RoomCustomers", t => t.customerID, cascadeDelete: true)
                .Index(t => t.customerID);
            
            CreateTable(
                "dbo.RoomCustomers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        firstname = c.String(),
                        email = c.String(),
                        phone = c.String(),
                        address = c.String(),
                        nationality = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomBookings", "customerID", "dbo.RoomCustomers");
            DropIndex("dbo.RoomBookings", new[] { "customerID" });
            DropTable("dbo.RoomCustomers");
            DropTable("dbo.RoomBookings");
        }
    }
}
