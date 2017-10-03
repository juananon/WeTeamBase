namespace WeTeam.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFittingRoom2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FittingRoomBookings", "FittingRoomId", "dbo.FittingRooms");
            DropIndex("dbo.FittingRoomBookings", new[] { "FittingRoomId" });
            AlterColumn("dbo.FittingRoomBookings", "Time", c => c.DateTime());
            AlterColumn("dbo.FittingRoomBookings", "FittingRoomId", c => c.Int());
            CreateIndex("dbo.FittingRoomBookings", "FittingRoomId");
            AddForeignKey("dbo.FittingRoomBookings", "FittingRoomId", "dbo.FittingRooms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FittingRoomBookings", "FittingRoomId", "dbo.FittingRooms");
            DropIndex("dbo.FittingRoomBookings", new[] { "FittingRoomId" });
            AlterColumn("dbo.FittingRoomBookings", "FittingRoomId", c => c.Int(nullable: false));
            AlterColumn("dbo.FittingRoomBookings", "Time", c => c.DateTime(nullable: false));
            CreateIndex("dbo.FittingRoomBookings", "FittingRoomId");
            AddForeignKey("dbo.FittingRoomBookings", "FittingRoomId", "dbo.FittingRooms", "Id", cascadeDelete: true);
        }
    }
}
