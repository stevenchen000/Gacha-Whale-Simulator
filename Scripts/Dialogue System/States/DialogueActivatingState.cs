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
            InitDialogue();
            SetupActorName();
            SetupPortrait();
            
            textbox.Size = initialSize;
        }

        private void InitDialogue()
        {
            dialogue.Activate();
            dialogue.currDialogue = dialogue.dialogue.GetDialogueStart();
        }

        private void SetupPortrait()
        {
            var currDialogue = dialogue.currDialogue;
            var portrait = currDialogue.GetPortrait();
            var size = currDialogue.GetPortraitSize();
            var position = currDialogue.GetPortraitPosition();
            dialogue.ChangePortrait(portrait, size, position);
        }

        private void SetupActorName()
        {
            var actor = dialogue.currDialogue.actor;
            dialogue.SetName(actor);
        }



        protected override void RunState(double delta)
        {
            if (Input.IsActionJustPressed("ui_accept") && timeInState > 0.1)
            {
                ChangeState(displayingTextState);
            }
            else
            {
                textbox.Size = textbox.Size.Lerp(maxSize, (float)(growSpeed * delta));

                if (timeInState >= transitionTime)
                {
                    ChangeState(displayingTextState);
                }
            }
        }

        protected override void OnStateDeactivated()
        {
            textbox.Size = maxSize;
        }
    }
}
