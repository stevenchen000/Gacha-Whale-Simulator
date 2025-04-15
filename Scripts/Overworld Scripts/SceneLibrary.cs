using Godot;
using System;

public partial class SceneLibrary : Node
{
    private static SceneLibrary _instance;

    [ExportCategory("Game Menus")]
    [Export] private PackedScene _playerRoomScene;
    public static PackedScene PlayerRoomScene { get { return _instance._playerRoomScene; } }

    [Export] private PackedScene _homeScene;
    public static PackedScene HomeScene { get { return _instance._homeScene; } }
    [Export] private PackedScene _partyScene;
    public static PackedScene PartyMenuScene { get { return _instance._partyScene; } }
    [Export] private PackedScene _gachaScene;
    public static PackedScene GachaScene { get { return _instance._gachaScene; } }
    [Export] private PackedScene _stageSelectionScene;
    public static PackedScene StageSelectionScene { get { return _instance._stageSelectionScene; } }
    [Export] private PackedScene _upgradeMenuScene;
    public static PackedScene UpgradeMenuScene { get{ return _instance._upgradeMenuScene; } }



    [ExportCategory("Combat Scenes")]
    [Export] private PackedScene _battleScene;
    public static PackedScene BattleScene { get { return _instance._battleScene; } }









    public override void _Ready()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            QueueFree();
        }
    }











}
