using System;
using CombatSystem;
using Godot;


public partial class CharacterDetailedDisplay : Control
{
    private CharacterData character;
    [Export] private CharacterPortraitDisplay display;
    [Export] private Label nameLabel;
    [Export] private Label levelLabel;
    [Export] private CharacterRarityDisplay lbDisplay;


    public void Init(CharacterData character)
    {
        this.character = character;
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if(character == null) return;

        if (display != null)
            UpdatePortrait();
        if (nameLabel != null)
            UpdateName();
        if(levelLabel != null)
            UpdateLevel();
        if (lbDisplay != null)
            UpdateLimitBreak();
    }

    private void UpdatePortrait()
    {
        var portrait = character.GetPortrait();
        display.UpdatePortrait(portrait);
    }

    private void UpdateName()
    {
        string name = character.Character.Name;
        nameLabel.Text = name;
    }

    private void UpdateLevel()
    {
        int currLevel = character.Level;
        int maxLevel = character.LevelCap;
        levelLabel.Text = $"Lv {currLevel}/{maxLevel}";
    }

    private void UpdateLimitBreak()
    {
        var lb = character.Stars;
        lbDisplay.SetStars(lb);
    }
}

