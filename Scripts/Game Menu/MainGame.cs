using CombatSystem;
using Godot;
using System;
using Godot.Collections;

public partial class MainGame : Node
{
    [Export] private PackedScene characterCreatorScene;
    [Export] private PackedScene battleScene;
    [Export] public Array<GameCharacter> playerParty;
    [Export] public Array<GameCharacter> enemyParty;

    private GameMenu currMenu;

    public override void _Ready()
    {
        CallDeferred(MethodName.InitMenu);
    }

    private void InitMenu()
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
