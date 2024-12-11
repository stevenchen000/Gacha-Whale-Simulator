using Godot;
using System;
using Godot.Collections;

namespace QuestSystem
{
    public partial class Quest
    {
        public string name { get { return baseQuest.name; } }
        public string description { get { return baseQuest.description; } }
        private BaseQuest baseQuest;
        private Array<QuestGoal> goals;

        public Quest(BaseQuest questToCopy)
        {
            baseQuest = questToCopy;
            var newGoals = questToCopy.goals;
            goals = new Array<QuestGoal>();
            foreach(var goal in newGoals)
            {
                goals.Add((QuestGoal)goal.Duplicate(true));
            }
        }

        public void ActivateQuest()
        {
            foreach(var goal in goals)
            {
                goal.SetupListeners();
            }
        }

        public void DeactivateQuest()
        {
            foreach(var goal in goals)
            {
                goal.RemoveListeners();
            }
        }

        public bool IsQuest(BaseQuest quest)
        {
            return baseQuest == quest;
        }

        public bool IsQuestCompleted()
        {
            bool result = true;

            foreach(var goal in goals)
            {
                if(!goal.IsCompleted()){
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}