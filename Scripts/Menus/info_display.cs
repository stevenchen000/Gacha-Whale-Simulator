using Godot;
using System;

public partial class info_display : Control
{

	[Export] public Label premiumCurrency;
	[Export] public Label currentPower;
	[Export] public Label upgradeMaterials;
	[Export] public Label money;
	[Export] public Label time;
	[Export] public Label day;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdateDisplay();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdateDisplay();
	}

	public void UpdateDisplay(){
		premiumCurrency.Text = $"Gems: {GameState.premiumCurrency}";
		currentPower.Text = $"Power: {GameState.currentPower}";
		upgradeMaterials.Text = $"Materials: {GameState.upgradeCurrency}";
		money.Text = $"${GameState.money}";
		time.Text = $"{GameState.hour}:{GameState.minute}";
		day.Text = $"Day {GameState.day}";
	}
}