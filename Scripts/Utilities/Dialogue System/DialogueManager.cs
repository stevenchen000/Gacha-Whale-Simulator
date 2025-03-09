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
		[Export] private TextureRect characterPortrait;
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
				instance.OnDialogueStart?.RaiseEvent(instance);
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

		public void SetName(string name)
        {
			nameLabel.Text = name;
        }

		public void SetName(Actor actor)
        {
			string name = actor.actorName;
			SetName(name);
        }

		public void EndDialogue()
        {
			dialogue = null;
        }

		public void ChangePortrait(Texture2D texture, Vector2 size, Vector2 offset)
        {
			characterPortrait.Texture = texture;
			characterPortrait.Size = size;
			characterPortrait.Position = offset;
        }



		/**************
		 * Helper
		 * ***************/

		
	}
}
