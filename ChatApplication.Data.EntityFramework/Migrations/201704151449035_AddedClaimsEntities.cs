namespace ChatApplication.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedClaimsEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClaimRecords",
                c => new
                    {
                        ClaimId = c.Long(nullable: false, identity: true),
                        ClaimName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClaimId);
            
            CreateTable(
                "dbo.UserClaimsRecords",
                c => new
                    {
                        UserClaimsId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ClaimId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.UserClaimsId)
                .ForeignKey("dbo.ClaimRecords", t => t.ClaimId, cascadeDelete: true)
                .ForeignKey("dbo.UserRecords", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ClaimId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserClaimsRecords", "UserId", "dbo.UserRecords");
            DropForeignKey("dbo.UserClaimsRecords", "ClaimId", "dbo.ClaimRecords");
            DropIndex("dbo.UserClaimsRecords", new[] { "ClaimId" });
            DropIndex("dbo.UserClaimsRecords", new[] { "UserId" });
            DropTable("dbo.UserClaimsRecords");
            DropTable("dbo.ClaimRecords");
        }
    }
}
