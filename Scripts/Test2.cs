using Godot;
using System;

public partial class Test2 : GameMenu
{
    public override void _Ready()
    {
        base._Ready();
        GD.Print("Ready");
    }

    public override void _Init()
    {
        GD.Print("Init");
    }

    public override void _Process(double delta)
    {
        GD.Print("Process");
    }

    public override void _EnterTree()
    {
        GD.Print($"Entered tree: {GetParent().Name}");
    }

    public override void _ExitTree()
    {
        GD.Print($"Exited tree, now parented to: {GetParent().Name}");
    }
}
