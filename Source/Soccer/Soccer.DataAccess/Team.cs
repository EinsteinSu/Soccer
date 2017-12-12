using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soccer.DataAccess
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Deleted { get; set; }
    }
}
