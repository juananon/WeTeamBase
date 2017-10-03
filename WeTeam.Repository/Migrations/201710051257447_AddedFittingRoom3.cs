namespace WeTeam.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFittingRoom3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FittingRooms", "Localization", c => c.String());
            AddColumn("dbo.FittingRooms", "FittingRoomStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FittingRooms", "FittingRoomStatus");
            DropColumn("dbo.FittingRooms", "Localization");
        }
    }
}
