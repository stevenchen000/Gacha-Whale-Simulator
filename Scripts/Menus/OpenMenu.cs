using Godot;
using System;

public partial class OpenMenu : Node
{
    private MainGame mainMenu;


    public override void _Ready()
    {
        mainMenu = Utils.FindParentOfType<MainGame>(this);
    }

    public void OpenBattleSelection()
    {
        mainMenu.OpenStageSelectionMenu();
    }
}
