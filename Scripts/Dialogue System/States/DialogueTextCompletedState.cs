﻿using System;
using Godot;
using StateSystem;
using EventSystem;

namespace DialogueSystem
{
    public partial class DialogueTextCompletedState : DialogueStateNode
    {
        [Export] private DialogueTextUpdatingState updatingTextState;
        [Export] private DialogueInactiveState inactiveState;
        [Export] private DialogueEvent OnDialogueEnd;

        protected override void OnStateActivated()
        {
            
        }

        protected override void RunState(double delta)
        {
            if (Input.IsActionPressed("ui_accept") && timeInState > 0.1)
            {
                var dialogueTree = dialogue.dialogue;
                var currDialogue = dialogue.currDialogue;
                dialogue.currDialogue = dialogue.dialogue.GetNextScene(currDialogue);
                currDialogue = dialogue.currDialogue;
                GD.Print(dialogueTree.ResourceName);
                
                if(currDialogue == null)
                {
                    ChangeState(inactiveState);
                    dialogue.EndDialogue();

                    GD.Print(dialogueTree.ResourceName);
                    OnDialogueEnd?.RaiseEvent(dialogueTree);
                }
                else
                {
                    ChangeState(updatingTextState);
                }
            }
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}