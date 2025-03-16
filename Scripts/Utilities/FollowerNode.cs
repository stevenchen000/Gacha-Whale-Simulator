using Godot;
using System;

public partial class FollowerNode : Node2D
{
    [Export] private Node2D nodeToFollow;
    [Export] private float followSpeed = 10f;

    public override void _Process(double delta)
    {
        var nodePos = nodeToFollow.Position;
        Position = Position.Lerp(nodePos, followSpeed * (float)delta);
    }
}
