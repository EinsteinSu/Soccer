namespace Soccer.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Schedules", "HostId");
            CreateIndex("dbo.Schedules", "GuestId");
            AddForeignKey("dbo.Schedules", "GuestId", "dbo.Teams", "Id");
            AddForeignKey("dbo.Schedules", "HostId", "dbo.Teams", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "HostId", "dbo.Teams");
            DropForeignKey("dbo.Schedules", "GuestId", "dbo.Teams");
            DropIndex("dbo.Schedules", new[] { "GuestId" });
            DropIndex("dbo.Schedules", new[] { "HostId" });
        }
    }
}
