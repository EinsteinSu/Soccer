using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soccer.DataAccess;

namespace Soccer.Business.Tests
{
    [TestClass]
    public class GoalMgrTest : TestBase
    {

        public IEnumerable<GameData> AddGoal()
        {
            var mgr = new ScheduleMgr();
            var scheduleId = AddSchedule(mgr);
            var schedule = mgr.GetItem(scheduleId);
            AddTeamPlayers(schedule.HostId, 18);
            AddTeamPlayers(schedule.GuestId, 18);
            mgr.GenerateGameData(scheduleId);
            return mgr.GetDataByScheduleId(scheduleId);
        }

        [TestMethod]
        public void GetGoalsInTheGame()
        {
            var items = AddGoal();
            var item = items.FirstOrDefault();
            Assert.IsNotNull(item);
            var goalMgr = new GoalMgr(Context);
            //2 goals
            goalMgr.AddGoal(item.Id);
            goalMgr.AddGoal(item.Id);

            var actual = goalMgr.GetGoalsInTheGame(item.Id).ToList();
            Assert.AreEqual(2, actual.Count);

            //1 goal after remove a goal
            goalMgr.RemoveGoal(actual[0].Id);

            actual = goalMgr.GetGoalsInTheGame(item.Id).ToList();
            Assert.AreEqual(1, actual.Count);
        }

        [TestMethod]
        public void GetGoalsByPlayerId()
        {
            var items = AddGoal();
            var item = items.FirstOrDefault();
            Assert.IsNotNull(item);
            var playerId = item.PlayerId;

            var goalMgr = new GoalMgr(Context);
            //2 goals
            goalMgr.AddGoal(item.Id);
            goalMgr.AddGoal(item.Id);
            var actual = goalMgr.GetGoalsByPlayerId(playerId);
            Assert.AreEqual(2, actual.Count());
        }
    }
}