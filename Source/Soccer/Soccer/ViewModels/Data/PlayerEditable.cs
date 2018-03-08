using System.ComponentModel.DataAnnotations;
using Soccer.Common.DataEdit;
using Soccer.DataAccess;

namespace Soccer.ViewModels.Data
{
    public class PlayerEditable : EditableModelBase<Player>
    {
        private string _displayName;
        private string _name;
        private int _number;
        private int _teamId;

        public override void ConvertFromModel(Player player)
        {
            Id = player.Id;
            Name = player.Name;
            Number = player.Number;
            DisplayName = player.DisplayName;
            TeamId = player.TeamId;
        }

        public override void SetModel(Player player)
        {
            player.Id = Id;
            player.Name = Name;
            player.Number = Number;
            player.DisplayName = DisplayName;
            player.TeamId = TeamId;
        }

        #region properties

        [Required]
        public int Number
        {
            get => _number;
            set
            {
                if (value == _number) return;
                _number = value;
                NotifyOfPropertyChange(() => Number);
            }
        }

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

        public int TeamId
        {
            get => _teamId;
            set
            {
                if (value == _teamId) return;
                _teamId = value;
                NotifyOfPropertyChange(() => TeamId);
            }
        }
        #endregion
    }
}