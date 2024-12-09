using System;
using Godot;
using StateSystem;

namespace DialogueSystem
{
    public partial class DialogueActivatingState : DialogueStateNode
    {
        [Export] private NinePatchRect textbox;
        [Export] private Vector2 initialSize;
        [Export] private Vector2 maxSize;
        [Export] private double growSpeed = 10;
        [Export] private double transitionTime = 2;
        [Export] private DialogueTextUpdatingState displayingTextState;

        protected override void OnStateActivated()
        {
            dialogue.Activate();
            dialogue.currDialogue = dialogue.dialogue.GetDialogueStart();
            textbox.Size = initialSize;
        }

        protected override void RunState(double delta)
        {
            textbox.Size = textbox.Size.Lerp(maxSize, (float)(growSpeed * delta));
            
            if (timeInState >= transitionTime)
            {
                ChangeState(displayingTextState);
            }
        }

        protected override void OnStateDeactivated()
        {
            textbox.Size = maxSize;
        }
    }
}
