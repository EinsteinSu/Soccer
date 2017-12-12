using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soccer.DataAccess;

namespace Soccer.Business.Tests
{
    [TestClass]
    public class PlayerMgrTest : CrudTestBase<Player>
    {
        protected override void InitializeStuffs()
        {
            Mgr = new PlayerMgr();
        }

        protected override int AddStuff()
        {
            var teamId = AddTeam(new TeamMgr());
            var player = new Player
            {
                Name = "Buffon",
                DisplayName = "Buffon",
                Number = 1,
                TeamId = teamId
            };
            Mgr.Add(player);
            Assert.IsTrue(player.Id > 0);
            return player.Id;
        }

        protected override void UpdateProperty(Player item)
        {
            item.Name = "Casillas";
        }

        protected override Player FindStuff(int id)
        {
            return Context.Players.FirstOrDefault(f => f.Id == id);
        }

        protected override void AssertUpdate(Player item, Player newItem)
        {
            Assert.AreEqual(item.Name, item.Name);
        }

        protected override void AssertDelete(Player item)
        {
            Assert.IsTrue(item.Deleted);
        }
    }
}