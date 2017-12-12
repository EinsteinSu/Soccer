using System.Collections.Generic;
using System.Linq;
using Soccer.DataAccess;

namespace Soccer.Business
{
    public interface ITeamMgr : ICrudMgr<Team>
    {
    }

    public class TeamMgr : CrudMgrBase<Team>, ITeamMgr
    {
        protected override string EntityName => "Team";

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
            Context.Teams.Remove(item);
        }
    }
}