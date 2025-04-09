using Godot;
using System;

public partial class DeleteAfterAnimation : Node
{
    private AnimationPlayer anim;

    public override void _Ready()
    {
        var parent = GetParent();
        anim = Utils.FindChildOfType<AnimationPlayer>(parent);
    }

    public override void _Process(double delta)
    {
        if (!anim.IsPlaying())
        {
            var parent = GetParent();
            parent.QueueFree();
            Utils.Print(this, "Deleted object");
        }
    }
}
