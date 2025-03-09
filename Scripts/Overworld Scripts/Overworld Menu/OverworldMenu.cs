using Godot;
using Godot.Collections;
using System;

public partial class OverworldMenu : Control
{
    private SimpleWeakRef<MainGame> game;
    [Export] private Array<MainMenuButton> buttons;
    [Export] private int currentMenuIndex = 0;

    /******************
     * Main functions
     * *******************/

    public override void _Ready()
    {
        var tempGame = Utils.FindParentOfType<MainGame>(this);
        game = new SimpleWeakRef<MainGame>(tempGame);
        CallDeferred(MethodName.Init);
    }

    private void Init()
    {
        buttons[currentMenuIndex].DisableMenu();
    }

    /************************
     * Scene functions
     * ********************/

    public void LoadHomeMenu()
    {
        //this is where the characters scroll through on menu
        game.Value.OpenHomeMenu();
    }

    public void LoadPartyMenu()
    {
        //this is where you edit parties
        Utils.Print(this, "Loading party menu...");
        game.Value.OpenPartyMenu();
    }

    public void LoadGachaMenu()
    {
        //this is where you pull for new characters
        Utils.Print(this, "Loading gacha menu...");
        game.Value.OpenGachaMenu();
    }

    public void LoadStageSelection()
    {
        //this is where you upgrade characters
        Utils.Print(this, "Loading stage selection...");
        game.Value.OpenStageSelectionMenu();
    }

    public void LoadUpgradeMenu()
    {
        Utils.Print(this, "Loading upgrade menu...");
        game.Value.OpenUpgradeMenu();
    }

    public void ReturnToRoom()
    {
        Utils.Print(this, "Returning home...");
        game.Value.OpenRoomMenu();
    }
}
