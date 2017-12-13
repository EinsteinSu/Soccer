namespace Soccer.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test5 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.GameDatas", new[] { "ReplacingPlayerId" });
            AlterColumn("dbo.GameDatas", "ReplacingPlayerId", c => c.Int());
            CreateIndex("dbo.GameDatas", "ReplacingPlayerId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.GameDatas", new[] { "ReplacingPlayerId" });
            AlterColumn("dbo.GameDatas", "ReplacingPlayerId", c => c.Int(nullable: false));
            CreateIndex("dbo.GameDatas", "ReplacingPlayerId");
        }
    }
}
