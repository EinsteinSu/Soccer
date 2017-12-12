using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soccer.DataAccess
{
    public class GameData
    {
        public int Id { get; set; }

        [ForeignKey("Schedule")]
        public int ScheduleId { get; set; }

        public Schedule Schedule { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }

        public Player Player { get; set; }

        public DateTime? YellowCard1 { get; set; }

        public DateTime? YellowCard2 { get; set; }

        public DateTime? RedCardDirectly { get; set; }

        #region replacing
        public bool IsFirst { get; set; }

        public DateTime? OutTime { get; set; }

        public DateTime? InTime { get; set; }

        public Player ReplacingPlayer { get; set; }

        [ForeignKey("ReplacingPlayer")]
        public int? ReplacingPlayerId { get; set; }
        #endregion

        public virtual List<Goal> Goals { get; set; }
    }

    public class Goal
    {
        public int Id { get; set; }

        public DateTime? Time { get; set; }

        [ForeignKey("GameData")]
        public int GameDataId { get; set; }

        public GameData GameData { get; set; }
    }
}