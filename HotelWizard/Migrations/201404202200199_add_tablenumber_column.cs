namespace HotelWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tablenumber_column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RestaurantBookings", "TableNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RestaurantBookings", "TableNumber");
        }
    }
}
