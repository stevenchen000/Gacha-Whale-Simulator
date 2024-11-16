using Godot;
using System;

public partial class GachaMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	

	public void _on_characters_button_up(){

	}

	public void _on_upgrades_button_up(){
		if(GameState.UseUpgradeCurrency(1000)){
			GameState.AddPower(100);
		}
	}

	public void _on_banners_button_up(){
		if(GameState.HasEnoughPremiumCurrency(1000)){
			GameState.UsePremiumCurrency(1000);
			GameState.AddPower(1000);
		}
	}

	public void _on_dungeons_button_up(){
		GameState.AddUpgradeCurrency(100);
		GameState.AddPremiumCurrency(100);
	}
}
