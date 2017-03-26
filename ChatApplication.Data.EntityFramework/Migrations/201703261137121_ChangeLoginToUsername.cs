namespace ChatApplication.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLoginToUsername : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.LoginRecords", new[] { "Login" });
            AddColumn("dbo.LoginRecords", "Username", c => c.String(nullable: false, maxLength: 30));
            CreateIndex("dbo.LoginRecords", "Username");
            DropColumn("dbo.LoginRecords", "Login");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoginRecords", "Login", c => c.String(nullable: false, maxLength: 30));
            DropIndex("dbo.LoginRecords", new[] { "Username" });
            DropColumn("dbo.LoginRecords", "Username");
            CreateIndex("dbo.LoginRecords", "Login");
        }
    }
}
