using System;
using Godot;
using StateSystem;

namespace DialogueSystem
{
    public partial class DialogueTextUpdatingState : DialogueStateNode
    {
        [Export] private DialogueTextCompletedState textCompletedState;
        [Export] private double charPerSecond = 30;
        private double charactersDisplayed = 0;

        protected override void OnStateActivated()
        {
            ResetText();
            SetupActorName();
            SetupActorPortrait();
        }
        private void ResetText()
        {
            SetText("");
            charactersDisplayed = 0;
        }

        private void SetupActorName()
        {
            var dialogueScene = dialogue.currDialogue;
            var actor = dialogueScene.actor;
            string actorName = actor.actorName;
            SetName(actorName);
        }

        private void SetupActorPortrait()
        {
            var currDialogue = dialogue.currDialogue;
            var portrait = currDialogue.GetPortrait();
            var size = currDialogue.GetPortraitSize();
            var position = currDialogue.GetPortraitPosition();

            dialogue.ChangePortrait(portrait, size, position);
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

            if (Input.IsActionPressed("ui_accept") && timeInState > 0.1)
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
            dialogue.SetText(text);
        }

        private void SetName(string name)
        {
            dialogue.SetName(name);
        }
    }
}
