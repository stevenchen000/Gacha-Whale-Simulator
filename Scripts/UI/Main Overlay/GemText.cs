using Godot;
using System;

public partial class GemText : DelayedNode
{
    [Export] private Label gemText;

    protected override void _Init()
    {
        base._Init();
        UpdateText();
    }

    public void UpdateText()
    {
        int gems = GameState.GetGame().premiumCurrency;
        gemText.Text = $"{gems}";
    }
}
