using CombatSystem;
using Godot;
using System;
using Godot.Collections;

public partial class MainGame : Node
{
    [ExportCategory("Menus")]
    [Export] private PackedScene characterCreatorScene;
    [Export] private PackedScene battleScene;
    [Export] private PackedScene homeScene;
    [Export] private PackedScene partyScene;
    [Export] private PackedScene gachaScene;
    [Export] private PackedScene stageSelectionScene;
    [Export] private PackedScene upgradeMenuScene;
    [Export] private PackedScene roomMenuScene;

    [ExportCategory("Battle Data")]
    [Export] public Array<CharacterData> playerParty { get; private set; }
    [Export] public StageData StageData { get; private set; }


    private GameMenu currMenu;

    public override void _Ready()
    {
        Utils.Print(this, 1);
        CallDeferred(MethodName.InitMenu);
    }

    private void InitMenu()
    {
        OpenMenu(roomMenuScene);
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
        OpenMenu(characterCreatorScene);
    }

    public void OpenGachaMenu()
    {
        OpenMenu(gachaScene);
    }

    public void OpenPartyMenu()
    {
        OpenMenu(partyScene);
    }

    public void OpenStageSelectionMenu()
    {
        OpenMenu(stageSelectionScene);
    }

    public void OpenBattleMenu()
    {
        OpenMenu(battleScene);
    }
    public void OpenHomeMenu()
    {
        OpenMenu(homeScene);
    }

    public void OpenUpgradeMenu()
    {
        OpenMenu(upgradeMenuScene);
    }

    public void OpenRoomMenu()
    {
        OpenMenu(roomMenuScene);
    }

    #endregion

    #region Menu Helpers

    public void OpenMenu(PackedScene menu)
    {
        CloseMenu(currMenu);
        var gameMenu = (GameMenu)(Utils.InstantiateCopy(menu));
        AddChild(gameMenu);
        gameMenu._Init();
        currMenu = gameMenu;
    }

    private void CloseMenu(GameMenu menu)
    {
        if(menu != null)
        {
            menu._OnDisable();
            menu.QueueFree();
        }
    }

    #endregion

    /****************
     * Data Management
     * ****************/

    public void SetStageData(StageData newStage)
    {
        StageData = newStage;
    }

    public void RemoveStageData()
    {
        StageData = null;
    }

    

}
