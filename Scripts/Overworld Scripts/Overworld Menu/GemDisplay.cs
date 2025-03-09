using Godot;
using System;

public partial class GemDisplay : Node
{
    [Export] private Label gemText;

    public override void _Process(double delta)
    {
        if(gemText != null)
            gemText.Text = GameState.GetGame().premiumCurrency.ToString();
    }
}
