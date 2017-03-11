namespace ChatApplication.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDatePosted : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageRecords",
                c => new
                    {
                        MessageId = c.Long(nullable: false, identity: true),
                        Text = c.String(),
                        PostedDate = c.DateTime(nullable: false),
                        RoomId = c.Long(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.RoomRecords", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.UserRecords", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RoomRecords",
                c => new
                    {
                        RoomId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoomId);
            
            CreateTable(
                "dbo.UserRecords",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageRecords", "UserId", "dbo.UserRecords");
            DropForeignKey("dbo.MessageRecords", "RoomId", "dbo.RoomRecords");
            DropIndex("dbo.MessageRecords", new[] { "UserId" });
            DropIndex("dbo.MessageRecords", new[] { "RoomId" });
            DropTable("dbo.UserRecords");
            DropTable("dbo.RoomRecords");
            DropTable("dbo.MessageRecords");
        }
    }
}
