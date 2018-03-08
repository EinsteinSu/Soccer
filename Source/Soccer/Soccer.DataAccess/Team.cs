using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Supeng.Data.Common;

namespace Soccer.DataAccess
{
    public class Team
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Deleted { get; set; }

        public int Id { get; set; }

        public virtual ObservableCollection<Player> Players { get; set; }
    }
}