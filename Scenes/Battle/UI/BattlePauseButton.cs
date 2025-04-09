using Godot;
using System;

public partial class BattlePauseButton : Node
{
    [Export] private PackedScene pauseMenu;
    [Export] private MenuStack menuStack;

    public void OpenPauseMenu()
    {
        menuStack.OpenMenu(pauseMenu);
    }
}
