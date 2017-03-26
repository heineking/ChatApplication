namespace ChatApplication.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginRecords",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        Username = c.String(nullable: false, maxLength: 30),
                        Password = c.String(),
                        LoginAttempts = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserRecords", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Username);
            
            CreateTable(
                "dbo.UserRecords",
                c => new
                    {
                        UserId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.MessageRecords",
                c => new
                    {
                        MessageId = c.Long(nullable: false, identity: true),
                        Text = c.String(),
                        PostedDate = c.DateTime(nullable: false),
                        RoomId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
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
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoginRecords", "UserId", "dbo.UserRecords");
            DropForeignKey("dbo.MessageRecords", "UserId", "dbo.UserRecords");
            DropForeignKey("dbo.MessageRecords", "RoomId", "dbo.RoomRecords");
            DropIndex("dbo.MessageRecords", new[] { "UserId" });
            DropIndex("dbo.MessageRecords", new[] { "RoomId" });
            DropIndex("dbo.LoginRecords", new[] { "Username" });
            DropIndex("dbo.LoginRecords", new[] { "UserId" });
            DropTable("dbo.RoomRecords");
            DropTable("dbo.MessageRecords");
            DropTable("dbo.UserRecords");
            DropTable("dbo.LoginRecords");
        }
    }
}
