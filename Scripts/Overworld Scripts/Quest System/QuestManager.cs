using Godot;
using System;
using System.Collections.Generic;

namespace QuestSystem
{
    public partial class QuestManager : Node
    {
        private List<Quest> currentQuests;

        public override void _Ready()
        {
            currentQuests = new List<Quest>();
        }

        public void AddQuest(BaseQuest quest)
        {
            var newQuest = new Quest(quest);
            currentQuests.Add(newQuest);
            newQuest.ActivateQuest();
            Utils.Print(this, $"Quest added: {quest.name}");
        }

        public void RemoveQuest(int index)
        {
            var quest = currentQuests[index];
            currentQuests.RemoveAt(index);
            quest.DeactivateQuest();
        }

        public void RemoveQuest(BaseQuest baseQuest)
        {
            Quest result = GetQuest(baseQuest);
            if(result != null)
            {
                currentQuests.Remove(result);
                result.DeactivateQuest();
                Utils.Print(this, $"Quest removed: {result.name}");
            }
        }

        public bool HasQuest(BaseQuest baseQuest)
        {
            var quest = GetQuest(baseQuest);
            return quest != null;
        }

        public bool QuestFinished(BaseQuest baseQuest)
        {
            var quest = GetQuest(baseQuest);
            bool result = false;
            if(quest != null)
            {
                result = quest.IsQuestCompleted();
            }
            return result;
        }

        private Quest GetQuest(BaseQuest quest)
        {
            Quest result = null;

            foreach(var currQuest in currentQuests)
            {
                if (currQuest.IsQuest(quest))
                {
                    result = currQuest;
                    break;
                }
            }

            return result;
        }
    }
}