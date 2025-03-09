using Godot;
using Godot.Collections;
using System;
using QuestSystem;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialogueConditionQuestFinished : DialogueCondition
    {
        [Export] private BaseQuest quest;
        protected override bool _ConditionMet() {
            return GameState.PlayerCompletedQuest(quest);
        }
    }
}