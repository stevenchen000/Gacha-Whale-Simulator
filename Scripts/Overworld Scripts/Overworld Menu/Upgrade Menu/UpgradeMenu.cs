using CombatSystem;
using Godot;
using System;

public partial class UpgradeMenu : GameMenu
{
    [Export] private CharacterSelector grid;
    [Export] private PackedScene levelUpgradeScene;

    private CharacterData selectedCharacter;


    public override void _Ready()
    {
        base._Ready();

    }

    private void Init()
    {
        grid.SubscribeEvent(SetData);
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        grid = null;
    }

    private void SetData(CharacterData data)
    {
        if (data != selectedCharacter)
        {
            selectedCharacter = data;
        }
        else
        {
            OpenLevelUpgradeMenu();
        }
    }


    private void OpenLevelUpgradeMenu()
    {
        
    }
}
