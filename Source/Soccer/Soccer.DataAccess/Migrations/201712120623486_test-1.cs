namespace Soccer.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GameDatas", "PlayerId", "dbo.Players");
            AddColumn("dbo.GameDatas", "IsFirst", c => c.Boolean(nullable: false));
            AddColumn("dbo.GameDatas", "OutTime", c => c.DateTime());
            AddColumn("dbo.GameDatas", "InTime", c => c.DateTime());
            AddColumn("dbo.GameDatas", "ReplacingPlayerId", c => c.Int(nullable: false));
            CreateIndex("dbo.GameDatas", "ReplacingPlayerId");
            AddForeignKey("dbo.GameDatas", "ReplacingPlayerId", "dbo.Players", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GameDatas", "PlayerId", "dbo.Players", "Id");
            DropColumn("dbo.GameDatas", "Replace_IsFirst");
            DropColumn("dbo.GameDatas", "Replace_OutTime");
            DropColumn("dbo.GameDatas", "Replace_InTime");
            DropColumn("dbo.GameDatas", "Replace_ReplacingPlayerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GameDatas", "Replace_ReplacingPlayerId", c => c.Int(nullable: false));
            AddColumn("dbo.GameDatas", "Replace_InTime", c => c.DateTime());
            AddColumn("dbo.GameDatas", "Replace_OutTime", c => c.DateTime());
            AddColumn("dbo.GameDatas", "Replace_IsFirst", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.GameDatas", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.GameDatas", "ReplacingPlayerId", "dbo.Players");
            DropIndex("dbo.GameDatas", new[] { "ReplacingPlayerId" });
            DropColumn("dbo.GameDatas", "ReplacingPlayerId");
            DropColumn("dbo.GameDatas", "InTime");
            DropColumn("dbo.GameDatas", "OutTime");
            DropColumn("dbo.GameDatas", "IsFirst");
            AddForeignKey("dbo.GameDatas", "PlayerId", "dbo.Players", "Id", cascadeDelete: true);
        }
    }
}
