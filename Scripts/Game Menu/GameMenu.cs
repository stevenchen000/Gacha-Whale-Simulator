﻿using System;
using Godot;


public partial class GameMenu : Control
{
    protected SimpleWeakRef<MainGame> game;

    public override void _Ready()
    {
        var tempGame = Utils.FindParentOfType<MainGame>(this);
        game = new SimpleWeakRef<MainGame>(tempGame);
        DelayedCalls.AddCall(0.5, () => LoadScreen.Deactivate());
    }

    public virtual void _Init()
    {

    }

    public virtual void _OnDisable()
    {

    }
}

