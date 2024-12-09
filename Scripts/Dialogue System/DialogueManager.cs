using Godot;
using System;
using EventSystem;
using StateSystem;

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

		[Export] private VoidEvent OnDialogueStart;
		[Export] private VoidEvent OnDialogueEnd;

		[Export] public DialogueTree dialogue;
		public DialogueScene currDialogue { get; set; }

		private DialogueState state = DialogueState.INACTIVE;

		private float numOfCharsDisplayed = 0;

		

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			if(instance == null)
			{
				base._Ready();
				instance = this;
			}
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			//RunState(delta);
			base._Process(delta);
		}

		public static void PlayDialogue(DialogueTree dialogue)
		{
			if (instance.dialogue == null && dialogue != null)
			{
				instance.dialogue = dialogue;
				//instance.ChangeState(DialogueState.ACTIVATING);
				instance.OnDialogueStart?.RaiseEvent();
			}
		}

		public void Deactivate()
        {
			dialogueBox.Visible = false;
        }

		public void Activate()
        {
			dialogueBox.Visible = true;
        }

		public void SetText(string text)
        {
			textLabel.Text = text;
        }

		public void EndDialogue()
        {
			dialogue = null;
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
					ChangeState(DialogueState.REVEALING_TEXT);
					break;
				case DialogueState.REVEALING_TEXT:
					RevealTextState(delta);
					break;
				case DialogueState.TEXT_DISPLAYED:
					TextDisplayedState(delta);
					break;
			}
		}

		private void ChangeState(DialogueState newState)
		{
			GD.Print(newState);
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
					dialogue = null;
					OnDialogueEnd?.RaiseEvent();
					break;
				case DialogueState.ACTIVATING:
					dialogueBox.Visible = true;
					currDialogue = dialogue.GetDialogueStart();
					break;
				case DialogueState.REVEALING_TEXT:
					break;
				case DialogueState.TEXT_DISPLAYED:
					break;
			}

			state = newState;
		}

		private void RevealTextState(double delta)
		{
			string textToDisplay = currDialogue.dialogue;

			if (Input.IsActionJustPressed("ui_accept"))
			{
				textLabel.Text = textToDisplay;
				ChangeState(DialogueState.TEXT_DISPLAYED);
			}
			else
			{
				numOfCharsDisplayed += (float)delta * charactersPerSecond;
				string substring = textToDisplay.Substr(0, (int)numOfCharsDisplayed);
				textLabel.Text = substring;

				if (substring == textToDisplay)
				{
					ChangeState(DialogueState.TEXT_DISPLAYED);
				}
			}
		}

		private void TextDisplayedState(double delta)
		{
			if (Input.IsActionJustPressed("ui_accept"))
			{
				numOfCharsDisplayed = 0;
				currDialogue = dialogue.GetNextScene(currDialogue);
				if (currDialogue != null)
				{
					ChangeState(DialogueState.REVEALING_TEXT);
                }
                else
                {
					ChangeState(DialogueState.INACTIVE);
                }
			}
		}


		/**************
		 * Helper
		 * ***************/

		private void SetName()
		{
			if (nameLabel != null)
				nameLabel.Text = currDialogue.GetActorName();
		}


		private void UpdateCurrentDialogue()
        {
			currDialogue = dialogue.GetNextScene(currDialogue);
			SetName();
			//UpdateImage();
        }
	}
}
