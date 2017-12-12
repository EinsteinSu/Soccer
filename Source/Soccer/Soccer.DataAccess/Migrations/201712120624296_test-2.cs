namespace Soccer.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GameDatas", "ReplacingPlayerId", "dbo.Players");
            AddForeignKey("dbo.GameDatas", "ReplacingPlayerId", "dbo.Players", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameDatas", "ReplacingPlayerId", "dbo.Players");
            AddForeignKey("dbo.GameDatas", "ReplacingPlayerId", "dbo.Players", "Id", cascadeDelete: true);
        }
    }
}
