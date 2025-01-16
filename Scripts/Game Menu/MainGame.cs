using CombatSystem;
using Godot;
using System;

public partial class MainGame : Node
{
    [Export] private PackedScene characterCreatorScene;
    [Export] private PackedScene battleScene;

    private GameMenu currMenu;

    public override void _Ready()
    {
        currMenu = (GameMenu)Utils.InstantiateCopy(battleScene);
        AddChild(currMenu);
        currMenu._Init();
    }

    public override void _Process(double delta)
    {
        
    }

    /***************
     * Menus
     * ************/

    #region Menus

    public void OpenCharacterCreator()
    {
        CloseMenu(currMenu);
        var creatorMenu = (GameMenu)Utils.InstantiateCopy(characterCreatorScene);
        AddChild(creatorMenu);
        creatorMenu._Init();
    }

    private void CloseMenu(GameMenu menu)
    {
        if(menu != null)
        {
            menu._OnDisable();
            RemoveChild(menu);
        }
    }

    #endregion


}
