namespace Soccer.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Teams", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schedules", "Deleted");
            DropColumn("dbo.Teams", "Deleted");
            DropColumn("dbo.Players", "Deleted");
        }
    }
}
