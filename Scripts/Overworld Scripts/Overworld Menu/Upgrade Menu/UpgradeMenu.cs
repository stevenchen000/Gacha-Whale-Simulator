using CombatSystem;
using Godot;
using System;

public partial class UpgradeMenu : GameMenu
{
    [Export] private CharacterSelector grid;
    [Export] private PackedScene levelUpgradeScene;


    public override void _Ready()
    {
        base._Ready();

    }

    public override void _Init()
    {
        grid.SubscribeEvent(SetData);
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        grid = null;
    }

    public void CloseLimitBreakMenu()
    {
        
    }

    private void SetData(CharacterData data)
    {
        Utils.Print(this, "Called func");
        if(data != null)
        {
            OpenLevelUpgradeMenu(data);
            Utils.Print(this, "Opening upgrade menu");
        }
    }


    private void OpenLevelUpgradeMenu(CharacterData character)
    {
        var menu = Utils.InstantiateCopy<CharacterUpgradeMenu>(levelUpgradeScene);
        menu.Init(character);
        AddChild(menu);
    }
}
