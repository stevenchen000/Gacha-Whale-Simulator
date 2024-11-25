using Godot;
using System;
using GachaSystem;

public partial class CharacterBoxDisplay : TextureRect
{
	//UI Elements
	[Export] private Sprite2D portraitElement;
	[Export] private Label copiesElement;
	[Export] private Label nameElement;
	[Export] private Label powerElement;


	//Portrait variables
	[Export] private GachaCharacterData character;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}



	public void UpdatePortrait(GachaCharacterData character){
		if(character == null){
			portraitElement.Texture = null;
		}else{
			var baseCharacter = character.baseCharacter;
			portraitElement.Texture = baseCharacter.characterPortrait;
			portraitElement.SetRegionRect(baseCharacter.portraitRegion);
			//set scale
			nameElement.Text = baseCharacter.characterName;

			int numOfCopies = character.numberOfCopies;
			if (numOfCopies > 1)
				copiesElement.Text = $"+{numOfCopies-1}";
			else
				copiesElement.Text = "";
		}

		this.character = character;
	}


}
