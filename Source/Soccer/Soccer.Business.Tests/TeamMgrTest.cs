using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soccer.DataAccess;

namespace Soccer.Business.Tests
{
    [TestClass]
    public class TeamMgrTest : CrudTestBase<Team>
    {

        protected override void InitializeStuffs()
        {
            Mgr = new TeamMgr();
        }

        #region teams
        [TestMethod]
        public override void GetList()
        {
            AddTeam();
            var id = AddTeam();
            Mgr.Delete(id);
            Assert.AreEqual(1, Mgr.GetItems().Count());
        }

        protected override int AddStuff()
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

        protected override void UpdateProperty(Team item)
        {
            item.Name = "China1";
        }

        protected override Team FindStuff(int id)
        {
            return Context.Teams.FirstOrDefault(f => f.Id == id);
        }

        protected override void AssertUpdate(Team item, Team newItem)
        {
            Assert.AreEqual(item.Name, newItem.Name);
        }

        protected override void AssertDelete(Team item)
        {
            Assert.IsTrue(item.Deleted);
        }


        protected int AddTeam()
        {
            return AddTeam(Mgr as TeamMgr);
        }
        #endregion

    }
}
