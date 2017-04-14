namespace ChatApplication.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserInformationToRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomRecords", "UserId", c => c.Long(nullable: false));
            CreateIndex("dbo.RoomRecords", "UserId");
            AddForeignKey("dbo.RoomRecords", "UserId", "dbo.UserRecords", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomRecords", "UserId", "dbo.UserRecords");
            DropIndex("dbo.RoomRecords", new[] { "UserId" });
            DropColumn("dbo.RoomRecords", "UserId");
        }
    }
}
