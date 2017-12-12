using System.Collections.Generic;
using System.Linq;
using Soccer.DataAccess;

namespace Soccer.Business
{
    public interface IPlayerMgr : ICrudMgr<Player>
    {
    }

    public class PlayerMgr : CrudMgrBase<Player>, IPlayerMgr
    {
        public PlayerMgr()
        {

        }

        public PlayerMgr(SoccerContext context) : base(context)
        {
        }

        protected override string EntityName => "Player";

        protected override IEnumerable<Player> GetEntries()
        {
            return Context.Players.Where(w => !w.Deleted);
        }

        protected override Player GetEntry(int id)
        {
            return Context.Players.FirstOrDefault(f => f.Id == id);
        }

        protected override void AddItem(Player item)
        {
            Context.Players.Add(item);
        }

        protected override void DeleteItem(Player item)
        {
            var player = GetItem(item.Id);
            player.Deleted = true;
            Context.Entry(player).Property(p => p.Deleted).IsModified = true;
        }
    }
}