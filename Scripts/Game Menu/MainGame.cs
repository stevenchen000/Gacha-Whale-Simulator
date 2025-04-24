using CombatSystem;
using Godot;
using System;
using Godot.Collections;

public partial class MainGame : Node
{
    
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
        OpenMenu(SceneLibrary.PlayerRoomScene);
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
        //OpenMenu(SceneLibrary.scene);
    }

    public void OpenGachaMenu()
    {
        OpenMenu(SceneLibrary.GachaScene);
    }

    public void OpenPartyMenu()
    {
        OpenMenu(SceneLibrary.PartyMenuScene);
    }

    public void OpenStageSelectionMenu()
    {
        OpenMenu(SceneLibrary.StageSelectionScene);
    }

    public void OpenBattleMenu()
    {
        OpenMenu(SceneLibrary.BattleScene);
    }
    public void OpenHomeMenu()
    {
        OpenMenu(SceneLibrary.HomeScene);
    }

    public void OpenUpgradeMenu()
    {
        OpenMenu(SceneLibrary.UpgradeMenuScene);
    }

    public void OpenRoomMenu()
    {
        OpenMenu(SceneLibrary.PlayerRoomScene);
    }

    #endregion

    #region Menu Helpers

    public void OpenMenu(PackedScene menu)
    {
        LoadScreen.Activate();
        double transitionTime = LoadScreen.TransitionTime;
        DelayedCalls.AddCall(transitionTime, () => _OpenMenu(menu));
        DelayedCalls.AddCall(transitionTime + 0.1, () => MemoryLeakChecker.TrackAllChildren(currMenu));
        //DelayedCalls.AddCall(transitionTime + 0.1, () => MemoryLeakChecker.CheckStatus());
    }

    private void _OpenMenu(PackedScene menu)
    {
        CloseMenu(currMenu);
        var gameMenu = (GameMenu)(Utils.InstantiateCopy(menu));
        AddChild(gameMenu);
        MoveChild(gameMenu, 0);
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
