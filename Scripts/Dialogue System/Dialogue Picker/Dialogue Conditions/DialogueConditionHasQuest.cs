using Godot;
using Godot.Collections;
using System;
using QuestSystem;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialogueConditionHasQuest : DialogueCondition
    {
        [Export] private BaseQuest quest;
        protected override bool _ConditionMet() {
            return GameState.PlayerHasQuest(quest);
        }
    }
}