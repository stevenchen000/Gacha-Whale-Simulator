using Godot;
using System;

public partial class RoomMenu : GameMenu
{



    public void ReturnToGame()
    {
        game.Value.OpenHomeMenu();
    }

    public void OpenCharacterEditor()
    {
        game.Value.OpenCharacterCreator();
    }

    public void ProgressTime()
    {
        GameState.ProgressTime();
    }
}
