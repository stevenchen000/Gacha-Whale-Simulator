using System;
using Godot;
using Godot.Collections;

namespace QuestSystem
{
    public class QuestData
    {
        private Array<int> questGoals;
        private Array<bool> goalMet;

        public QuestData(int numOfGoals)
        {
            questGoals = new Array<int>();
            questGoals.AddRange(new int[numOfGoals]);
            goalMet = new Array<bool>();
            goalMet.AddRange(new bool[numOfGoals]);
        }

        public int GetGoalProgress(int index)
        {
            return questGoals[index];
        }

        public void SetGoalProgress(int index, int amount)
        {
            questGoals[index] = amount;
        }

        public void IncrementGoalProgress(int index, int amount = 1)
        {
            questGoals[index] += amount;
        }

        public void DecreaseGoalProgress(int index, int amount)
        {
            questGoals[index] -= amount;
        }

        public void SetIsGoalFinished(int index, bool isFinished)
        {
            goalMet[index] = isFinished;
        }

        public bool IsGoalFinished(int index)
        {
            return goalMet[index];
        }

        public bool AreAllGoalsFinished()
        {
            bool result = true;

            foreach(bool value in goalMet)
            {
                if (!value)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}
