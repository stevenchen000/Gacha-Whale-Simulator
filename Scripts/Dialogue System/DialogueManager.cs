using Godot;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace DialogueSystem
{
	public partial class DialogueManager : Node
	{
		private static DialogueManager instance;

		[Export] private Node dialogueBox;
		[Export] private Label nameLabel;
		[Export] private Label textLabel;
        [Export] private int charactersPerSecond = 30;

        [Export] private string textToDisplay = "";
		private DialogueState state;

		private float numOfCharsDisplayed = 0;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			if(instance == null)
			{
				instance = this;
			}
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			DisplayText(delta);
		}

		public static void SetText(string text)
        {
            instance.textToDisplay = text;
        }


		private void RunState(double delta)
		{
            switch (state)
            {
                case DialogueState.INACTIVE:
                    break;
                case DialogueState.ACTIVATING:
                    break;
                case DialogueState.REVEALING_TEXT:
                    break;
                case DialogueState.TEXT_DISPLAYED:
                    break;
            }
        }

		private void ChangeState(DialogueState newState)
		{
            switch (state)
            {
                case DialogueState.INACTIVE:
                    break;
                case DialogueState.ACTIVATING:
                    break;
                case DialogueState.REVEALING_TEXT:
                    break;
                case DialogueState.TEXT_DISPLAYED:
                    break;
            }

            switch (newState)
            {
                case DialogueState.INACTIVE:
                    break;
                case DialogueState.ACTIVATING:
                    break;
                case DialogueState.REVEALING_TEXT:
                    break;
                case DialogueState.TEXT_DISPLAYED:
                    break;
            }
        }

        



        private void SetName(string name)
		{
			if(nameLabel != null)
				nameLabel.Text = name;
		}

		private void DisplayText(double delta)
		{
            numOfCharsDisplayed += (float)delta * charactersPerSecond;
            textLabel.Text = textToDisplay.Substr(0, (int)numOfCharsDisplayed);
        }
    }
}