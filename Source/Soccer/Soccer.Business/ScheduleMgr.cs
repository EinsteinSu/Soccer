using System.Collections.Generic;
using System.Linq;
using Soccer.DataAccess;

namespace Soccer.Business
{
    public interface IScheduleMgr : ICrudMgr<Schedule>
    {
        IEnumerable<GameData> GetDataByScheduleId(int scheduleId);

        void GenerateGameData(int scheduleId);

        void UpdateGameData(GameData data);
    }


    public class ScheduleMgr : CrudMgrBase<Schedule>, IScheduleMgr
    {
        protected override string EntityName => "Schedule";

        public IEnumerable<GameData> GetDataByScheduleId(int scheduleId)
        {
            return Context.GameDatas.Where(w => w.ScheduleId == scheduleId);
        }

        public void GenerateGameData(int scheduleId)
        {
            var schedule = GetItem(scheduleId);
            AddPlayers(schedule.HostId, scheduleId);
            AddPlayers(schedule.GuestId, scheduleId);
        }

        public void UpdateGameData(GameData data)
        {
            var gameDataMgr = new GameDataMgr(Context);
            gameDataMgr.Update(data);
        }

        protected override IEnumerable<Schedule> GetEntries()
        {
            return Context.Schedules.Where(w => !w.Deleted);
        }

        protected override Schedule GetEntry(int id)
        {
            return Context.Schedules.FirstOrDefault(f => f.Id == id);
        }

        protected override void AddItem(Schedule item)
        {
            Context.Schedules.Add(item);
        }

        protected override void DeleteItem(Schedule item)
        {
            var schedule = GetItem(item.Id);
            schedule.Deleted = true;
            Context.Entry(schedule).Property(p => p.Deleted).IsModified = true;
        }

        private void AddPlayers(int teamId, int scheduleId)
        {
            foreach (var player in Context.Players.Where(w => w.TeamId == teamId))
            {
                var data = new GameData
                {
                    ScheduleId = scheduleId,
                    PlayerId = player.Id
                };
                Context.GameDatas.Add(data);
            }

            Context.SaveChanges();
        }
    }
}