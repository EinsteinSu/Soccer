using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Data;
using DevExpress.Mvvm;
using Soccer.Business;
using Soccer.Common.DataEdit;
using Soccer.DataAccess;
using Soccer.ViewModels.Data;
using Supeng.Data.Common;
using Supeng.Wpf.Common.Commons;
using Supeng.Wpf.Common.Interfaces;
using Supeng.Wpf.Common.ViewModels;

namespace Soccer.ViewModels
{
    public class TeamAndPlayerViewModel : ViewModelBase
    {
        private readonly IEditWindow _editTeamWindow;
        private readonly IEditWindow _editPlayerWindow;
        private readonly IMessageBox _messageBox;
        private readonly ITeamMgr mgr = new TeamMgr();
        private EditableCollection<TeamEditable> _collection;

        public TeamAndPlayerViewModel(IEditWindow editTeamWindow, IEditWindow editPlayerWindow, IMessageBox messageBox)
        {
            _editTeamWindow = editTeamWindow;
            _editPlayerWindow = editPlayerWindow;
            _messageBox = messageBox;
        }

        public EditableCollection<TeamEditable> Collection
        {
            get => _collection;
            set => SetProperty(ref _collection, value, "Collection");
        }

        public void AddTeam()
        {
            var data = new TeamEditable().CreateNewData();
            if (_editTeamWindow.Show(data))
            {
                data.Insert(mgr);
            }
        }

        public void ModifyTeam()
        {
            if (Collection.CurrentItem != null)
            {
                var data = Collection.CurrentItem.Clone();
                if (_editTeamWindow.Show(data))
                {
                    data.Update(mgr);
                }
            }
        }

        public void DeleteTeam()
        {
            if (Collection.CurrentItem != null && _messageBox.ShowMessage("Are you sure to delete the message?", "Message Deleting"))
            {
                Collection.CurrentItem.Delete(mgr);
                Collection.Remove(Collection.CurrentItem);
            }
        }
    }

    public class TeamAndPlayerVm : ViewModelBase
    {
        SoccerContext context = new SoccerContext();
        private ObservableCollection<Team> _collection;
        private Team _currentTeam;

        public TeamAndPlayerVm()
        {
        }

        public ObservableCollection<Team> Collection
        {
            get { return _collection; }
            set { SetProperty(ref _collection, value, "Collection"); }
        }

        public Team CurrentTeam
        {
            get { return _currentTeam; }
            set { SetProperty(ref _currentTeam, value, "CurrentTeam"); }
        }

        public void Refresh()
        {
            context.Teams.Load();
            Collection = context.Teams.Local;
        }

        public void Save()
        {

        }
    }

    public class EditWindow<T> : IEditWindow where T : new()
    {
        private readonly DataLayoutDialogWindowViewModel<T> _window;

        public EditWindow(DataLayoutDialogWindowViewModel<T> window)
        {
            _window = window;
        }

        public bool Show(object data)
        {
            _window.Data = (T)data;

            var result = _window.ShowDialogWindowWithReturns();
            if (result.HasValue && result.Value)
                return true;
            return false;
        }
    }
}