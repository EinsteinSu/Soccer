using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Soccer.Common.DataEdit;
using Soccer.DataAccess;
using Supeng.Data.Common;
using Supeng.Wpf.Common.ViewModels;

namespace Soccer.ViewModels.Data
{
    public class TeamEditable : EditableModelBase<Team>, IInitialize<TeamEditable>, ICloneable<TeamEditable>
    {
        private string _description;
        private string _displayName;
        private string _name;
        private EditableCollection<PlayerEditable> _players;

        public TeamEditable()
        {
            _players = new EditableCollection<PlayerEditable>();
        }

        public TeamEditable Clone()
        {
            var data = new TeamEditable
            {
                Id = Id,
                Name = Name,
                DisplayName = DisplayName,
                Description = Description
            };
            return data;
        }

        public TeamEditable CreateNewData()
        {
            return new TeamEditable { Name = "Team A", DisplayName = "Team A" };
        }

        public override void ConvertFromModel(Team data)
        {
            Id = data.Id;
            Name = data.Name;
            DisplayName = data.DisplayName;
            Description = data.Description;
        }

        public override void SetModel(Team data)
        {
            data.Id = Id;
            data.Name = Name;
            data.DisplayName = DisplayName;
            data.Description = Description;
        }

        #region properties

        [Required]
        [MaxLength(50)]
        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        [Required]
        [MaxLength(10)]
        public string DisplayName
        {
            get => _displayName;
            set
            {
                if (value == _displayName) return;
                _displayName = value;
                NotifyOfPropertyChange(() => DisplayName);
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (value == _description) return;
                _description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }

        public EditableCollection<PlayerEditable> Players
        {
            get => _players;
            set
            {
                if (Equals(value, _players)) return;
                _players = value;
                NotifyOfPropertyChange(() => Players);
            }
        }
        #endregion


    }

    public class TeamEditWindowViewModel : DataLayoutDialogWindowViewModel<TeamEditable>
    {
        protected override string LayoutFileName => "TeamEditWindow.config";

        protected override string Check()
        {
            return string.Empty;
        }
    }
}