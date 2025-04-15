using CombatSystem;
using Godot;
using System;

public partial class CharacterUpgradeMenu : Node
{
    private CharacterData character;
    private UpgradeMenu menu;
    [Export] private CharacterDetailedDisplay charDisplay;

    [Export] private LimitBreakMenu limitBreakMenu;

    public void Init(CharacterData character)
    {
        this.character = character;
        UpdateDisplay();
        UpdateLimitBreakMenu();
        menu = Utils.FindParentOfType<UpgradeMenu>(this);
    }

    private void UpdateDisplay()
    {
        charDisplay.Init(character);
    }

    private void UpdateLimitBreakMenu()
    {
        limitBreakMenu.Init(character);
    }

    public void OnClick()
    {
        
    }

    public void CloseMenu()
    {
        QueueFree();
    }
}
