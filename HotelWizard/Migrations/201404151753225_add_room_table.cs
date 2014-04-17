namespace HotelWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_room_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        roomNum = c.Int(nullable: false),
                        roomRate = c.Int(nullable: false),
                        roomType = c.String(),
                    })
                .PrimaryKey(t => t.RoomId);
            
            AddColumn("dbo.RoomBookings", "room_RoomId", c => c.Int());
            CreateIndex("dbo.RoomBookings", "room_RoomId");
            AddForeignKey("dbo.RoomBookings", "room_RoomId", "dbo.Rooms", "RoomId");
            DropColumn("dbo.RoomBookings", "roomRate");
            DropColumn("dbo.RoomBookings", "roomType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoomBookings", "roomType", c => c.String());
            AddColumn("dbo.RoomBookings", "roomRate", c => c.Int(nullable: false));
            DropForeignKey("dbo.RoomBookings", "room_RoomId", "dbo.Rooms");
            DropIndex("dbo.RoomBookings", new[] { "room_RoomId" });
            DropColumn("dbo.RoomBookings", "room_RoomId");
            DropTable("dbo.Rooms");
        }
    }
}
