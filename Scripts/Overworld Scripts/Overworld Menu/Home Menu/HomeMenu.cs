using Godot;
using Godot.Collections;
using System;

public partial class HomeMenu : GameMenu
{
    [Export] private Array<GameCharacter> _characters;
    [Export] private Node2D homescreenSprite;
    private TimeHandler timer;

    public override void _Init()
    {
        base._Init();
        timer = new TimeHandler();
    }

    public override void _Process(double delta)
    {
        timer.Tick(delta);

        if (timer.IntervalsBetween(5) == 1)
        {

        }
    }

    /// <summary>
    /// Used to save in editor,
    /// don't use anywhere else
    /// </summary>
    public void DebugSave()
    {
        GameState.SaveData();
    }
}
