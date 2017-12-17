using System.ComponentModel.DataAnnotations;
using Caliburn.Micro;
using Soccer.DataAccess;
using Supeng.Data.Common;

namespace Soccer.ViewModels.Data
{
    public class TeamEditable : PropertyChangedBase, IEditableData
    {
        private string _description;
        private string _displayName;
        private string _name;

        public TeamEditable()
        {
        }

        public TeamEditable(Team team)
        {
            FromTeam(team);
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

        public int Id { get; set; }

        public Team ToTeam()
        {
            var team = new Team
            {
                Id = Id,
                Name = Name,
                DisplayName = DisplayName,
                Description = Description
            };
            return team;
        }

        public void SetTeam(Team team)
        {
            team.Id = Id;
            team.Name = Name;
            team.DisplayName = DisplayName;
            team.Description = Description;
        }

        public void FromTeam(Team team)
        {
            Id = team.Id;
            Name = team.Name;
            DisplayName = team.DisplayName;
            Description = team.Description;
        }
    }
}