using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soccer.DataAccess;

namespace Soccer.Business.Tests
{
    public class TestBase
    {
        protected SoccerContext Context = new SoccerContext();

        [TestInitialize]
        public virtual void Initialize()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Databases"));
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SoccerContext>());

        }

        [TestCleanup]
        public void Cleanup()
        {
            Context.Database.ExecuteSqlCommand("Delete from GameDatas");
            Context.Database.ExecuteSqlCommand("Delete from Schedules");
            Context.Database.ExecuteSqlCommand("Delete from Players");
            Context.Database.ExecuteSqlCommand("Delete from Teams");
        }

        protected int AddTeam(TeamMgr mgr)
        {
            var team = new Team
            {
                Name = "China",
                DisplayName = "CHN",
                Description = "Balabala"
            };
            mgr.Add(team);
            Assert.IsTrue(team.Id > 0);
            return team.Id;
        }

        protected int AddSchedule(ScheduleMgr mgr)
        {
            var teamMgr = new TeamMgr();
            var host = AddTeam(teamMgr);
            var guest = AddTeam(teamMgr);
            var schedule = new Schedule
            {
                Name = "World cup match 1",
                DisplayName = "World cup",
                HostId = host,
                GuestId = guest
            };
            mgr.Add(schedule);
            Assert.IsTrue(schedule.Id > 0);
            return schedule.Id;
        }

        protected void AddTeamPlayers(int teamId, int playerCount)
        {
            var playerMgr = new PlayerMgr(Context);
            for (int i = 0; i < playerCount; i++)
            {
                var player = new Player
                {
                    Name = $"Player{i + 1}",
                    DisplayName = $"Player{i + 1}",
                    Number = i + 1,
                    TeamId = teamId
                };
                playerMgr.Add(player);
            }
        }

        protected int AddPlayer(int teamId, string name, int number)
        {
            var player = new Player();
            player.Name = player.DisplayName = name;
            player.Number = number;
            player.TeamId = teamId;
            var mgr = new PlayerMgr();
            mgr.Add(player);
            Assert.IsTrue(player.Id > 0);
            return player.Id;
        }
    }
}