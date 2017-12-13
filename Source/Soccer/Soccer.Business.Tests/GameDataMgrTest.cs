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
    public class GameDataMgrTest : CrudTestBase<GameData>
    {
        protected override void InitializeStuffs()
        {
            Mgr = new GameDataMgr(Context);
        }

        protected override int AddStuff()
        {
            var mgr = new ScheduleMgr();
            var id = AddSchedule(mgr);
            var schedule = mgr.GetItem(id);
            Assert.IsNotNull(schedule);
            AddTeamPlayers(schedule.HostId, 18);
            AddTeamPlayers(schedule.GuestId, 18);
            GameData data = new GameData();
            data.ScheduleId = id;
            return 0;
        }

        protected override void UpdateProperty(GameData item)
        {
            item.IsFirst = true;
        }

        protected override GameData FindStuff(int id)
        {
            throw new NotImplementedException();
        }

        protected override void AssertUpdate(GameData item, GameData newItem)
        {
            throw new NotImplementedException();
        }

        protected override void AssertDelete(GameData item)
        {
            throw new NotImplementedException();
        }
    }
}
