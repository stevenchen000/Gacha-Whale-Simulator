using System;
using Godot;
using StateSystem;
using EventSystem;

namespace DialogueSystem
{
    public partial class DialogueInactiveState : DialogueStateNode
    {
        [Export] private DialogueActivatingState activatingState;
        [Export] private DialogueEvent OnDialogueStart;

        protected override void OnStateActivated()
        {
            dialogue.Deactivate();
            dialogue.SetText("");
        }

        protected override void RunState(double delta)
        {
            if (dialogue.dialogue != null)
            {
                var currDialogue = dialogue.dialogue;
                ChangeState(activatingState);
                OnDialogueStart?.RaiseEvent(this,currDialogue);
            }
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
