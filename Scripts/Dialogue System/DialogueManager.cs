using Godot;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace DialogueSystem
{
	public partial class DialogueManager : CanvasLayer
	{
		private static DialogueManager instance;

		[Export] private Control dialogueBox;
		[Export] private Label nameLabel;
		[Export] private Label textLabel;
		[Export] private TextureRect leftCharacterPortrait;
		[Export] private TextureRect rightCharacterPortrait;
		[Export] private Color enabledColor;
		[Export] private Color disabledColor;
		[Export] private int charactersPerSecond = 30;

		[Export] private DialogueTree dialogue;

		[Export] private string textToDisplay = "";
		private DialogueState state = DialogueState.INACTIVE;

		private float numOfCharsDisplayed = 0;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			if(instance == null)
			{
				instance = this;
				if(dialogue == null)
				{
					ChangeState(DialogueState.INACTIVE);
				}
				else
				{
					ChangeState(DialogueState.ACTIVATING);
				}
			}
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			
		}

		public static void SetText(string text)
		{
			instance.textToDisplay = text;
		}


		/****************
		 * States
		 * *****************/

		private void RunState(double delta)
		{
			switch (state)
			{
				case DialogueState.INACTIVE:
					break;
				case DialogueState.ACTIVATING:
					break;
				case DialogueState.REVEALING_TEXT:
					RevealTextState(delta);
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
					dialogueBox.Visible = false;
					break;
				case DialogueState.ACTIVATING:
					dialogueBox.Visible = true;
					break;
				case DialogueState.REVEALING_TEXT:
					break;
				case DialogueState.TEXT_DISPLAYED:
					numOfCharsDisplayed = 0;
					break;
			}
		}

		private void RevealTextState(double delta)
		{
			if (Input.IsActionJustPressed("ui_accept"))
			{
				textLabel.Text = textToDisplay;
				ChangeState(DialogueState.TEXT_DISPLAYED);
			}

			numOfCharsDisplayed += (float)delta * charactersPerSecond;
			string substring = textToDisplay.Substr(0, (int)numOfCharsDisplayed);
			textLabel.Text = substring;

			if (substring == textToDisplay)
			{
				ChangeState(DialogueState.TEXT_DISPLAYED);
			}
		}

		private void TextDisplayedState(double delta)
		{
			if (Input.IsActionJustPressed("ui_accept"))
			{
				ChangeState(DialogueState.REVEALING_TEXT);
			}
		}


		/**************
		 * Helper
		 * ***************/

		private void SetName(string name)
		{
			if(nameLabel != null)
				nameLabel.Text = name;
		}
	}
}
