using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm;
using Soccer.Business;
using Soccer.DataAccess;
using Soccer.ViewModels.Data;
using Supeng.Data.Common;

namespace Soccer.ViewModels
{
    public class TeamAndPlayerViewModel : ViewModelBase
    {
        readonly TeamMgr _mgr = new TeamMgr();
        private EditableCollection<TeamEditable> _collection;

        public EditableCollection<TeamEditable> Collection
        {
            get => _collection;
            set => SetProperty(ref _collection, value, "Collection");
        }

        public void Refresh()
        {
            Collection = new EditableCollection<TeamEditable>();
            foreach (var item in _mgr.GetItems())
            {
                Collection.Add(new TeamEditable(item));
            }
            Collection.AcceptChanges();
        }

        public void Save()
        {
            foreach (var change in _collection.Changes)
            {
                switch (change.State)
                {
                    case DataState.Added:
                        _mgr.Add(change.Data.ToTeam());
                        break;
                    case DataState.Modified:
                        var team = _mgr.GetItem(change.Data.Id);
                        change.Data.SetTeam(team);
                        _mgr.Update(team);
                        break;case DataState.Deleted:
                        _mgr.Delete(change.Data.Id);
                        break;
                }
            }
            Refresh();
        }
    }
}