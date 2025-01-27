using Godot;
using System;
using EventSystem;

namespace CharacterCreator
{
	public partial class CharacterDisplayUi : CanvasLayer
	{
		[Export] private TextureRect spriteRect;
		[Export] private Label nameText;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			spriteRect.Texture = null;
			nameText.Text = "";
		}


		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}





		public void UpdateDisplay(CharacterPortrait portrait)
		{
			spriteRect.Texture = portrait.GetPortrait();
		}

		public void UpdateDisplay(GameCharacter character)
		{
			var portrait = character.GetPortrait();
			UpdateDisplay(portrait);
			nameText.Text = character.Name;
		}

		public void ResetDisplay()
		{
			spriteRect.Texture = null;
			nameText.Visible = false;
		}
	}
}