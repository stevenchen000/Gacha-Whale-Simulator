using Godot;
using System;
using GachaSystem;

public partial class CharacterBoxDisplay : Control
{
	//UI Elements
	[Export] private Sprite2D portraitElement;
	[Export] private Label nameElement;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdatePortrait(Texture2D texture)
    {
		nameElement.Text = "";
		portraitElement.Texture = texture;
    }

	public void UpdatePortrait(GachaCharacterData character){
		if(character == null){
			portraitElement.Texture = null;
		}else{
			var baseCharacter = character.baseCharacter;
			//set scale
			nameElement.Text = baseCharacter.characterName;

			int numOfCopies = character.numberOfCopies;
		}
	}


}
