namespace ChatApplication.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDescriptionToRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomRecords", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomRecords", "Description");
        }
    }
}
