using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soccer.DataAccess
{
    public class Schedule
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string DisplayName { get; set; }

        public DateTime? StartTime { get; set; }

        [ForeignKey("Host")]
        public int HostId { get; set; }

        public Team Host { get; set; }

        [ForeignKey("Guest")]
        public int GuestId { get; set; }

        public Team Guest { get; set; }

        public int HostScore { get; set; }

        public int GuestScore { get; set; }

        public GameStatus Status { get; set; }

        public List<GameData> Data { get;set; }

        public bool Deleted { get; set; }
    }

    public enum GameStatus
    {
        NotStart,
        Gaming,
        Finished
    }
}