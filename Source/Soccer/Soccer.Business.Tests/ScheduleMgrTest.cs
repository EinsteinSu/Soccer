using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soccer.DataAccess;

namespace Soccer.Business.Tests
{
    [TestClass]
    public class ScheduleMgrTest : CrudTestBase<Schedule>
    {
        protected override void InitializeStuffs()
        {
            Mgr = new ScheduleMgr();
        }

        protected override int AddStuff()
        {
            return AddSchedule(Mgr as ScheduleMgr);
        }

        protected override void UpdateProperty(Schedule item)
        {
            item.Name = "Europe cup";
        }

        protected override Schedule FindStuff(int id)
        {
            return Context.Schedules.FirstOrDefault(f => f.Id == id);
        }

        protected override void AssertUpdate(Schedule item, Schedule newItem)
        {
            Assert.AreEqual(item.Name, newItem.Name);
        }

        [TestMethod]
        public void GenerateGameData()
        {
            var id = AddStuff();
            var mgr = Mgr as ScheduleMgr;
            Assert.IsNotNull(mgr);
            var schedule = mgr.GetItem(id);
            AddTeamPlayers(schedule.HostId, 18);
            AddTeamPlayers(schedule.GuestId, 18);
            mgr.GenerateGameData(id);
            Assert.AreEqual(18 * 2, mgr.GetDataByScheduleId(id).Count());
        }

        protected override void AssertDelete(Schedule item)
        {
            Assert.IsTrue(item.Deleted);
        }
    }
}
