using System.Collections.Generic;
using System.Linq;
using Soccer.DataAccess;

namespace Soccer.Business
{
    public interface ITeamMgr : ICrudMgr<Team>
    {
        IEnumerable<Player> GetPlayersByTeamId(int teamId);

        void AddPlayer(int teamId, Player player);

        void UpdatePlayer(Player player);

        void DeletePlayer(int playerId);
    }

    public class TeamMgr : CrudMgrBase<Team>, ITeamMgr
    {
        private readonly PlayerMgr _playerMgr;

        public TeamMgr()
        {
            _playerMgr = new PlayerMgr(Context);
        }

        protected override string EntityName => "Team";

        public IEnumerable<Player> GetPlayersByTeamId(int teamId)
        {
            return Context.Players.Where(w => w.TeamId == teamId && !w.Deleted);
        }

        public void AddPlayer(int teamId, Player player)
        {
            player.TeamId = teamId;
            _playerMgr.Add(player);
        }

        public void UpdatePlayer(Player player)
        {
            _playerMgr.Update(player);
        }

        public void DeletePlayer(int playerId)
        {
            _playerMgr.Delete(playerId);
        }

        protected override IEnumerable<Team> GetEntries()
        {
            return Context.Teams.Where(w => !w.Deleted).ToList();
        }

        protected override Team GetEntry(int id)
        {
            return Context.Teams.FirstOrDefault(f => f.Id == id);
        }

        protected override void AddItem(Team item)
        {
            Context.Teams.Add(item);
        }

        protected override void DeleteItem(Team item)
        {
            var team = GetItem(item.Id);
            team.Deleted = true;
            Context.Entry(team).Property(p => p.Deleted).IsModified = true;
        }
    }
}