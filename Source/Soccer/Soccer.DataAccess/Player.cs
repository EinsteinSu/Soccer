using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soccer.DataAccess
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string DisplayName { get; set; }

        public Team Team { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public bool Deleted { get; set; }
    }
}