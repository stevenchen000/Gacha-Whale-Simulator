using Godot;
using System;

public partial class DeleteParentAfterTime : Node
{
    [Export] private double waitTime = 1;
    private double time;

    public override void _Process(double delta)
    {
        if(time >= waitTime && time > 0)
        {
            GD.Print("Deleting object");
            GetParent().QueueFree();
        }
        time += delta;
    }
}
