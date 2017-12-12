using System.Data.Entity;

namespace Soccer.DataAccess
{
    public class SoccerContext : DbContext
    {
        public SoccerContext() : base("Soccer")
        {
        }

        public IDbSet<Team> Teams { get; set; }

        public IDbSet<Player> Players { get; set; }

        public IDbSet<Schedule> Schedules { get; set; }

        public IDbSet<GameData> GameDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Schedule>().HasRequired(t => t.Guest).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Schedule>().HasRequired(t => t.Host).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<GameData>().HasRequired(p => p.Player).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<GameData>().HasRequired(p => p.ReplacingPlayer).WithMany().WillCascadeOnDelete(false);
        }
    }
}