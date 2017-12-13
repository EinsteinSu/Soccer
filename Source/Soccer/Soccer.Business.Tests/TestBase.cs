using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soccer.DataAccess;

namespace Soccer.Business.Tests
{
    public class TestBase
    {
        protected SoccerContext Context = new SoccerContext();

        [TestInitialize]
        public virtual void Initialize()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Databases"));
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SoccerContext>());

        }

        [TestCleanup]
        public void Cleanup()
        {
            Context.Database.ExecuteSqlCommand("Delete from GameDatas");
            Context.Database.ExecuteSqlCommand("Delete from Schedules");
            Context.Database.ExecuteSqlCommand("Delete from Players");
            Context.Database.ExecuteSqlCommand("Delete from Teams");
        }
    }
}