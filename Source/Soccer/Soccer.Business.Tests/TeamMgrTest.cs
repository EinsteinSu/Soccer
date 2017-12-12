using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soccer.DataAccess;

namespace Soccer.Business.Tests
{
    [TestClass]
    public class TeamMgrTest : TestBase
    {
        protected TeamMgr Mgr = new TeamMgr();

        [TestMethod]
        public void GetList()
        {
            AddTeam();
            var id = AddTeam();
            Mgr.Delete(id);
            Assert.AreEqual(1, Mgr.GetItems().Count());
        }

        [TestMethod]
        public void Add()
        {
            AddTeam();
        }

        [TestMethod]
        public void Update()
        {
            var id = AddTeam();
            Assert.IsTrue(id > 0);
        }

        [TestMethod]
        public void Remove()
        {
            var id = AddTeam();
            Mgr.Delete(id);
            var list = Mgr.GetItems();
            Assert.IsTrue(!list.Any());
        }

        protected int AddTeam()
        {
            var team = new Team
            {
                Name = "China",
                DisplayName = "CHN",
                Description = "Balabala"
            };
            Mgr.Add(team);
            Assert.IsTrue(team.Id > 0);
            return team.Id;
        }
    }

    public class TestBase
    {
        protected SoccerContext Context = new SoccerContext();

        [TestInitialize]
        public void Initialize()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Databases"));
        }

        [TestCleanup]
        public void Cleanup()
        {
            Context.Database.ExecuteSqlCommand("Delete from Teams");
        }
    }
}
