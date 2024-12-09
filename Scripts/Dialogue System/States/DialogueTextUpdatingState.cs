using System;
using Godot;
using StateSystem;

namespace DialogueSystem
{
    public partial class DialogueTextUpdatingState : DialogueStateNode
    {
        [Export] private Label textbox;
        [Export] private DialogueTextCompletedState textCompletedState;
        [Export] private double charPerSecond = 30;
        private double charactersDisplayed = 0;

        protected override void OnStateActivated()
        {
            SetText("");
            charactersDisplayed = 0;
        }

        protected override void RunState(double delta)
        {
            charactersDisplayed = charPerSecond * timeInState;
            var currDialogue = dialogue.currDialogue;
            string fullText = currDialogue.dialogue;
            string substring = fullText.Substring(0, (int)charactersDisplayed);
            SetText(substring);

            if(substring == fullText)
            {
                ChangeState(textCompletedState);
            }

            if (Input.IsActionJustPressed("ui_accept"))
            {
                ChangeState(textCompletedState);
                SetText(fullText);
            }

        }

        protected override void OnStateDeactivated()
        {
            
        }

        private void SetText(string text)
        {
            textbox.Text = text;
        }
    }
}
