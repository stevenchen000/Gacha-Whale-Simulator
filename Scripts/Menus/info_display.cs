using Godot;
using System;

public partial class info_display : CanvasLayer
{

	[Export] public Label premiumCurrency;
	[Export] public Label currentPower;
	[Export] public Label upgradeMaterials;
	[Export] public Label money;

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
		premiumCurrency.Text = $"Gems: {GameState.state.premiumCurrency}";
		currentPower.Text = $"Power: {GameState.state.currentPower}";
		upgradeMaterials.Text = $"Materials: {GameState.state.upgradeCurrency}";
		money.Text = $"${GameState.GetMoney()}";
	}
}
