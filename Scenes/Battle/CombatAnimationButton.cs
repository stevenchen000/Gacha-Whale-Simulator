using Godot;
using System;

public partial class CombatAnimationButton : Node
{
    [Export] private TextureRect buttonTexture;
    [Export] private Label onOffText;
    [Export] private Color enabledColor;
    [Export] private Color disabledColor;

    public override void _Ready()
    {
        UpdateButton();
    }
    public void OnClick()
    {
        string settingName = SettingNames.DisableCombatAnimationsFlag;
        GameState.ToggleFlag(settingName);
        UpdateButton();
    }

    private void UpdateButton()
    {
        string settingName = SettingNames.DisableCombatAnimationsFlag;
        if (GameState.GetFlag(settingName))
        {
            DisableButton();
        }
        else
        {
            EnableButton();
        }
    }

    private void EnableButton()
    {
        onOffText.Text = "ON";
        buttonTexture.SelfModulate = enabledColor;
    }

    private void DisableButton()
    {
        onOffText.Text = "OFF";
        buttonTexture.SelfModulate = disabledColor;
    }
}
