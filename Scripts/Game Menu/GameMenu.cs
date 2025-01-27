using System;
using Godot;


public partial class GameMenu : Control
{
    protected MainGame game;

    public override void _Ready()
    {
        game = Utils.FindParentOfType<MainGame>(this);
    }

    public virtual void _Init()
    {

    }

    public virtual void _OnDisable()
    {

    }
}

