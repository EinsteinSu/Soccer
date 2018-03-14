namespace Soccer.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class redesignforsimplebinding : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScheduleId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        YellowCard1 = c.DateTime(),
                        YellowCard2 = c.DateTime(),
                        RedCardDirectly = c.DateTime(),
                        IsFirst = c.Boolean(nullable: false),
                        OutTime = c.DateTime(),
                        InTime = c.DateTime(),
                        ReplacingPlayerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId)
                .ForeignKey("dbo.Players", t => t.ReplacingPlayerId)
                .ForeignKey("dbo.Schedules", t => t.ScheduleId, cascadeDelete: true)
                .Index(t => t.ScheduleId)
                .Index(t => t.PlayerId)
                .Index(t => t.ReplacingPlayerId);
            
            CreateTable(
                "dbo.Goals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(),
                        GameDataId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameDatas", t => t.GameDataId, cascadeDelete: true)
                .Index(t => t.GameDataId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                        DisplayName = c.String(nullable: false, maxLength: 10),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        DisplayName = c.String(nullable: false, maxLength: 10),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        DisplayName = c.String(nullable: false, maxLength: 30),
                        StartTime = c.DateTime(),
                        HostId = c.Int(nullable: false),
                        GuestId = c.Int(nullable: false),
                        HostScore = c.Int(nullable: false),
                        GuestScore = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.GuestId)
                .ForeignKey("dbo.Teams", t => t.HostId)
                .Index(t => t.HostId)
                .Index(t => t.GuestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameDatas", "ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.Schedules", "HostId", "dbo.Teams");
            DropForeignKey("dbo.Schedules", "GuestId", "dbo.Teams");
            DropForeignKey("dbo.GameDatas", "ReplacingPlayerId", "dbo.Players");
            DropForeignKey("dbo.GameDatas", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Goals", "GameDataId", "dbo.GameDatas");
            DropIndex("dbo.Schedules", new[] { "GuestId" });
            DropIndex("dbo.Schedules", new[] { "HostId" });
            DropIndex("dbo.Players", new[] { "TeamId" });
            DropIndex("dbo.Goals", new[] { "GameDataId" });
            DropIndex("dbo.GameDatas", new[] { "ReplacingPlayerId" });
            DropIndex("dbo.GameDatas", new[] { "PlayerId" });
            DropIndex("dbo.GameDatas", new[] { "ScheduleId" });
            DropTable("dbo.Schedules");
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
            DropTable("dbo.Goals");
            DropTable("dbo.GameDatas");
        }
    }
}
