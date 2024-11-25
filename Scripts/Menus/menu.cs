using Godot;
using System;
using GachaSystem;

public partial class menu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}



	public void OnDailyButtonPressed(){
		GameState.AddPremiumCurrency(50);
		GameState.AddPower(100);
		GD.Print(GameState.state.premiumCurrency);
	}

	public void OnBannerButtonPressed(){
		if(GameState.HasEnoughPremiumCurrency(1000)){
			GameState.UsePremiumCurrency(1000);
			GachaCharacter character = GameState.PullRandomGachaCharacter();
			GD.Print(character.characterName);
		}
	}

	public void OnEventButtonPressed(){

	}

	public void OnWorkButtonPressed(){
		GameState.Work();
	}

	public void OnSleepButtonPressed(){
		GameState.GoToSleep();
	}
}
