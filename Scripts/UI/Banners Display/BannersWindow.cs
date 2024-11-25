using Godot;
using System;

public partial class BannersWindow : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SummonCharacterButton()
    {
		var game = GameState.GetGame();
		if (game.HasEnoughPremiumCurrency(1000))
		{
			var pulledCharacter = game.PullRandomCharacter();
			
		}
    }

	public void SummonTenCharactersButton()
    {

    }
}
