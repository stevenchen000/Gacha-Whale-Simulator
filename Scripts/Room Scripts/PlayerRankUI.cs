using Godot;
using System;

public partial class PlayerRankUI : Node
{
    [Export] private Label rankText;
    [Export] private Label staminaText;
    [Export] private Label coinText;
    [Export] private Label gemsText;
    [Export] private Label moneyText;

    public override void _Ready()
    {
        CallDeferred(MethodName._Init);
    }

    private void _Init()
    {
        UpdateRankText();
        UpdateStaminaText();
        UpdateCoinText();
        UpdateGemText();
        UpdateMoneyText();
    }

    public void UpdateRankText()
    {
        rankText.Text = "1";
    }

    public void UpdateStaminaText()
    {
        int currStamina = GameState.GetCurrentStamina();
        int maxStamina = GameState.GetMaxStamina();
        staminaText.Text = $"{currStamina}/{maxStamina}";
    }

    public void UpdateCoinText()
    {
        coinText.Text = "0";
    }

    public void UpdateGemText()
    {
        int gems = GameState.GetGame().premiumCurrency;
        gemsText.Text = String.Format("{0:N0}", gems);
    }

    public void UpdateMoneyText()
    {
        moneyText.Text = "100";
    }
}
