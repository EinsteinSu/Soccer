using System;
using System.Linq;
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
            var playerId = AddPlayer(schedule.HostId, "Buffon", 1);
            var data = new GameData
            {
                ScheduleId = id,
                PlayerId = playerId,
                YellowCard1 = DateTime.Now
            };
            Mgr.Add(data);
            return data.Id;
        }

        public int AddGameData()
        {
            return AddStuff();
        }

        protected override void UpdateProperty(GameData item)
        {
            item.IsFirst = true;
        }

        protected override GameData FindStuff(int id)
        {
            return Context.GameDatas.FirstOrDefault(f => f.Id == id);
        }

        protected override void AssertUpdate(GameData item, GameData newItem)
        {
            Assert.AreEqual(item.IsFirst, newItem.IsFirst);
        }

        protected override void AssertDelete(GameData item)
        {
            Assert.IsNull(item);
        }
    }
}