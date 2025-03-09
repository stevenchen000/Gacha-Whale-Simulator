using Godot;
using System;

public partial class StaminaText : DelayedNode
{
    [Export] private Label staminaText;


    protected override void _Init()
    {
        base._Init();
        UpdateText();
    }


    public void UpdateText()
    {
        int maxStamina = GameState.GetMaxStamina();
        int currStamina = GameState.GetCurrentStamina();

        staminaText.Text = $"{currStamina}/{maxStamina}";
    }
}
