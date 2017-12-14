using System;
using System.Collections.Generic;
using System.Linq;
using Soccer.DataAccess;

namespace Soccer.Business
{
    public interface IGoalMgr
    {
        IEnumerable<Goal> GetGoalsByPlayerId(int playerId);

        IEnumerable<Goal> GetGoalsInTheGame(int gameDataId);

        void AddGoal(int gameDataId, DateTime? time = null);

        void RemoveGoal(int goalId);
    }

    public class GoalMgr : IGoalMgr
    {
        protected readonly SoccerContext Context;

        public GoalMgr(SoccerContext context)
        {
            Context = context;
        }

        public IEnumerable<Goal> GetGoalsByPlayerId(int playerId)
        {
            return Context.Goals.Where(w => w.GameData.PlayerId == playerId);
        }

        public IEnumerable<Goal> GetGoalsInTheGame(int gameDataId)
        {
            return Context.Goals.Where(w => w.GameDataId == gameDataId);
        }

        public void AddGoal(int gameDataId, DateTime? time = null)
        {
            var goal = new Goal {Time = time ?? DateTime.Now};
            try
            {
                Context.Goals.Add(goal);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"Add goal failed, {e.Message}");
            }
        }

        public void RemoveGoal(int goalId)
        {
            var goal = Context.Goals.FirstOrDefault(f => f.Id == goalId);
            if (goal == null) throw new Exception($"Could not found any goal with id {goalId}");

            try
            {
                Context.Goals.Remove(goal);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"Could not delete goal {goalId}, {e.Message}");
            }
        }
    }
}