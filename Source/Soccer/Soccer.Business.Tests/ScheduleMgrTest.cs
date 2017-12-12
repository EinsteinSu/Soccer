using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var teamMgr = new TeamMgr();
            var host = AddTeam(teamMgr);
            var guest = AddTeam(teamMgr);
            var schedule = new Schedule();
            schedule.Name = "World cup match 1";
            schedule.DisplayName = "World cup";
            schedule.HostId = host;
            schedule.GuestId = guest;
            Mgr.Add(schedule);
            Assert.IsTrue(schedule.Id > 0);
            return schedule.Id;
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

        protected override void AssertDelete(Schedule item)
        {
            Assert.IsTrue(item.Deleted);
        }
    }
}
