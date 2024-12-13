using Godot;
using Godot.Collections;
using System;
using QuestSystem;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialoguePickerQuest : DialoguePicker
    {
        [Export] private BaseQuest quest;
        [Export] private string questFlag;
        [Export] private DialogueTree questDialogue;
        [Export] private DialogueTree questNotFinishedDialogue;
        [Export] private DialogueTree questFinishedDialogue;

        public override DialogueTree GetDialogue()
        {
            DialogueTree result = null;
            bool playerHasQuest = GameState.PlayerHasQuest(quest);
            bool questTurnedIn = GameState.GetFlag(questFlag);
            bool questFinished = GameState.PlayerCompletedQuest(quest);

            if (!playerHasQuest && !questTurnedIn)
            {
                result = questDialogue;
            }
            else if(playerHasQuest && !questFinished)
            {
                result = questNotFinishedDialogue;
            }else if(playerHasQuest && questFinished)
            {
                result = questFinishedDialogue;
            }

            return result;
        }

    }
}