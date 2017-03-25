namespace ChatApplication.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomRecordDateCreated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomRecords", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomRecords", "DateCreated");
        }
    }
}
