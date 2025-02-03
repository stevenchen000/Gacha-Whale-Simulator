using Godot;
using System;

public partial class Test2 : GameMenu
{
    public override void _Ready()
    {
        base._Ready();
        Utils.Print(this, "Ready");
    }

    public override void _Init()
    {
        Utils.Print(this, "Init");
    }

    public override void _Process(double delta)
    {
        Utils.Print(this, "Process");
    }

    public override void _EnterTree()
    {
        Utils.Print(this, $"Entered tree: {GetParent().Name}");
    }

    public override void _ExitTree()
    {
        Utils.Print(this, $"Exited tree, now parented to: {GetParent().Name}");
    }
}
