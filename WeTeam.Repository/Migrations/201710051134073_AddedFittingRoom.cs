namespace WeTeam.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFittingRoom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FittingRoomBookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FittingRoomBookingGuid = c.Guid(nullable: false),
                        FittingRoomBookingStatus = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                        FittingRoomId = c.Int(nullable: false),
                        UserNotificationId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FittingRooms", t => t.FittingRoomId, cascadeDelete: true)
                .Index(t => t.FittingRoomId);
            
            CreateTable(
                "dbo.FittingRooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FittingRoomBookings", "FittingRoomId", "dbo.FittingRooms");
            DropIndex("dbo.FittingRoomBookings", new[] { "FittingRoomId" });
            DropTable("dbo.FittingRooms");
            DropTable("dbo.FittingRoomBookings");
        }
    }
}
