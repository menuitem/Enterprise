namespace HotelWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_roomid_to_roombooking : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoomBookings", "room_RoomId", "dbo.Rooms");
            DropIndex("dbo.RoomBookings", new[] { "room_RoomId" });
            RenameColumn(table: "dbo.RoomBookings", name: "room_RoomId", newName: "roomID");
            AlterColumn("dbo.RoomBookings", "roomID", c => c.Int(nullable: false));
            CreateIndex("dbo.RoomBookings", "roomID");
            AddForeignKey("dbo.RoomBookings", "roomID", "dbo.Rooms", "RoomId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomBookings", "roomID", "dbo.Rooms");
            DropIndex("dbo.RoomBookings", new[] { "roomID" });
            AlterColumn("dbo.RoomBookings", "roomID", c => c.Int());
            RenameColumn(table: "dbo.RoomBookings", name: "roomID", newName: "room_RoomId");
            CreateIndex("dbo.RoomBookings", "room_RoomId");
            AddForeignKey("dbo.RoomBookings", "room_RoomId", "dbo.Rooms", "RoomId");
        }
    }
}
