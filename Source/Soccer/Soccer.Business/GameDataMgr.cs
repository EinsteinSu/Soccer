using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soccer.DataAccess;

namespace Soccer.Business
{
    public interface IGameDataMgr : ICrudMgr<GameData>
    {

    }

    public class GameDataMgr : CrudMgrBase<GameData>, IGameDataMgr
    {
        public GameDataMgr(SoccerContext context) : base(context) { }

        protected override string EntityName => "Game Data";
        protected override IEnumerable<GameData> GetEntries()
        {
            return Context.GameDatas.ToList();
        }

        protected override GameData GetEntry(int id)
        {
            return Context.GameDatas.FirstOrDefault(f => f.Id == id);
        }

        protected override void AddItem(GameData item)
        {
            Context.GameDatas.Add(item);
        }

        protected override void DeleteItem(GameData item)
        {
            Context.GameDatas.Remove(item);
        }
    }
}
